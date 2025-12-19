namespace src;

using System;
using Xunit;
using System.IO;
using System.Text;
using OpenTelekomCloud.Serverless.Function.Common;
using OpenTelekomCloud.Serverless.Function.Events.CTS;

public class UnitTest1
{

  JsonSerializer serializer = new JsonSerializer();

  [Fact]
  public void TestHandlerWithValidJson()
  {
    // Arrange
    var program = new Program();

    var context = new TestContextSimple();

    string jsonInput = "";

    // Read sample CTS event JSON from file
    string filePath = "resources/cts_event.json";

    using (StreamReader r = new StreamReader(filePath))
    {
      jsonInput = r.ReadToEnd();
      var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonInput));

      // call the handler
      var resultStream = program.Handler(inputStream, context);

      Assert.NotNull(resultStream);

      using (var reader = new StreamReader(resultStream))
      {
        string result = reader.ReadToEnd();
        Console.WriteLine("Function output: " + result);
      }

    }
  }

}