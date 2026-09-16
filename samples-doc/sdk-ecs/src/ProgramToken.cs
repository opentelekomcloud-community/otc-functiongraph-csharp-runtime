namespace src
{

  // ONLY FOR NET6.0 OR GREATER
  using OpenTelekomCloud.Serverless.Function.Common;

  using OpenTelekomCloud.Serverless.Function.Events.Timer;
  using System;
  using System.IO;
  using System.Text;

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

  public class ProgramToken
  {

    public Stream HandlerECS(Stream inputEvent, IFunctionContext context)
    {
      string payload = "";

      var ms = new MemoryStream();
      using (var sw = new StreamWriter(ms))
      {

        var logger = context.Logger;


        JsonSerializer serializer = new JsonSerializer();

        string action = "";

        TimerEvent anEvent = serializer.Deserialize<TimerEvent>(inputEvent);
        if (anEvent != null && !string.IsNullOrWhiteSpace(anEvent.UserEvent))
        {
          logger.Logf("Using action from TimerEvent.UserEvent: {0}", anEvent.UserEvent);
          action = anEvent.UserEvent.ToLower();
        }
        else
        {
          logger.Logf("Using action from user data ECS_ACTION");
          action = context.GetUserData("ECS_ACTION", "start").ToLower();
        }

        if (action != "start" && action != "stop" && action != "reboot")
        {
          logger.Logf("ECS_ACTION {0} not supported, only 'start', 'stop', 'reboot' is supported", action);
          sw.WriteLine($"ECS_ACTION {action} not supported, only 'start', 'stop', 'reboot' is supported");
          return new MemoryStream(ms.ToArray());
        }

        string instanceId = context.GetUserData("ECS_INSTANCE_ID", "");
        if (instanceId == "")
        {
          logger.Log("ECS_INSTANCE_ID user data not set");
          sw.WriteLine("ECS_INSTANCE_ID user data not set");
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


        if (context.Token == null || context.Token == "")
        {
          logger.Log("Token not set, specify an agency with ecs permissions");
          sw.WriteLine("Token not set, specify an agency with ecs permissions");
          return new MemoryStream(ms.ToArray());
        }

        logger.Logf("CSharp runtime test: ECS {0} instance {1}", action, instanceId);

        string projectID = context.ProjectId;

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

        Uri uri = new Uri($"{ecs_endpoint_url}/v1/{projectID}/cloudservers/action");

        using var client = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Headers.Add("X-Auth-Token", context.Token);
        request.Content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

        try
        {
          HttpResponseMessage resp = client.SendAsync(request).GetAwaiter().GetResult();
          string responseBody = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();

          logger.Logf("Response code {0}, {1}", (int)resp.StatusCode, resp.ReasonPhrase);
          sw.WriteLine(responseBody);
        }
        catch (HttpRequestException e)
        {
          logger.Logf(e.Message);
          sw.WriteLine(e.Message);
        }

        sw.WriteLine(payload);

      }
      return new MemoryStream(ms.ToArray());
    }
  }
}
