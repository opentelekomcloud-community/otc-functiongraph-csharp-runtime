namespace src
{

#if NET6_0_OR_GREATER
  using OpenTelekomCloud.Serverless.Function.Common;
#else
  using HC.Serverless.Function.Common;
  using OpenTelekomCloud.Serverless.Function.Common;
#endif

  using System;
  using System.IO;
  using System.Text;


  /// <summary>
  /// FunctionGraph C# runtime program for JSON serialization/deserialization example
  /// </summary>
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

    public Stream Handler(Stream input, IFunctionContext context)
    {
      var logger = context.Logger;
      logger.Logf("Serialization sample CSharp");

      JsonSerializer test = new JsonSerializer();

      // Deserialize input JSON
      TestJson testJson = test.Deserialize<TestJson>(input);

      if (testJson != null)
      {
        logger.Logf("json Deserialize Key={0}", testJson.Key);
      }
      else
      {
        return null;
      }

      // Serialize output JSON
      return test.Serialize<TestJson>(testJson);
    }


  }
}
