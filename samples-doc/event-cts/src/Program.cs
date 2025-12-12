namespace src
{

#if NET6_0_OR_GREATER
  using OpenTelekomCloud.Serverless.Function.Common;
#else
  using HC.Serverless.Function.Common;
  using OpenTelekomCloud.Serverless.Function.Common;
#endif
  using OpenTelekomCloud.Serverless.Function.Events.CTS;
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
    /// Handler method for CTS event
    /// </summary>
    public Stream Handler(Stream inputEvent, IFunctionContext context)
    {

      JsonSerializer serializer = new JsonSerializer();

      var logger = context.Logger;
      logger.Logf("CSharp runtime test: CTS");

      CTSEvent ctsEvent = serializer.Deserialize<CTSEvent>(inputEvent);

      var cts= ctsEvent.Cts;

      if (cts != null)
      {
        logger.Logf("Trace Name: {0} , Time: {1}", cts.TraceName, cts.Time);
      }

      var ms = new MemoryStream();
      using (var sw = new StreamWriter(ms))
      {
        sw.WriteLine("OK");
      }

      return new MemoryStream(ms.ToArray());
    }
  }
}
