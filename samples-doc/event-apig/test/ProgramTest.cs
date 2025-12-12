namespace src;

using System;
using Xunit;
using System.IO;
using System.Text;
using OpenTelekomCloud.Serverless.Function.Common;
using OpenTelekomCloud.Serverless.Function.Events.APIG;
using System.Security.Cryptography;

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

    string folderProjectLevel = Utils.GetPathProjectFolder();

    // Read sample APIG event JSON from file
    string filePath = Path.Combine(folderProjectLevel, "resources/apig_event.json");

    using (StreamReader r = new StreamReader(filePath))
    {
      jsonInput = r.ReadToEnd();
      var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonInput));

      // call the handler
      var resultStream = program.Handler(inputStream, context);

      Assert.NotNull(resultStream);

      APIGResponse apigResponse;

      using (var reader = new StreamReader(resultStream))
      {
        string result = reader.ReadToEnd();
        Console.WriteLine("Function output: " + result);

        resultStream.Seek(0, SeekOrigin.Begin);
        apigResponse = serializer.Deserialize<APIGResponse>(resultStream);
      }

      Console.WriteLine("Function output: " + apigResponse.Body);

    }
  }

  [Fact]
  public void TestHandlerWithValidJsonBase64()
  {
    // Arrange
    var program = new Program();

    var context = new TestContextSimple();

    string jsonInput = "";

    string folderProjectLevel = Utils.GetPathProjectFolder();

    // Read sample APIG event JSON from file
    string filePath = Path.Combine(folderProjectLevel, "resources/apig_base64_event.json");

    using (StreamReader r = new StreamReader(filePath))
    {
      jsonInput = r.ReadToEnd();

      var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonInput));

      // call the handler
      var resultStream = program.Handler(inputStream, context);

      Assert.NotNull(resultStream);

      APIGResponse apigResponse;

      using (var reader = new StreamReader(resultStream))
      {
        string result = reader.ReadToEnd();
        Console.WriteLine("Function output: " + result);

        resultStream.Seek(0, SeekOrigin.Begin);
        apigResponse = serializer.Deserialize<APIGResponse>(resultStream);
      }

      Console.WriteLine("Function output: " + apigResponse.Body);

      Console.WriteLine(string.Join(", ", apigResponse.Headers.getAdditionalHeaderKeys()));

    }
  }

  [Fact]
  public void TestHandlerWithParameters()
  {
    // Arrange
    var program = new Program();

    var context = new TestContextSimple();

    string jsonInput = "";

    string folderProjectLevel = Utils.GetPathProjectFolder();

    // Read sample APIG event JSON from file
    string filePath = Path.Combine(folderProjectLevel, "resources/apig_event_with_params.json");

    using (StreamReader r = new StreamReader(filePath))
    {
      jsonInput = r.ReadToEnd();

      var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonInput));

      // call the handler
      var resultStream = program.Handler(inputStream, context);

      Assert.NotNull(resultStream);

      APIGResponse apigResponse;

      using (var reader = new StreamReader(resultStream))
      {
        string result = reader.ReadToEnd();
        Console.WriteLine("Function output: " + result);

        resultStream.Seek(0, SeekOrigin.Begin);
        apigResponse = serializer.Deserialize<APIGResponse>(resultStream);
      }

      Console.WriteLine("Function output: " + apigResponse.Body);

      Console.WriteLine(string.Join(", ", apigResponse.Headers.getAdditionalHeaderKeys()));

    }
  }

}