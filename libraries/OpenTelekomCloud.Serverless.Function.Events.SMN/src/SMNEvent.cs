namespace OpenTelekomCloud.Serverless.Function.Events.SMN
{
  using System;
  using System.Collections.Generic;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

  /// <summary>
  /// Simple Message Notification (SMN) sends messages to email addresses, mobile phones, or HTTP/HTTPS URLs. 
  /// If you create a function with an SMN trigger, messages published to a specified topic will be passed as a parameter to invoke the function. 
  /// Then, the function processes the event, for example, publishing messages to other SMN topics or sending them to other cloud services. 
  /// <para>
  /// For details, see <see href="For details, see"/>
  /// </para>
  /// </summary>
  public class SMNEvent
  {
    /// <summary>
    /// Message records 
    /// </summary>
    [JsonProperty("record", NullValueHandling = NullValueHandling.Ignore)]
    public List<SMNRecord> Record { get; set; }

    /// <summary>
    /// Name of function
    /// </summary>
    [JsonProperty("functionname", NullValueHandling = NullValueHandling.Ignore)]
    public string Functionname { get; set; }


    [JsonProperty("requestId", NullValueHandling = NullValueHandling.Ignore)]
    public string RequestId { get; set; }

    [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
    public string Timestamp { get; set; }

    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }
  public class SMNRecord
  {
    /// <summary>
    /// Event version. (Currently, the version is 1.0.)
    /// </summary>
    [JsonProperty("event_version", NullValueHandling = NullValueHandling.Ignore)]
    public string EventVersion { get; set; }


/// <summary>
/// Message body
/// </summary>
    [JsonProperty("smn", NullValueHandling = NullValueHandling.Ignore)]
    public SMNBody Smn { get; set; }

    /// <summary>
    /// Subscription Uniform Resource Name (URN).
    /// </summary>
    [JsonProperty("event_subscription_urn", NullValueHandling = NullValueHandling.Ignore)]
    public string EventSubscriptionUrn { get; set; }

    /// <summary>
    /// Event source
    /// </summary>
    [JsonProperty("event_source", NullValueHandling = NullValueHandling.Ignore)]
    public string EventSource { get; set; }
  }

  public class SMNBody
  {
    /// <summary>
    /// ID of an SMN event
    /// </summary>
    [JsonProperty("topic_urn", NullValueHandling = NullValueHandling.Ignore)]
    public string TopicUrn { get; set; }

    /// <summary>
    /// time when event occured
    /// </summary>
    [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
    public DateTime Timestamp { get; set; }


    /// <summary>
    /// message attributes
    /// </summary>
    [JsonProperty("message_attributes", NullValueHandling = NullValueHandling.Ignore)]
    public object MessageAttributes { get; set; }

    /// <summary>
    /// message body
    /// </summary>
    [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
    public string Message { get; set; }

    /// <summary>
    /// Message format
    /// </summary>
    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
    public string Type { get; set; }

    /// <summary>
    /// Message ID. The ID of each message is unique.
    /// </summary>
    [JsonProperty("message_id", NullValueHandling = NullValueHandling.Ignore)]
    public string MessageId { get; set; }

    /// <summary>
    /// Subject of message
    /// </summary>
    [JsonProperty("subject", NullValueHandling = NullValueHandling.Ignore)]
    public string Subject { get; set; }
  }

}