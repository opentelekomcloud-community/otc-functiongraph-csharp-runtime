namespace OpenTelekomCloud.Serverless.Function.Events.APIG
{

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;


  /// <summary>
  /// API Gateway (APIG) is an API hosting service that helps enterprises to build, manage, 
  /// and deploy APIs at any scale. With APIG, your function can be invoked through HTTPS by 
  /// using a custom REST API and a specified backend. 
  /// <para/>
  /// You can map each API operation (such as, <c>GET</c> and <c>PUT</c>) 
  /// to a specific function. <para/>
  /// APIG invokes the relevant function when an HTTPS request is sent to the API backend.
  /// <para>For more information about how to use HTTPS calls to trigger functions, see 
  /// <see href="https://docs.otc.t-systems.com/function-graph/umn/creating_triggers/using_an_apig_dedicated_trigger.html#"/>
  /// </para>
  /// </summary>
  public class APIGEvent
  {
    /// <summary>
    /// Request body (base64 encoded or plain text, <see cref="IsBase64Encoded"/>)
    /// </summary>
    [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
    public string Body { get; set; }


    /// <summary>
    /// Get the request body as string, decoding from Base64 if necessary
    /// </summary>
    /// <returns></returns>
    public string GetBodyAsString()
    {
      if (this.IsBase64Encoded)
      {
        byte[] data = System.Convert.FromBase64String(this.Body);
        return System.Text.Encoding.UTF8.GetString(data);
      }
      else
      {
        return this.Body;
      }
    }

    /// <summary>
    /// Request information, including the API gateway configuration, request ID, authentication information, and source.
    /// </summary>
    /// <returns><see cref="RequestContext"/></returns>
    [JsonProperty("requestContext", NullValueHandling = NullValueHandling.Ignore)]
    public RequestContext RequestContext { get; set; }

    /// <summary>
    /// Query strings configured in APIG and their actual values
    /// </summary>
    [JsonProperty("queryStringParameters", NullValueHandling = NullValueHandling.Ignore)]
    public QueryStringParameters QueryStringParameters { get; set; }

    /// <summary>
    /// HTTP request method
    /// </summary>
    /// <returns><see cref="string"/></returns>
    [JsonProperty("httpMethod", NullValueHandling = NullValueHandling.Ignore)]
    public string HttpMethod { get; set; }

    /// <summary>
    /// Path parameters configured in APIG and their actual values
    /// </summary>
    /// <returns><see cref="PathParameters"/></returns>
    [JsonProperty("pathParameters", NullValueHandling = NullValueHandling.Ignore)]
    public PathParameters PathParameters { get; set; }

    /// <summary>
    /// Request headers.
    /// </summary>
    [JsonProperty("headers", NullValueHandling = NullValueHandling.Ignore)]
    public APIGRequestHeaders Headers { get; set; }

    /// <summary>
    /// Return the path part of the HTTP request
    /// </summary>
    /// <returns><see cref="string"/></returns>
    [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
    public string Path { get; set; }

    /// <summary>
    /// base64 encoded body (default: true)
    /// <para/>
    /// When calling a function using APIG, <c>isBase64Encoded</c> is valued true by default, 
    /// indicating that the request body transferred to FunctionGraph is encoded using 
    /// Base64 and must be decoded for processing.
    /// 
    /// </summary>
    /// <returns><see cref="bool"/></returns>
    [JsonProperty("isBase64Encoded", NullValueHandling = NullValueHandling.Ignore)]
    public bool IsBase64Encoded { get; set; }

    /// <summary>
    /// Serialize APIGEvent to JSON string
    /// </summary>
    /// <returns>Serialized JSON string representation of the APIGEvent object</returns>
    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }

  /// <summary>
  /// Request headers
  /// </summary>
  public class APIGRequestHeaders
  {
    /// <summary>
    /// The HTTP Accept-Language request header indicates the natural
    /// language and locale that the client prefers. 
    /// </summary>
    [JsonProperty("accept-language", NullValueHandling = NullValueHandling.Ignore)]
    public string AcceptLanguage { get; set; }

    /// <summary>
    /// The HTTP Accept-Encoding  header indicates the content encoding
    /// (usually a compression algorithm) that the sender can understand.
    /// </summary>
    [JsonProperty("accept-encoding", NullValueHandling = NullValueHandling.Ignore)]
    public string AcceptEncoding { get; set; }

    /// <summary>
    /// The protocol port used by the listener is transmitted to backend servers through the HTTP header.
    /// </summary>
    [JsonProperty("x-forwarded-port", NullValueHandling = NullValueHandling.Ignore)]
    public string XForwardedPort { get; set; }

    /// <summary>
    /// Source IP addresses and proxy IP addresses of the clients are
    /// transmitted to backend servers through the HTTP header.
    /// </summary>
    [JsonProperty("x-forwarded-for", NullValueHandling = NullValueHandling.Ignore)]
    public string XForwardedFor { get; set; }


    /// <summary> 
    /// The HTTP Accept request and response header indicates which 
    /// content types, expressed as MIME types, the sender is able to understand.
    /// </summary>
    [JsonProperty("accept", NullValueHandling = NullValueHandling.Ignore)]
    public string Accept { get; set; }

    /// <summary>
    /// The HTTP Upgrade-Insecure-Requests request header sends a signal to the server 
    /// indicating the client's preference for an encrypted and authenticated response,
    /// and that the client can successfully handle the upgrade-insecure-requests CSP directive.
    /// </summary>
    [JsonProperty("upgrade-insecure-requests", NullValueHandling = NullValueHandling.Ignore)]
    public string UpgradeInsecureRequests { get; set; }

    /// <summary>
    /// The HTTP Host request header specifies the host and
    /// port number of the server to which the request is being sent.
    /// </summary>
    [JsonProperty("host", NullValueHandling = NullValueHandling.Ignore)]
    public string Host { get; set; }

    /// <summary>
    /// The protocol type (HTTP or HTTPS) of the request is transmitted to
    /// backend servers through the HTTP header.
    /// </summary>
    [JsonProperty("x-forwarded-proto", NullValueHandling = NullValueHandling.Ignore)]
    public string XForwardedProto { get; set; }

    /// <summary>
    /// The HTTP Pragma header is an implementation-specific header that may have
    /// various effects along the request-response chain. This header serves for
    /// backwards compatibility with HTTP/1.0 caches that do not
    /// support the Cache-Control HTTP/1.1 header.
    /// </summary>
    [JsonProperty("pragma", NullValueHandling = NullValueHandling.Ignore)]
    public string Pragma { get; set; }

    /// <summary>
    /// The HTTP Cache-Control header holds directives (instructions) in both requests
    /// and responses that control caching in browsers and shared caches
    /// (e.g., Proxies, CDNs).
    /// </summary>
    [JsonProperty("cache-control", NullValueHandling = NullValueHandling.Ignore)]
    public string CacheControl { get; set; }

    /// <summary>
    /// Source IP addresses of the clients are
    /// transmitted to backend servers through the HTTP header.
    /// </summary>
    [JsonProperty("x-real-ip", NullValueHandling = NullValueHandling.Ignore)]
    public string XRealIp { get; set; }

    /// <summary>
    /// The HTTP User-Agent request header is a characteristic string that lets
    /// servers and network peers identify the application, operating system,
    /// vendor, and/or version of the requesting user agent.
    /// </summary>
    [JsonProperty("user-agent", NullValueHandling = NullValueHandling.Ignore)]
    public string UserAgent { get; set; }

    /// <summary>
    /// Additional headers not mapped to properties
    /// see: <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers"/>
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JToken> _additionalHeaders;

    /// <summary>
    /// Add additional header key/value pair
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void addAdditionalHeader(string key, string value)
    {
      if (this._additionalHeaders == null)
      {
        this._additionalHeaders = new System.Collections.Generic.Dictionary<string, JToken>();
      }

      this._additionalHeaders.Add(key, new JValue(value));
    }

    /// <summary>
    /// Remove additional header by key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool removeAdditionalHeader(string key)
    {
      if (this._additionalHeaders != null && this._additionalHeaders.ContainsKey(key))
      {
        return this._additionalHeaders.Remove(key);
      }
      return false;
    }

    /// <summary>
    /// Get additional header by key 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string getAdditionalHeader(string key)
    {
      if (this._additionalHeaders != null && this._additionalHeaders.ContainsKey(key))
      {
        return this._additionalHeaders[key].ToString();
      }

      return null;
    }

    /// <summary>
    /// Get all additional header keys
    /// </summary>
    /// <returns></returns>
    public List<string> getAdditionalHeaderKeys()
    {
      if (this._additionalHeaders != null)
      {
        return new List<string>(this._additionalHeaders.Keys);
      }

      return null;
    }

    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }

  }


  /// <summary>
  /// Request context information
  /// </summary>
  public class RequestContext
  {
    /// <summary>
    ///  API ID
    /// </summary>
    [JsonProperty("apiId", NullValueHandling = NullValueHandling.Ignore)]
    public string ApiId { get; set; }

    /// <summary>
    ///  Request ID
    /// </summary>
    [JsonProperty("requestId", NullValueHandling = NullValueHandling.Ignore)]
    public string RequestId { get; set; }

    /// <summary>
    /// Stage name
    /// </summary>
    [JsonProperty("stage", NullValueHandling = NullValueHandling.Ignore)]
    public string Stage { get; set; }
  }

  /// <summary>
  /// Query strings configured in APIG and their actual values
  /// </summary>
  public class QueryStringParameters
  {
    [JsonProperty("responseType", NullValueHandling = NullValueHandling.Ignore)]
    public string ResponseType { get; set; }

    [JsonExtensionData]
    public IDictionary<string, JToken> _parameters;

    /// <summary>
    /// Get query parameter by key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string getParameter(string key)
    {
      if (this._parameters != null && this._parameters.ContainsKey(key))
      {
        return this._parameters[key].ToString();
      }

      return null;
    }

    /// <summary>
    /// Get all query parameter keys
    /// </summary>
    /// <returns></returns>
    public List<string> getParameterKeys()
    {
      if (this._parameters != null)
      {
        return new List<string>(this._parameters.Keys);
      }

      return new List<string>();
    }

  }


  /// <summary>
  /// Path parameters configured in APIG and their actual values
  /// </summary>
  public class PathParameters
  {
    [JsonExtensionData]
    public IDictionary<string, JToken> _parameters;

    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }

    /// <summary>
    /// Get all path parameters as array
    /// </summary>
    public string[] getParameters()
    {
      if (this._parameters != null)
      {
        string parameters = this._parameters[""].ToString();

        return parameters.Split('/');
      }

      return null;
    }

  }

  /// <summary>
  /// API Gateway (APIG) Response object
  /// </summary>
  public class APIGResponse
  {

    /// <summary>
    /// Constructor setting default IsBase64Encoded to true
    /// </summary>
    public APIGResponse()
    {
      this.IsBase64Encoded = true;
    }

    /// <summary>
    /// HTTP Response body
    /// </summary>
    [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
    public string Body { get; set; }

    /// <summary>
    /// HTTP Response headers
    /// </summary>
    [JsonProperty("headers", NullValueHandling = NullValueHandling.Ignore)]
    public APIGResponseHeaders Headers { get; set; }

    /// <summary>
    /// HTTP Status code
    /// </summary>
    [JsonProperty("statusCode", NullValueHandling = NullValueHandling.Ignore)]
    public int StatusCode { get; set; }

    /// <summary>
    /// Base64 encoded body indicator
    /// </summary>
    [JsonProperty("isBase64Encoded", NullValueHandling = NullValueHandling.Ignore)]
    public bool IsBase64Encoded { get; set; }


  }

  public class APIGResponseHeaders
  {
    /// <summary>
    /// Constructor setting default Content-Type to application/json
    /// </summary>
    public APIGResponseHeaders()
    {
      this.ContentType = "application/json";
    }

    /// <summary>
    /// Content-Type header
    /// </summary>
    [JsonProperty("Content-Type", NullValueHandling = NullValueHandling.Ignore)]
    public string ContentType { get; set; }

    [JsonExtensionData]
    public IDictionary<string, JToken> _additionalHeaders;

    /// <summary>
    /// Add additional header key/value pair
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void addAdditionalHeader(string key, string value)
    {
      if (this._additionalHeaders == null)
      {
        this._additionalHeaders = new System.Collections.Generic.Dictionary<string, JToken>();
      }

      this._additionalHeaders.Add(key, new JValue(value));
    }

    /// <summary>
    /// Get additional header by key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string getAdditionalHeader(string key)
    {
      if (this._additionalHeaders != null && this._additionalHeaders.ContainsKey(key))
      {
        return this._additionalHeaders[key].ToString();
      }

      return null;
    }

    /// <summary>
    /// Get all additional header keys
    /// </summary>
    /// <returns></returns>
    public List<string> getAdditionalHeaderKeys()
    {
      if (this._additionalHeaders != null)
      {
        return new List<string>(this._additionalHeaders.Keys);
      }

      return new List<string>();
    }

    /// <summary>
    /// Remove additional header by key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool removeAdditionalHeader(string key)
    {
      if (this._additionalHeaders != null && this._additionalHeaders.ContainsKey(key))
      {
        return this._additionalHeaders.Remove(key);
      }
      return false;
    }

  }

}
