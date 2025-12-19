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


  public class Program
  {

    private const float MAX_DIMENSION = 100;
    private const string REGEX = ".*\\.([^\\.]*)";
    private const string JPG_TYPE = "jpg";
    private const string JPG_MIME = "image/jpeg";
    private const string PNG_TYPE = "png";
    private const string PNG_MIME = "image/png";

    /// <summary>
    /// Main method - not used in FunctionGraph but needed for compilation
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
      Console.WriteLine("This is a FunctionGraph C# runtime program");
    }

    /// <summary>
    /// Initializer of the function
    /// </summary>
    /// <param name="context"></param>
    public void Initializer(IFunctionContext context)
    {
      var logger = context.Logger;
      logger.Logf("CSharp runtime test: OBSS3 - Initializer");
    }

    /// <summary>
    /// Handler of the function
    /// </summary>
    /// <param name="inputEvent"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public Stream Handler(Stream inputEvent, IFunctionContext context)
    {

      var logger = context.Logger;
      logger.Logf("CSharp runtime test: OBSS3");

      string endpoint_url = context.GetUserData("OBS_ENDPOINT", "https://obs.otc.t-systems.com");

      var ms = new MemoryStream();

      JsonSerializer serializer = new JsonSerializer();

      OBSS3Event anEvent = serializer.Deserialize<OBSS3Event>(inputEvent);

      S3EventNotificationRecord record = anEvent.Records[0];
      string bucketName = record.S3.Bucket.Name;
      string srcKey = record.S3.Object.Key;

      logger.Logf($"Processing file: {bucketName}/{srcKey}");

      string dstBucket = context.GetUserData("OUTPUT_BUCKET");
      string dstKey = $"resized-{srcKey}";

      AmazonS3Config config = new AmazonS3Config
      {
        ServiceURL = endpoint_url,
        UseHttp = false,
        SignatureVersion = "v4",
        ForcePathStyle = true
      }
      ;
      AWSConfigsS3.UseSignatureVersion4 = true;

      string sAccessKeyId = context.SecurityAccessKey;
      string sAccessKeySecret = context.SecuritySecretKey;
      string sSessionToken = context.SecurityToken;

      if (string.IsNullOrEmpty(sAccessKeyId) || string.IsNullOrEmpty(sAccessKeySecret) || string.IsNullOrEmpty(sSessionToken))
      {
        logger.Logf("Failed to access OBS because no temporary AK, SK, or token has been obtained. Please set an agency.");
        return CreateResponse("Failed to access OBS because no temporary AK, SK, or token has been obtained. Please set an agency.");
      }

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
        return CreateResponse($"Unable to infer image type for key {srcKey}");
      }

      // Check if the image type is supported (jpg/png)
      string imageType = match.Groups[1].Value;
      if (imageType != JPG_TYPE && imageType != PNG_TYPE)
      {
        logger.Logf($"Skipping non-image {srcKey}");
        return CreateResponse($"Skipping non-image {srcKey}");
      }

      Task<GetObjectResponse> getResultTask = null;

      GetObjectRequest getObjectRequest = new GetObjectRequest()
      {
        BucketName = bucketName,
        Key = srcKey
      };

      try
      {
        getResultTask = s3Client.GetObjectAsync(getObjectRequest);
        getResultTask.Wait();
      }
      catch (Exception ex)
      {
        logger.Logf($"Error fetching object {srcKey} from bucket {bucketName}: {ex.Message}");
        return CreateResponse($"Error fetching object {srcKey} from bucket {bucketName}: {ex.Message}");
      }

      logger.Logf($"Downloaded file size: {getResultTask.Result.ContentLength} bytes");

      using Stream newImageStream = new MemoryStream();
      long size = ResizeImage(getResultTask.Result.ResponseStream, newImageStream);
      logger.Logf($"Resized image size: {size} bytes");

      newImageStream.Position = 0;

      PutObjectRequest putRequest = new PutObjectRequest()
      {

        BucketName = dstBucket,
        Key = dstKey,
        InputStream = newImageStream,
        ContentType = getResultTask.Result.Headers["Content-Type"],
        AutoCloseStream = true,
      };

      Task<PutObjectResponse> putResponseTask = null;
      try
      {
        putResponseTask = s3Client.PutObjectAsync(putRequest);
        putResponseTask.Wait();
      }
      catch (Exception ex)
      {
        logger.Logf($"Error saving object {dstKey} to bucket {dstBucket}: {ex.Message}");
        return CreateResponse($"Error saving object {dstKey} to bucket {dstBucket}: {ex.Message}");
      }

      return CreateResponse("OK");
    }

    /// <summary>
    /// Creates a response stream with the given message.
    /// </summary>
    /// <param name="message">Message to include in response</param>
    /// <returns>Memory stream containing the message</returns>
    private Stream CreateResponse(string message)
    {
      var ms = new MemoryStream();
      using (var sw = new StreamWriter(ms, leaveOpen: true))
      {
        sw.WriteLine(message);
      }
      ms.Position = 0;
      return ms;
    }

    /// <summary>
    /// Resizes an image from the source stream and writes it to the destination stream.
    /// </summary>
    /// <param name="srcStream"></param>
    /// <param name="dstStream"></param>
    /// <returns></returns>
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
