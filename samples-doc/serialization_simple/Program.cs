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

  

  class Program
  {

    public Stream Handler(Stream input, IFunctionContext context)
    {
      var logger = context.Logger;
      logger.Logf("Serializer sample CSharp runtime test");

      JsonSerializer test = new JsonSerializer();

      TestJson Testjson = test.Deserialize<TestJson>(input);

      if (Testjson != null)
      {
        logger.Logf("json Deserialize Key={0}", Testjson.Key);

      }
      else
      {
        return null;
      }

      return test.Serialize<TestJson>(Testjson);
    }

    public class TestJson
    {
      public string Key { get; set; }//Define the attribute of the serialization class as KetTest.

    }
  }
}
