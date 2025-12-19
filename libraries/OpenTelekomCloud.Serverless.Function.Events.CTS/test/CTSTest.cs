namespace test
{

  using System;
  using Xunit;
  using System.IO;
  using System.Text;
  using OpenTelekomCloud.Serverless.Function.Common;
  using OpenTelekomCloud.Serverless.Function.Common.TestUtils;
  using OpenTelekomCloud.Serverless.Function.Events.CTS;

  using Newtonsoft.Json.Linq;
  using System.Collections.Generic;


  public class UnitTest1
  {

    JsonSerializer serializer = new JsonSerializer();

    [Fact]
    public void Test_CreateFunction()
    {

      Stream jsonStream = typeof(UnitTest1).GetResourceStream("resources", "cts_createFunction.json");
      // string json = typeof(UnitTest1).GetResourceAsString("resources", "cts_createFunction.json");

      // CTSEvent ctsEvent = serializer.Deserialize<CTSEvent>(new MemoryStream(Encoding.UTF8.GetBytes(json)));
      CTSEvent ctsEvent = serializer.Deserialize<CTSEvent>(jsonStream);

      Console.WriteLine(ctsEvent.Cts.Message);

      if (ctsEvent.Cts._properties != null)
      {

        IDictionary<string, JToken> p = ctsEvent.Cts._properties;

        bool success = p.TryGetValue("tracker_name", out JToken? trackerNameToken);
        if (success && trackerNameToken != null)
        {
          string trackerName = trackerNameToken.ToString();
          Console.WriteLine("Tracker Name: " + trackerName);
        }
        else
        {
          Console.WriteLine("Tracker Name not found in additional properties.");
        }


      }

    }
  

  [Fact]
    public void Test_Login()
    {

      Stream jsonStream = typeof(UnitTest1).GetResourceStream("resources", "cts_login.json");
      
      CTSEvent ctsEvent = serializer.Deserialize<CTSEvent>(jsonStream);

      Console.WriteLine(ctsEvent.Cts.Message);

      if (ctsEvent.Cts._properties != null)
      {

        IDictionary<string, JToken> p = ctsEvent.Cts._properties;

        bool success = p.TryGetValue("tracker_name", out JToken? trackerNameToken);
        if (success && trackerNameToken != null)
        {
          string trackerName = trackerNameToken.ToString();
          Console.WriteLine("Tracker Name: " + trackerName);
        }
        else
        {
          Console.WriteLine("Tracker Name not found in additional properties.");
        }


      }

    }
  }
}
