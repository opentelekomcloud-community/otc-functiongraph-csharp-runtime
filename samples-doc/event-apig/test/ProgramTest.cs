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

    // Use the TestContext from serialization_simple test project
    var context = new TestContext();

    string jsonInput = "";

    string folderProjectLevel = GetPathProjectFolder();

    Console.WriteLine("Project folder: " + folderProjectLevel);


    // Read sample APIG event JSON from file
    string filePath = Path.Combine(folderProjectLevel, "resources/apig_event.json");

    using (StreamReader r = new StreamReader(filePath))
    {
      jsonInput = r.ReadToEnd();
      // APIGEvent apigEvent = JsonSerializer.Deserialize<APIGEvent>(json);
      // string eventJson = JsonSerializer.Serialize<APIGEvent>(apigEvent);

      var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonInput));

      // call the handler
      var resultStream = program.Handler(inputStream, context);

      using (var reader = new StreamReader(resultStream))
      {
        string result = reader.ReadToEnd();
        Console.WriteLine("Function output: " + result);

      }
    }


    
  }

  /// <summary>
  /// Get the path to the current project.
  /// </summary>
  /// <returns></returns>
  private static string GetPathProjectFolder()
  {
    string pathAssembly = System.Reflection.Assembly.GetExecutingAssembly().Location;
    string folderAssembly = System.IO.Path.GetDirectoryName(pathAssembly);

    string folderProjectLevel = System.IO.Path.GetFullPath(folderAssembly);

    folderProjectLevel = Path.Combine(folderProjectLevel, "../../../../");
    return folderProjectLevel;
  }

}