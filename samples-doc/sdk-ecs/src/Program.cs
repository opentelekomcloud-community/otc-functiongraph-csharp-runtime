namespace src
{

#if NET6_0_OR_GREATER
  using OpenTelekomCloud.Serverless.Function.Common;
#else
  using HC.Serverless.Function.Common;
#endif
  using OpenTelekomCloud.Serverless.Function.Events.Timer;
  using System;
  using System.IO;
  using System.Text;

  using OpenTelekomCloud.API.Signing.Core;

  using System.Net;
  using System.Net.Http;
  using Newtonsoft.Json.Linq;

  /// <summary>
  /// FunctionGraph C# runtime program for ECS operations
  /// 
  /// An agency with ECS permissions is required to run this function:
  /// e.g. ECS_USER
  /// 
  /// Following environment/user data variables are used:
  ///
  /// - ECS_INSTANCE_ID: ID of the ECS instance to operate on
  /// - ECS_ACTION: Action to perform on the ECS instance ("start", "stop", "reboot")
  /// - ECS_ACTION_TYPE: Type of action ("soft" or "hard"), default is "soft"
  /// - ECS_ENDPOINT_URL: ECS service endpoint, default is "https://ecs.eu-de.otc.t-systems.com"
  /// 
  /// 
  /// </summary>

  public class Program
  {

    /// <summary>
    /// Main method - not used in FunctionGraph but needed for compilation
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
      Console.WriteLine("This is a FunctionGraph C# runtime program");
    }

    public Stream HandlerECS(Stream inputEvent, IFunctionContext context)
    {
      string payload = "";

      var ms = new MemoryStream();
      using (var sw = new StreamWriter(ms))
      {

        var logger = context.Logger;

        string instanceId = context.GetUserData("ECS_INSTANCE_ID", "");
        if (instanceId == "")
        {
          logger.Log("ECS_INSTANCE_ID user data not set");
          sw.WriteLine("ECS_INSTANCE_ID user data not set");
          return new MemoryStream(ms.ToArray());
        }

        string action = context.GetUserData("ECS_ACTION", "start").ToLower();
        if (action == "")
        {
          logger.Logf("ECS_ACTION {0} not supported, only 'start', 'stop', 'reboot' is supported", action);
          sw.WriteLine($"ECS_ACTION {action} not supported, only 'start', 'stop', 'reboot' is supported");
          return new MemoryStream(ms.ToArray());
        }

        string actionType = context.GetUserData("ECS_ACTION_TYPE", "soft").ToUpper();
        if (actionType != "SOFT" && actionType != "HARD")
        {
          logger.Logf("ECS_ACTION_TYPE {0} not supported, only 'soft' and 'hard' is supported", actionType);
          sw.WriteLine($"ECS_ACTION_TYPE {actionType} not supported, only 'soft' and 'hard' is supported");
          return new MemoryStream(ms.ToArray());
        }

        string ecs_endpoint_url = context.GetUserData("ECS_ENDPOINT_URL", "https://ecs.eu-de.otc.t-systems.com");


        if (context.SecurityToken == null || context.SecurityToken == "")
        {
          logger.Log("SecurityToken not set, specify an agency with ecs permissions");
          sw.WriteLine("SecurityToken not set, specify an agency with ecs permissions");
          return new MemoryStream(ms.ToArray());
        }

        logger.Logf("CSharp runtime test: ECS {0} instance {1}", action, instanceId);
        Signer signer = new Signer
        {
          Key = context.SecurityAccessKey,
          Secret = context.SecuritySecretKey,
          SecurityToken = context.SecurityToken
        };


        string projectID = context.ProjectId;

        HttpRequest r = new HttpRequest("POST",
          new Uri($"{ecs_endpoint_url}/v1/{projectID}/cloudservers/action"));

        r.headers.Add("X-Project-Id", projectID);
        r.headers.Add("Content-Type", "application/json;charset=utf8");


        JObject body = null;
        switch (action)
        {
          // https://docs.otc.t-systems.com/elastic-cloud-server/api-ref/apis_recommended/batch_operations/starting_ecss_in_a_batch.html#starting-ecss-in-a-batch
          case "start":
            body = new JObject(
              new JProperty("os-start",
                new JObject(
                  new JProperty("servers",
                    new JArray(
                      new JObject(
                      new JProperty("id", instanceId)
                    )
                  )
                )
              )
            )
          );
            break;
          case "stop":
            // https://docs.otc.t-systems.com/elastic-cloud-server/api-ref/apis_recommended/batch_operations/stopping_ecss_in_a_batch.html#stopping-ecss-in-a-batch
            body = new JObject(
              new JProperty("os-stop",
                new JObject(
                new JProperty("type", actionType),
                new JProperty("servers",
                  new JArray(
                    new JObject(
                      new JProperty("id", instanceId)
                    )
                  )
                )
              )
            )
          );
            break;
          case "reboot":
            // https://docs.otc.t-systems.com/elastic-cloud-server/api-ref/apis_recommended/batch_operations/restarting_ecss_in_a_batch.html#restarting-ecss-in-a-batch
            body = new JObject(
              new JProperty("reboot",
                new JObject(
                  new JProperty("type", actionType),
                  new JProperty("servers",
                    new JArray(
                      new JObject(
                        new JProperty("id", instanceId)
                      )
                    )
                  )
                )
              )
            );
            break;
          default:
            body = null;
            break;
        }

        // set request body
        r.body = body.ToString();

        // Sign the request
        HttpWebRequest req = signer.Sign(r);

        try
        {
          var writer = new StreamWriter(req.GetRequestStream());
          writer.Write(r.body);
          writer.Flush();
          HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
          var reader = new StreamReader(resp.GetResponseStream());

          logger.Logf("Response code {0}, {1}", (int)resp.StatusCode, resp.StatusDescription);
          sw.WriteLine(reader.ReadToEnd());
        }
        catch (WebException e)
        {
          HttpWebResponse resp = (HttpWebResponse)e.Response;
          if (resp != null)
          {
            logger.Logf("Response code {0}, {1}", (int)resp.StatusCode, resp.StatusDescription);
            var reader = new StreamReader(resp.GetResponseStream());

            sw.WriteLine(reader.ReadToEnd());
          }
          else
          {
            logger.Logf(e.Message);
          }
        }

        sw.WriteLine(payload);

      }
      return new MemoryStream(ms.ToArray());
    }
  }
}
