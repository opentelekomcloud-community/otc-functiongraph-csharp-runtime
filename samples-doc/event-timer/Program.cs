namespace src
{

#if NET6_0_OR_GREATER
  using OpenTelekomCloud.Serverless.Function.Common;
#else
  using HC.Serverless.Function.Common;
  using OpenTelekomCloud.Serverless.Function.Common;
#endif
  using OpenTelekomCloud.Serverless.Function.Events.Timer;
  using System;
  using System.IO;
  using System.Text;

  class Program
  {
    public Stream myFunc(Stream inputEvent, IFunctionContext context)
    {
      string payload = "";

      var ms = new MemoryStream();
      using (var sw = new StreamWriter(ms))
      {

        var logger = context.Logger;
        logger.Logf("CSharp runtime test: Timer");

        JsonSerializer serializer = new JsonSerializer();

        TimerEvent anEvent = serializer.Deserialize<TimerEvent>(inputEvent);
        if (anEvent != null)
        {
          payload = anEvent.ToString();
        }
        else
        {
          payload = "?";
        }

        sw.WriteLine(payload);

      }
      return new MemoryStream(ms.ToArray());
    }
  }
}
