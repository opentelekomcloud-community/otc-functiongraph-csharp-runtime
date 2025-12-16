namespace OpenTelekomCloud.Serverless.Function.Events.CTS
{
  using System;
  using System.Collections.Generic;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

  /// <summary>
  /// According to the CTS cloud audit service type and the event notification required for 
  /// the operation subscription, when the CTS cloud audit service obtains the subscribed 
  /// operation record, the collected operation record is passed as a parameter ( CTS sample event ) 
  /// through the CTS trigger to call the FunctionGraph function. 
  /// Through the function, the key information in the log is analyzed and processed, and the system, 
  /// network and other business modules are automatically repaired, or alarms are generated through SMS,
  /// email, etc. to notify business personnel to handle. 
  /// For the use of CTS triggers, please refer to Using a CTS Trigger. 
  /// <para>
  /// See: <see href="https://docs.otc.t-systems.com/function-graph/umn/creating_triggers/using_a_cts_trigger.html"/>
  /// </para>
  /// </summary>
  public class CTSEvent
  {
    [JsonProperty("cts", NullValueHandling = NullValueHandling.Ignore)]
    public Cts Cts { get; set; }

    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }

  public class Cts
  {
    /// <summary>
    /// time in epoch
    /// </summary>
    [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
    public long Time { get; set; }

    /// <summary>
    /// Information about the user who initiated this request
    /// </summary>
    [JsonProperty("user", NullValueHandling = NullValueHandling.Ignore)]
    public User User { get; set; }

    /// <summary>
    /// Event request content
    /// </summary>

    [JsonProperty("request", NullValueHandling = NullValueHandling.Ignore)]
    public Request Request { get; set; }

    /// <summary>
    /// Incident response content
    /// </summary>
    [JsonProperty("response", NullValueHandling = NullValueHandling.Ignore)]
    public Response Response { get; set; }

    /// <summary>
    /// 	Event response code, such as 200, 400
    /// </summary>
    [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
    public int Code { get; set; }

    /// <summary>
    /// Abbreviation of the sender, such as vpc, ecs, etc.
    /// </summary>
    [JsonProperty("service_type", NullValueHandling = NullValueHandling.Ignore)]
    public string ServiceType { get; set; }

    /// <summary>
    /// The sender resource type, such as vm, vpn, etc.
    /// </summary>
    [JsonProperty("resource_type", NullValueHandling = NullValueHandling.Ignore)]
    public string ResourceType { get; set; }

    /// <summary>
    /// Resource name, such as the name of a virtual machine in the ecs service
    /// </summary>
    [JsonProperty("resource_name", NullValueHandling = NullValueHandling.Ignore)]
    public string ResourceName { get; set; }

    [JsonProperty("resource_id", NullValueHandling = NullValueHandling.Ignore)]
    public string ResourceId { get; set; }

    /// <summary>
    /// Event name, such as: startServer, shutDown, etc.
    /// </summary>
    [JsonProperty("trace_name", NullValueHandling = NullValueHandling.Ignore)]
    public string TraceName { get; set; }

    /// <summary>
    /// The event source type, such as ApiCall
    /// </summary>
    [JsonProperty("trace_type", NullValueHandling = NullValueHandling.Ignore)]
    public string TraceType { get; set; }

    /// <summary>
    /// The time when the cts service receives this trace (Epoch timestamp in milliseconds)
    /// </summary>
    [JsonProperty("record_time", NullValueHandling = NullValueHandling.Ignore)]
    public long RecordTime { get; set; }

    /// <summary>
    /// Unique identifier for the event
    /// </summary>
    [JsonProperty("trace_id", NullValueHandling = NullValueHandling.Ignore)]
    public string TraceId { get; set; }

    /// <summary>
    /// Status of the trace (e.g. normal) 
    /// </summary>
    [JsonProperty("trace_status", NullValueHandling = NullValueHandling.Ignore)]
    public string TraceStatus { get; set; }

    /// <summary>
    /// Trace rating (e.g., normal)
    /// </summary>
    [JsonProperty("trace_rating", NullValueHandling = NullValueHandling.Ignore)]
    public string TraceRating { get; set; }

    /// <summary>
    /// API version
    /// </summary>
    [JsonProperty("api_version", NullValueHandling = NullValueHandling.Ignore)]
    public string ApiVersion { get; set; }

    /// <summary>
    ///   Message 
    /// </summary>
    [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
    public string Message { get; set; }

    /// <summary>
    /// Source IP address 
    /// </summary>
    [JsonProperty("source_ip", NullValueHandling = NullValueHandling.Ignore)]
    public string SourceIp { get; set; }

    /// <summary>
    /// Domain ID
    /// </summary>
    [JsonProperty("domain_id", NullValueHandling = NullValueHandling.Ignore)]
    public string DomainId { get; set; }

    /// <summary>
    /// Project ID
    /// </summary>
    [JsonProperty("project_id", NullValueHandling = NullValueHandling.Ignore)]
    public string ProjectId { get; set; }

    /// <summary>
    /// Indicates whether the event is read-only
    /// </summary>
    [JsonProperty("read_only", NullValueHandling = NullValueHandling.Ignore)]
    public bool ReadOnly { get; set; }

    /// <summary>
    /// ID of the operation (ex. CreateFunction)
    /// </summary>
    [JsonProperty("operation_id", NullValueHandling = NullValueHandling.Ignore)]
    public string OperationId { get; set; }

    /// <summary>
    /// ID of the resource account
    /// </summary>
    [JsonProperty("resource_account_id", NullValueHandling = NullValueHandling.Ignore)]
    public string ResourceAccountId { get; set; }

    /// <summary>
    /// Event type (e.g. global)
    /// </summary>
    [JsonProperty("event_type", NullValueHandling = NullValueHandling.Ignore)]
    public string EventType { get; set; }

    // [JsonProperty("tracker_name", NullValueHandling = NullValueHandling.Ignore)]
    // public string TrackerName { get; set; }

    [JsonExtensionData]
    public IDictionary<string, JToken> _properties;
  }

  public class Domain
  {
    /// <summary>
    /// Domain name
    /// </summary>
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    /// <summary>
    /// Domain ID
    /// </summary>
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string Id { get; set; }
  }

  public class Request
  {
    [JsonExtensionData]
    public IDictionary<string, JToken> _parameters;
  }

  public class Response
  {
    [JsonExtensionData]
    public IDictionary<string, JToken> _parameters;
  }

  public class User
  {
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string Id { get; set; }

    [JsonProperty("domain", NullValueHandling = NullValueHandling.Ignore)]
    public Domain Domain { get; set; }
  }
}