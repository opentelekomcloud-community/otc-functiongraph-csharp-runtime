namespace test
{
  using System;
  using Xunit;
  using System.IO;
  using System.Text;
  using OpenTelekomCloud.Serverless.Function.Common;
  using OpenTelekomCloud.Serverless.Function.Common.TestUtils;
  using OpenTelekomCloud.Serverless.Function.Events.OBSS3;

  using Newtonsoft.Json.Linq;
  using System.Collections.Generic;


  public class UnitTest1
  {

    JsonSerializer serializer = new JsonSerializer();

    [Fact]
    public void Test_OBSS3Event()
    {

      Stream jsonStream = typeof(UnitTest1).GetResourceStream("resources", "obss3_event.json");

      OBSS3Event obss3Event = serializer.Deserialize<OBSS3Event>(jsonStream);
      Console.WriteLine("OBSS3 Event: " + obss3Event.ToString());
    }

  }
}