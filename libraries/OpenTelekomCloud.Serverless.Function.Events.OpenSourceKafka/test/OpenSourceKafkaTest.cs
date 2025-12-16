namespace test
{
  using System;
  using Xunit;
  using System.IO;
  using System.Text;
  using OpenTelekomCloud.Serverless.Function.Common;
  using OpenTelekomCloud.Serverless.Function.Common.TestUtils;
  using OpenTelekomCloud.Serverless.Function.Events.OpenSourceKafka;

  using Newtonsoft.Json.Linq;
  using System.Collections.Generic;


  public class UnitTest1
  {

    JsonSerializer serializer = new JsonSerializer();

    [Fact]
    public void Test_OpenSourceKafkaEvent()
    {

      Stream jsonStream = typeof(UnitTest1).GetResourceStream("resources", "opensource_kafka_event.json");

      OpenSourceKafkaEvent openSourceKafkaEvent = serializer.Deserialize<OpenSourceKafkaEvent>(jsonStream);
      Console.WriteLine("OpenSourceKafka Event: " + openSourceKafkaEvent.ToString());
    }

  }
}