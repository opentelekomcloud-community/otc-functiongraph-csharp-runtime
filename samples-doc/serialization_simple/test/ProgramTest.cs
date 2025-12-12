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
        var context = new TestContextSimple();
        
        string jsonInput = "{\"Key\":\"TestValue\"}";
        var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonInput));

        // Act
        var resultStream = program.Handler(inputStream, context);

        // Assert
        Assert.NotNull(resultStream);
        
        using (var reader = new StreamReader(resultStream))
        {
            string result = reader.ReadToEnd();
            Assert.Contains("TestValue", result);
        }
    }

   
}