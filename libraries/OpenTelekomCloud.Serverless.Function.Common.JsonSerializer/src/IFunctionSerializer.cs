namespace OpenTelekomCloud.Serverless.Function.Common
{
  using System.IO;
  /// <summary>
  /// Function Serializer Interface
  /// </summary>
  public interface IFunctionSerializer
  {

    /// <summary>
    /// Deserialize from stream 
    /// </summary>
    T Deserialize<T>(Stream ins);

    /// <summary>
    ///   Serialize to stream
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    Stream Serialize<T>(T value);
  }
}
