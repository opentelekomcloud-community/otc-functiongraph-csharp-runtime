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


  class Program
  {

    public Stream HandlerECSStart(Stream inputEvent, IFunctionContext context)
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

        logger.Logf("CSharp runtime test: ECS start instance {0}", instanceId);
        Signer signer = new Signer();

        signer.Key = context.SecurityAccessKey;
        signer.Secret = context.SecuritySecretKey;

        signer.SecurityToken = context.SecurityToken;

        if (context.SecurityToken == null || context.SecurityToken == "")
        {
          logger.Log("SecurityToken not set, specify an agency with ecs permissions");
          sw.WriteLine("SecurityToken not set, specify an agency with ecs permissions");
          return new MemoryStream(ms.ToArray());
        }

        string ecs_endpoint = context.GetUserData("ECS_ENDPOINT", "ecs.eu-de.otc.t-systems.com");
        string projectID = context.ProjectId;

        HttpRequest r = new HttpRequest("POST",
          new Uri("https://" + ecs_endpoint + "/v1/" + projectID + "/cloudservers/action"));

        r.headers.Add("X-Project-Id", projectID);
        r.headers.Add("Content-Type", "application/json;charset=utf8");
        r.body = "{\"os-start\": {\"servers\": [ {\"id\": \"" + instanceId + "\"}]}}";

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
