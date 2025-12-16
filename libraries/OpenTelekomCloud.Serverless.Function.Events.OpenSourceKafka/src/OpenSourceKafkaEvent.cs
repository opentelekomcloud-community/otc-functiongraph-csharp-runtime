namespace OpenTelekomCloud.Serverless.Function.Events.OpenSourceKafka
{
  using System;
  using System.Collections.Generic;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

  public class OpenSourceKafkaEvent
  {

    /// <summary>
    /// Event version
    /// </summary>
    [JsonProperty("event_version", NullValueHandling = NullValueHandling.Ignore)]
    public string EventVersion { get; set; }

    /// <summary>
    /// Time when event occured (epoch)
    /// </summary>
    [JsonProperty("event_time", NullValueHandling = NullValueHandling.Ignore)]
    public int EventTime { get; set; }

    /// <summary>
    /// Trigger Type (Kafka)
    /// </summary>
    [JsonProperty("trigger_type", NullValueHandling = NullValueHandling.Ignore)]
    public string TriggerType { get; set; }

    /// <summary>
    /// Region
    /// </summary>
    [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
    public string Region { get; set; }

    /// <summary>
    /// Instance Id
    /// </summary>
    [JsonProperty("instance_id", NullValueHandling = NullValueHandling.Ignore)]
    public string InstanceId { get; set; }

    /// <summary>
    /// MessageRecords
    /// </summary>
    [JsonProperty("records", NullValueHandling = NullValueHandling.Ignore)]
    public List<KafkaRecord> Records { get; set; }

    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }

  public class KafkaRecord
  {
    /// <summary>
    /// Message body
    /// </summary>
    [JsonProperty("messages", NullValueHandling = NullValueHandling.Ignore)]
    public List<string> Messages { get; set; }

    /// <summary>
    /// Topic Id
    /// </summary>
    [JsonProperty("topic_id", NullValueHandling = NullValueHandling.Ignore)]
    public string TopicId { get; set; }
  }


}