namespace test
{
  using System;
  using Xunit;
  using System.IO;
  using System.Text;
  using OpenTelekomCloud.Serverless.Function.Common;
  using OpenTelekomCloud.Serverless.Function.Common.TestUtils;
  using OpenTelekomCloud.Serverless.Function.Events.DMS4Kafka;

  using Newtonsoft.Json.Linq;
  using System.Collections.Generic;


  public class UnitTest1
  {

    JsonSerializer serializer = new JsonSerializer();

    [Fact]
    public void Test_DMS4KafkaEvent()
    {

      Stream jsonStream = typeof(UnitTest1).GetResourceStream("resources", "dms4kafka_event.json");

      DMS4KafkaEvent apigEvent = serializer.Deserialize<DMS4KafkaEvent>(jsonStream);

      Console.WriteLine("DMS4Kafka Event: " + apigEvent.ToString());
    }

  }
}