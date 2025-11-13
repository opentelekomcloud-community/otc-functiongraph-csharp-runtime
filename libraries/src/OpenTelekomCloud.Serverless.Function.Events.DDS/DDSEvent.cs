namespace OpenTelekomCloud.Serverless.Function.Events.DDS
{
  using System;
  using System.Collections.Generic;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

  /// <summary>
  /// Using DDS triggers, each time a table in the database is updated, a Functiongraph function can be triggered to perform additional work. 
  /// <para>
  /// For more information about how to use DDS triggers, 
  /// see <see href="https://docs.otc.t-systems.com/function-graph/umn/creating_triggers/using_a_dds_trigger.html"/>.
  /// </para>
  /// </summary>
  public class DDSEvent
  {
    [JsonProperty("records", NullValueHandling = NullValueHandling.Ignore)]
    public List<DDSRecord> Records { get; set; }

    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }

  public class Age
  {
    [JsonProperty("$numberDouble", NullValueHandling = NullValueHandling.Ignore)]
    public string NumberDouble { get; set; }
  }

  public class Dds
  {
    /// <summary>
    /// The number of bytes in the message
    /// </summary>
    [JsonProperty("size_bytes", NullValueHandling = NullValueHandling.Ignore)]
    public string SizeBytes { get; set; }

    /// <summary>
    /// 	Base64 encoded data
    /// </summary>
    [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
    public string Token { get; set; } // TODO map to Token

    /// <summary>
    /// Complete file information
    /// </summary>
    [JsonProperty("full_document", NullValueHandling = NullValueHandling.Ignore)]
    public string FullDocument { get; set; } // TODO map to FullDocument

    /// <summary>
    /// Column Name
    /// </summary>
    [JsonProperty("ns", NullValueHandling = NullValueHandling.Ignore)]
    public string Ns { get; set; } // TODO map to NS
  }

  public class FullDocument
  {
    [JsonProperty("_id", NullValueHandling = NullValueHandling.Ignore)]
    public Id Id { get; set; }

    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }

    [JsonProperty("age", NullValueHandling = NullValueHandling.Ignore)]
    public Age Age { get; set; }
  }

  public class Id
  {
    [JsonProperty("$oid", NullValueHandling = NullValueHandling.Ignore)]
    public string Oid { get; set; }
  }

  public class Ns
  {
    [JsonProperty("db", NullValueHandling = NullValueHandling.Ignore)]
    public string Db { get; set; }

    [JsonProperty("coll", NullValueHandling = NullValueHandling.Ignore)]
    public string Coll { get; set; }
  }

  public class DDSRecord
  {
    /// <summary>
    /// Source of the event
    /// </summary>
    [JsonProperty("event_source", NullValueHandling = NullValueHandling.Ignore)]
    public string EventSource { get; set; }

    /// <summary>
    /// Event name
    /// </summary>
    [JsonProperty("event_name", NullValueHandling = NullValueHandling.Ignore)]
    public string EventName { get; set; }

    /// <summary>
    /// The region where the DDS instance is located
    /// </summary>
    [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
    public string Region { get; set; }

    /// <summary>
    /// Event protocol version
    /// </summary>
    [JsonProperty("event_version", NullValueHandling = NullValueHandling.Ignore)]
    public string EventVersion { get; set; }

    [JsonProperty("dds", NullValueHandling = NullValueHandling.Ignore)]
    public Dds Dds { get; set; }

    /// <summary>
    /// Event source unique identifier
    /// </summary>
    [JsonProperty("event_source_id", NullValueHandling = NullValueHandling.Ignore)]
    public string EventSourceId { get; set; }
  }
  public class Token
  {
    [JsonProperty("_data", NullValueHandling = NullValueHandling.Ignore)]
    public string Data { get; set; }
  }

}
