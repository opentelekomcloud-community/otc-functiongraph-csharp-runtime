namespace src
{

  using System;
  using System.IO;
  using System.Text;

  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;

  using Newtonsoft.Json.Linq;


  public partial class Program
  {
    private static readonly HttpClient httpClientUserNamePassword = new HttpClient();

    private static async Task<string> getTokenUserNamePasswordAsync(string userName, string userPassword, string domainName, string authURL, string projectID)
    {
      var tokenURI = authURL + "/auth/tokens?v3/auth/tokens?nocatalog=true";

      JObject authBody = new JObject(
          new JProperty("auth",
              new JObject(
                  new JProperty("identity",
                      new JObject(
                          new JProperty("methods",
                              new JArray("password")),
                          new JProperty("password",
                              new JObject(
                                  new JProperty("user",
                                      new JObject(
                                          new JProperty("name", userName),
                                          new JProperty("password", userPassword),
                                          new JProperty("domain",
                                              new JObject(
                                                  new JProperty("name", domainName)
                                              )
                                          )
                                      )
                                  )
                              )
                          )
                      )
                  ), new JProperty("scope",
                      new JObject(
                          new JProperty("domain",
                              new JObject(
                                  new JProperty("name", domainName)
                              )
                          ),
                          new JProperty("project",
                              new JObject(
                                  new JProperty("id", projectID)
                              )
                          )
                      )
                      )
              )
          )
      );

      string requestBody = authBody.ToString();
      var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

      string token = "";
      try
      {
        var response = await httpClientUserNamePassword.PostAsync(tokenURI, content);

        if (response.Headers.TryGetValues("X-Subject-Token", out var tokenValues))
        {
          token = string.Join("", tokenValues);
        }

        if (!response.IsSuccessStatusCode)
        {
          string responseText = await response.Content.ReadAsStringAsync();
          Console.WriteLine($"Error getting token: {responseText}");
        }
      }
      catch (HttpRequestException ex)
      {
        Console.WriteLine($"Error getting token: {ex.Message}");
      }

      return token;
    }

    /// <summary>  
    /// Call FunctionGraph function using SDK signing
    /// with Username and Password
    /// </summary>
    private static async Task callFunctionGraphUserNamePasswordAsync()
    {
      var userName = System.Environment.GetEnvironmentVariable("OTC_USER_NAME");
      var userPassword = System.Environment.GetEnvironmentVariable("OTC_USER_PASSWORD");

      var domainName = System.Environment.GetEnvironmentVariable("OTC_DOMAIN_NAME");

      var projectID = System.Environment.GetEnvironmentVariable("OTC_SDK_PROJECTID");
      var region = System.Environment.GetEnvironmentVariable("OTC_TENANT_NAME");
      var authURL = System.Environment.GetEnvironmentVariable("OTC_IAM_ENDPOINT");

      var token = await getTokenUserNamePasswordAsync(userName, userPassword, domainName, authURL, projectID);

      var fgEndpoint = $"https://functiongraph.{region}.otc.t-systems.com";

      var functionApp = "default";
      var functionName = "DefaultPython3_10";
      var functionVersion = "latest";

      var function_URN = $"urn:fss:{region}:{projectID}:function:{functionApp}:{functionName}:{functionVersion}";
      var invokeURI = $"{fgEndpoint}/v2/{projectID}/fgs/functions/{function_URN}/invocations";

      // X-Cff-Log-Type:
      // "tail": 4KB logs will be returned
      // "": no logs will be returned
      string X_CFF_LOG_TYPE = "tail";

      // X-CFF-Request-Version:
      // "v0" response body in text format
      // "v1" response body in json format
      string X_CFF_REQUEST_VERSION = "v1";


      // set request body
      JObject body = new JObject(
        new JProperty("key", "Hello World of OTC")
      );
      

      try
      {
        var request = new HttpRequestMessage(HttpMethod.Post, invokeURI);

        // set auth token header 
        request.Headers.Add("X-AUTH-Token", token);
        request.Headers.Add("X-Cff-Log-Type", X_CFF_LOG_TYPE);
        request.Headers.Add("X-CFF-Request-Version", X_CFF_REQUEST_VERSION);

        // Set request body
        request.Content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

        // Send the request
        var response = await httpClientUserNamePassword.SendAsync(request);

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