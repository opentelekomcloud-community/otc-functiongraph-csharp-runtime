namespace OpenTelekomCloud.Serverless.Function.Events.APIG
{

  using System;
  using System.Collections.Generic;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;


  /// <summary>
  /// API Gateway (APIG) is an API hosting service that helps enterprises to build, manage, 
  /// and deploy APIs at any scale. With APIG, your function can be invoked through HTTPS by 
  /// using a custom REST API and a specified backend. You can map each API operation (such as, GET and PUT) 
  /// to a specific function. APIG invokes the relevant function when an HTTPS request is sent to the API backend.
  /// <para>For more information about how to use HTTPS calls to trigger functions, see 
  /// <see href="https://docs.otc.t-systems.com/function-graph/umn/creating_triggers/using_an_apig_dedicated_trigger.html#"/>
  /// </para>
  /// </summary>
  public class APIGEvent
  {
    [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
    public string Body { get; set; }

    /// <summary>
    /// Request information, including the API gateway configuration, request ID, authentication information, and source.
    /// </summary>
    [JsonProperty("requestContext", NullValueHandling = NullValueHandling.Ignore)]
    public RequestContext RequestContext { get; set; }

    /// <summary>
    /// Query strings configured in APIG and their actual values
    /// </summary>
    [JsonProperty("queryStringParameters", NullValueHandling = NullValueHandling.Ignore)]
    public QueryStringParameters QueryStringParameters { get; set; }

    /// <summary>
    /// http method
    /// </summary>
    [JsonProperty("httpMethod", NullValueHandling = NullValueHandling.Ignore)]
    public string HttpMethod { get; set; }

    /// <summary>
    /// Path parameters configured in APIG and their actual values
    /// </summary>
    [JsonProperty("pathParameters", NullValueHandling = NullValueHandling.Ignore)]
    public PathParameters PathParameters { get; set; }

    /// <summary>
    /// complete headers:
    /// "accept-language", 
    /// "accept-encoding",
    /// "x-forwarded-port",
    /// "x-forwarded-for",
    /// "accept",
    /// "upgrade-insecure-requests",
    /// "host"
    /// "x-forwarded-proto",
    /// "pragma",
    /// "cache-control",
    /// "x-real-ip", 
    /// "user-agent"
    /// </summary>

    [JsonProperty("headers", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, string> Headers { get; set; }

    /// <summary>
    /// complete path
    /// </summary>
    [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
    public string Path { get; set; }

    /// <summary>
    /// base64 encoded body (default: true)
    /// <para>
    /// When calling a function using APIG, isBase64Encoded is valued true by default, 
    /// indicating that the request body transferred to FunctionGraph is encoded using 
    /// Base64 and must be decoded for processing.
    /// </para>
    /// </summary>
    [JsonProperty("isBase64Encoded", NullValueHandling = NullValueHandling.Ignore)]
    public bool IsBase64Encoded { get; set; }


    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }




public class RequestContext
{
  [JsonProperty("apiId", NullValueHandling = NullValueHandling.Ignore)]
  public string ApiId { get; set; }

  [JsonProperty("requestId", NullValueHandling = NullValueHandling.Ignore)]
  public string RequestId { get; set; }

  [JsonProperty("stage", NullValueHandling = NullValueHandling.Ignore)]
  public string Stage { get; set; }
}

public class QueryStringParameters
{
  [JsonProperty("responseType", NullValueHandling = NullValueHandling.Ignore)]
  public string ResponseType { get; set; }

  [JsonExtensionData]
  public IDictionary<string, JToken> _parameters;
}

public class PathParameters
{
  [JsonExtensionData]
  public IDictionary<string, JToken> _parameters;

  public override string ToString()
  {
    return JsonConvert.SerializeObject(this);
  }

}

public class APIGResponse
{
  [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
  public string Body { get; set; }

  [JsonProperty("headers", NullValueHandling = NullValueHandling.Ignore)]
  public Dictionary<string, string> Headers { get; set; }

  [JsonProperty("statusCode", NullValueHandling = NullValueHandling.Ignore)]
  public int StatusCode { get; set; }

  [JsonProperty("isBase64Encoded", NullValueHandling = NullValueHandling.Ignore)]
  public bool IsBase64Encoded { get; set; }


}

}
