namespace test
{
  using System;
  using Xunit;
  using System.IO;
  using System.Text;
  using OpenTelekomCloud.Serverless.Function.Common;
  using OpenTelekomCloud.Serverless.Function.Common.TestUtils;
  using OpenTelekomCloud.Serverless.Function.Events.Timer;

  using Newtonsoft.Json.Linq;
  using System.Collections.Generic;


  public class UnitTest1
  {

    JsonSerializer serializer = new JsonSerializer();

    [Fact]
    public void Test_TimerEvent()
    {

      Stream jsonStream = typeof(UnitTest1).GetResourceStream("resources", "timer_event.json");

      TimerEvent timerEvent = serializer.Deserialize<TimerEvent>(jsonStream);
      Console.WriteLine("Timer Event: " + timerEvent.ToString());
    }

  }
}