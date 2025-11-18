namespace src
{

#if NET6_0_OR_GREATER
  using OpenTelekomCloud.Serverless.Function.Common;
#else
  using HC.Serverless.Function.Common;
  using OpenTelekomCloud.Serverless.Function.Common;
#endif
  using OpenTelekomCloud.Serverless.Function.Events.OBSS3;
  using System;
  using System.IO;
  using System.Text;
  using Amazon.S3;
  using Amazon.S3.Model;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Amazon;

  using SkiaSharp;

  using System.Text.RegularExpressions;


  class Program
  {

    private const float MAX_DIMENSION = 100;
    private const string REGEX = ".*\\.([^\\.]*)";
    private const string JPG_TYPE = "jpg";
    private const string JPG_MIME = "image/jpeg";
    private const string PNG_TYPE = "png";
    private const string PNG_MIME = "image/png";

    public static void Main(string[] args)
    {

    }

    public void Initializer(IFunctionContext context)
    {
      var logger = context.Logger;
      logger.Logf("CSharp runtime test: OBSS3 - Initializer");
    }


    public Stream Handler(Stream inputEvent, IFunctionContext context)
    {

      var logger = context.Logger;
      logger.Logf("CSharp runtime test: OBSS3");

      var ms = new MemoryStream();

      JsonSerializer serializer = new JsonSerializer();

      OBSS3Event anEvent = serializer.Deserialize<OBSS3Event>(inputEvent);

      S3EventNotificationRecord record = anEvent.Records[0];
      string bucketName = record.S3.Bucket.Name;
      string srcKey = record.S3.Object.Key;

      logger.Logf($"Processing file: {bucketName}/{srcKey}");

      string dstBucket = context.GetUserData("OUTPUT_BUCKET");
      string dstKey = $"resized-{srcKey}";

      AmazonS3Config config = new AmazonS3Config();
      config.ServiceURL = context.GetUserData("OBS_ENDPOINT", "https://obs.otc.t-systems.com");
      config.UseHttp = false;
      config.SignatureVersion = "v4";
      AWSConfigsS3.UseSignatureVersion4 = true;

      string sAccessKeyId = context.SecurityAccessKey;
      string sAccessKeySecret = context.SecuritySecretKey;
      string sSessionToken = context.SecurityToken;

      using AmazonS3Client s3Client = new AmazonS3Client(
              sAccessKeyId,
              sAccessKeySecret,
              sSessionToken,
              config
              );


      Match match = Regex.Match(srcKey, REGEX);
      if (!match.Success)
      {
        logger.Logf($"Unable to infer image type for key {srcKey}");
        return null;
      }

      // Check if the image type is supported (jpg/png)
      string imageType = match.Groups[1].Value;
      if (imageType != JPG_TYPE && imageType != PNG_TYPE)
      {
        logger.Logf($"Skipping non-image {srcKey}");
        return null;
      }

      Task<GetObjectResponse> getResult = null;

      GetObjectRequest getObjectRequest = new GetObjectRequest()
      {
        BucketName = bucketName,
        Key = srcKey
      };

      try
      {
        getResult = s3Client.GetObjectAsync(getObjectRequest);
        getResult.Wait();
      }
      catch (Exception ex)
      {
        logger.Logf($"Error fetching object {srcKey} from bucket {bucketName}: {ex.Message}");
        return null;
      }

      logger.Logf($"Downloaded file size: {getResult.Result.ContentLength} bytes");

      using Stream newImageStream = new MemoryStream();
      long size = ResizeImage(getResult.Result.ResponseStream, newImageStream);
      logger.Logf($"Resized image size: {size} bytes");

      newImageStream.Position = 0;

      PutObjectRequest putRequest = new PutObjectRequest()
      {

        BucketName = dstBucket,
        Key = dstKey,
        InputStream = newImageStream,
        ContentType = getResult.Result.Headers["Content-Type"],
        AutoCloseStream = true,
      };

      Task<PutObjectResponse> putResponse = null;
      try
      {
        putResponse = s3Client.PutObjectAsync(putRequest);
        putResponse.Wait();
      }
      catch (Exception ex)
      {
        logger.Logf($"Error saving object {dstKey} to bucket {dstBucket}: {ex.Message}");
      }

      return null;
    }

    private long ResizeImage(Stream srcStream, Stream dstStream)
    {
      using SKCodec codec = SKCodec.Create(srcStream);
      using SKBitmap srcImage = SKBitmap.Decode(codec);

      float scaleFactor = Math.Min(
          MAX_DIMENSION / (float)srcImage.Width, MAX_DIMENSION / (float)srcImage.Height);

      int newWidth = (int)(srcImage.Width * scaleFactor);
      int newHeight = (int)(srcImage.Height * scaleFactor);

      using SKBitmap scaledBitmap = srcImage.Resize(new SKSizeI(newWidth, newHeight), new SKSamplingOptions(SKFilterMode.Linear));
      using SKData newImage = scaledBitmap.Encode(codec.EncodedFormat, 50);

      newImage.SaveTo(dstStream);

      return newImage.Size;

    }

  }

}
