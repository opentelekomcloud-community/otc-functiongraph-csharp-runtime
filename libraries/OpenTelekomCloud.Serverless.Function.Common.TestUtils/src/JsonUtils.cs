namespace OpenTelekomCloud.Serverless.Function.Common.TestUtils
{
  using System;
  using System.IO;
  using Newtonsoft.Json;

  public class JsonUtils
  {
    /// <summary>
    /// return prettified json string
    /// </summary>
    /// <param name="json">unformatted json string</param>
    /// <returns>prettified json string</returns>
    public static string JsonPrettify(string json)
    {
      using (var stringReader = new StringReader(json))
      using (var stringWriter = new StringWriter())
      {
        var jsonReader = new JsonTextReader(stringReader);
        var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
        jsonWriter.WriteToken(jsonReader);
        return stringWriter.ToString();
      }
    }
  }

}