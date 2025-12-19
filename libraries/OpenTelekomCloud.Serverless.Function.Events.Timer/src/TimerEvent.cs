namespace OpenTelekomCloud.Serverless.Function.Events.Timer
{
  using System;
  using System.Collections.Generic;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;
  /// <summary>
  /// You can schedule a timer to invoke your code based on a fixed rate of minutes, hours, or days or a cron expression.
  /// <para>
  /// For details, see <see href="https://docs.otc.t-systems.com/function-graph/umn/creating_triggers/using_a_timer_trigger.html"/>
  /// </para>
  /// </summary>
  public class TimerEvent
  {
    /// <summary>
    /// Event version
    /// </summary>
    [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
    public string Version { get; set; }

    /// <summary>
    /// Time when an event occurs. (e.g. 2018-06-01T08:30:00+08:00)
    /// </summary>
    [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
    public DateTime Time { get; set; }

    /// <summary>
    /// Trigger type
    /// </summary>
    [JsonProperty("trigger_type", NullValueHandling = NullValueHandling.Ignore)]
    public string TriggerType { get; set; }

    /// <summary>
    /// Trigger name
    /// </summary>
    [JsonProperty("trigger_name", NullValueHandling = NullValueHandling.Ignore)]
    public string TriggerName { get; set; }

    /// <summary>
    /// Additional information of the trigger
    /// </summary>
    [JsonProperty("user_event", NullValueHandling = NullValueHandling.Ignore)]
    public string UserEvent { get; set; }

    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }
}