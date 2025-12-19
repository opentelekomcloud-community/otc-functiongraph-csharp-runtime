namespace OpenTelekomCloud.Serverless.Function.Events.LTS
{

  using System;
  using System.Collections.Generic;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

  /// <summary>
  /// You can write FunctionGraph functions to process logs subscribed to Cloud Log Service. 
  /// When Cloud Log Service collects subscribed logs, you can call FunctionGraph functions
  /// by passing the collected logs as parameters ( LTS sample events ). 
  /// FunctionGraph function code can be customized, analyzed, or loaded into other systems.
  /// <para>
  /// For the use of LTS log triggers, please refer to <see href="https://docs.otc.t-systems.com/function-graph/umn/creating_triggers/using_an_lts_trigger.html"/> 
  /// </para>
  /// </summary>
  public class LTSEvent
  {
    [JsonProperty("lts", NullValueHandling = NullValueHandling.Ignore)]
    public Lts Lts { get; set; }
    /// <summary>
    /// Returns a JSON string that represents the current object.
    /// </summary>
    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }

  public class Lts
  {
    /// <summary>
    /// Base64 encoded data
    /// </summary>
    [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
    public string Data { get; set; }
  }

}