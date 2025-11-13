namespace OpenTelekomCloud.Serverless.Function.Events.DMS4Kafka
{
  using System;
  using System.Collections.Generic;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;


  /// <summary>
  /// DMS for Kafka is a message queuing service that provides Kafka premium instances. If you create a Kafka trigger for a function,
  /// when a message is sent to a Kafka instance topic, FunctionGraph will retrieve the message and trigger the function to perform other operations.
  /// <para>
  /// For details, see: <see href="https://docs.otc.t-systems.com/function-graph/umn/creating_triggers/using_a_kafka_trigger.html"/>
  /// </para>
  /// </summary>
  public class DMS4KafkaEvent
  {
    /// <summary>
    /// Event version
    /// </summary>
    [JsonProperty("event_version", NullValueHandling = NullValueHandling.Ignore)]
    public string EventVersion { get; set; }

    /// <summary>
    /// Time when an event occurs
    /// </summary>
    [JsonProperty("event_time", NullValueHandling = NullValueHandling.Ignore)]
    public int EventTime { get; set; }

    /// <summary>
    /// Event type
    /// </summary>
    [JsonProperty("trigger_type", NullValueHandling = NullValueHandling.Ignore)]
    public string TriggerType { get; set; }

    /// <summary>
    /// Region where a Kafka instance resides
    /// </summary>
    [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
    public string Region { get; set; }

    /// <summary>
    /// Kafka instance ID
    /// </summary>
    [JsonProperty("instance_id", NullValueHandling = NullValueHandling.Ignore)]
    public string InstanceId { get; set; }

    [JsonProperty("records", NullValueHandling = NullValueHandling.Ignore)]
    public List<DMS4KafkaRecord> Records { get; set; }
    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }

  public class DMS4KafkaRecord
  {
    /// <summary>
    ///	Message content
    /// </summary>
    [JsonProperty("messages", NullValueHandling = NullValueHandling.Ignore)]
    public List<string> Messages { get; set; }

    /// <summary>
    /// 	Message ID
    /// </summary>
    [JsonProperty("topic_id", NullValueHandling = NullValueHandling.Ignore)]
    public string TopicId { get; set; }
  }

}