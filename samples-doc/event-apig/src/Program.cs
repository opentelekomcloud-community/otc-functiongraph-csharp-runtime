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
    public Stream Handler(Stream inputEvent, IFunctionContext context)
    {
      string payload = "";


      JsonSerializer serializer = new JsonSerializer();

      var logger = context.Logger;
      logger.Logf("CSharp runtime test: APIG");

      APIGEvent apigEvent = serializer.Deserialize<APIGEvent>(inputEvent);

      if (apigEvent != null)
      {
        payload = apigEvent.Body;
        logger.Logf("json Deserialize Body={0}", payload);
      }
      else
      {
        payload = "?";
      }


      APIGResponse response = new APIGResponse()
      {
        StatusCode = 200,
        Body = payload,
        IsBase64Encoded = false,
        Headers = new System.Collections.Generic.Dictionary<string, string>()
        {
          { "Content-Type", "application/json" }
        }
        
      };

      return serializer.Serialize<APIGResponse>(response);
    }
  }
}
