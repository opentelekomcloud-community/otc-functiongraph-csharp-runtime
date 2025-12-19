namespace test
{
  using System;
  using Xunit;
  using System.IO;
  using System.Text;
  using OpenTelekomCloud.Serverless.Function.Common;
  using OpenTelekomCloud.Serverless.Function.Common.TestUtils;
  using OpenTelekomCloud.Serverless.Function.Events.APIG;

  using Newtonsoft.Json.Linq;
  using System.Collections.Generic;


  public class UnitTest1
  {

    JsonSerializer serializer = new JsonSerializer();

    [Fact]
    public void Test_APIGEvent()
    {

      Stream jsonStream = typeof(UnitTest1).GetResourceStream("resources", "apig_base64_event.json");

      APIGEvent apigEvent = serializer.Deserialize<APIGEvent>(jsonStream);

      Console.WriteLine("APIG Event: " + apigEvent.ToString());
    }


    [Fact]
    public void Test_APIGResponse()
    {

      Stream jsonStream = typeof(UnitTest1).GetResourceStream("resources", "apig_response.json");

      APIGResponse apigResponse = serializer.Deserialize<APIGResponse>(jsonStream);

      Console.WriteLine("APIGResponse Event: " + apigResponse.ToString());
    }

  }
}