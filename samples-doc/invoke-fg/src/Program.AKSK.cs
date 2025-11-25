namespace src
{

  using System;
  using System.IO;
  using System.Text;
  using System.Threading.Tasks;

  using OpenTelekomCloud.API.Signing.Core;

  using System.Net;
  using System.Net.Http;

  using Newtonsoft.Json.Linq;


  public partial class Program
  {
    private static readonly HttpClient httpClient_AKSK = new HttpClient();

    /// <summary>  
    /// Call FunctionGraph function using SDK signing
    /// with Access Key and Secret Key
    /// </summary>
    private static async Task callFunctionGraphAKSKAsync()
    {
      Console.WriteLine("Calling FunctionGraph function with Access Key and Secret Key...");


      var projectID = System.Environment.GetEnvironmentVariable("OTC_SDK_PROJECTID");
      var region = System.Environment.GetEnvironmentVariable("OTC_TENANT_NAME");

      var ak = System.Environment.GetEnvironmentVariable("OTC_SDK_AK");
      var sk = System.Environment.GetEnvironmentVariable("OTC_SDK_SK");

      var fgEndpoint = $"https://functiongraph.{region}.otc.t-systems.com";

      var functionApp = "default";
      var functionName = "DefaultPython3_10";
      var functionVersion = "latest";

      var function_URN = $"urn:fss:{region}:{projectID}:function:{functionApp}:{functionName}:{functionVersion}";
      var invokeURI = $"{fgEndpoint}/v2/{projectID}/fgs/functions/{function_URN}/invocations";

      Signer signer = new Signer{
        Key = ak,
        Secret = sk
      };

      HttpRequest r = new HttpRequest("POST",
          new Uri(invokeURI));

      // add required headers
      r.headers.Add("X-Project-Id", projectID);
      r.headers.Add("Content-Type", "application/json;charset=utf8");

      // X-Cff-Log-Type:
      // "tail": 4KB logs will be returned
      // "": no logs will be returned
      string X_CFF_LOG_TYPE = "tail";
      r.headers.Add("X-Cff-Log-Type", X_CFF_LOG_TYPE);

      // X-CFF-Request-Version:
      // "v0" response body in text format
      // "v1" response body in json format

      string X_CFF_REQUEST_VERSION = "v1";
      r.headers.Add("X-CFF-Request-Version", X_CFF_REQUEST_VERSION);

      // set request body
      JObject body = new JObject(
        new JProperty("key", "Hello World of OTC")
      );
      
      r.body = body.ToString();

      // Sign the request
      HttpWebRequest req = signer.Sign(r);

      Console.WriteLine($"Invoking FunctionGraph function using URI {invokeURI}");

      try
      {
        // Create HttpRequestMessage from the signed HttpWebRequest
        var request = new HttpRequestMessage(HttpMethod.Post, invokeURI);

        // Set request body
        request.Content = new StringContent(r.body, Encoding.UTF8, "application/json");

        // Send the request
        var response = await httpClient_AKSK.SendAsync(request);

        Console.WriteLine("Response code {0}, {1}", (int)response.StatusCode, response.ReasonPhrase);

        string responseBody = await response.Content.ReadAsStringAsync();

        if (X_CFF_REQUEST_VERSION == "v1")
        {
          // OutputType is json
          var data = JObject.Parse(responseBody);

          Console.WriteLine("---- Result ----");

          Console.WriteLine(data.SelectToken("result")?.ToString());

          if (X_CFF_LOG_TYPE == "tail")
          {
            Console.WriteLine("---- Logs ----");
            Console.WriteLine(data.SelectToken("log")?.ToString());
          }
        }
        else
        {
          // OutputType is text
          Console.WriteLine(responseBody);
        }
      }
      catch (HttpRequestException e)
      {
        Console.WriteLine($"Request error: {e.Message}");
      }
      catch (Exception e)
      {
        Console.WriteLine($"Error: {e.Message}");
      }
    }
  }
}