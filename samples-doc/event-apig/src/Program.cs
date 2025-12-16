namespace src
{

#if NET6_0_OR_GREATER
  using OpenTelekomCloud.Serverless.Function.Common;
#else
  using HC.Serverless.Function.Common;
  using OpenTelekomCloud.Serverless.Function.Common;
#endif
  using OpenTelekomCloud.Serverless.Function.Events.APIG;
  using System;
  using System.IO;
  using System.Text;

  public class Program
  {

    /// <summary>
    /// Main method - not used in FunctionGraph but needed for compilation
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
      Console.WriteLine("This is a FunctionGraph C# runtime program");
    }

    /// <summary>
    /// Handler method for APIG event
    /// </summary>
    public Stream Handler(Stream inputEvent, IFunctionContext context)
    {
      string payload = "";

      JsonSerializer serializer = new JsonSerializer();

      var logger = context.Logger;
      logger.Logf("CSharp runtime test: APIG");

      APIGEvent apigEvent = serializer.Deserialize<APIGEvent>(inputEvent);

      if (apigEvent != null)
      {
        payload = apigEvent.GetBodyAsString();
        logger.Logf("APIG Event Payload: {0}", payload);
        
        // display path parameters
        var parameters = apigEvent.PathParameters.getParameters();
        if (parameters != null)
        {
          logger.Log("################# Path Parameter ##############################################");
          for (int i = 0; i < parameters.Length; i++)
          {
            logger.Logf("Path Parameter[{0}]={1}", i, parameters[i]);
          }
        }
        var headers = apigEvent.Headers;
        if (headers.getAdditionalHeaderKeys() != null)
        {
          logger.Log("################# additional Headers #####################################################");
          foreach (var header in headers.getAdditionalHeaderKeys())
          {
            logger.Logf("Header '{0}' = '{1}'", header, headers.getAdditionalHeader(header)); 
          }
        }
      }
      else
      {
        payload = "?";
      }

      string body = "";

      if (apigEvent.IsBase64Encoded)
      {
        body = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(payload));
      }
      else
      {
        body = payload;
      }

      APIGResponseHeaders responseHeaders = new APIGResponseHeaders();
      responseHeaders.addAdditionalHeader("X-Custom-Header", "CustomValue");

      APIGResponse response = new APIGResponse()
      {
        StatusCode = 200,
        Body = body,
        IsBase64Encoded = apigEvent.IsBase64Encoded,
        Headers = responseHeaders
      };

      return serializer.Serialize<APIGResponse>(response);
    }
  }
}
