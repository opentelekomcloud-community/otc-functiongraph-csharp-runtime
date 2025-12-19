namespace src;

using System;
using Xunit;
using System.IO;
using System.Text;
using OpenTelekomCloud.Serverless.Function.Common;


public class UnitTest1
{

  [Fact]
  public void TestHandlerWithValidJson()
  {
    // Arrange
    var program = new Program();

    // configure test context
    var context = new TestContextSimple(){
      TimeoutSeconds = 60
    };
    context.UserData["ud-a"] = "USER DATA A VALUE";

    string jsonInput = "";

    // Read sample CTS event JSON from file
    string filePath = "resources/simple.json";

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