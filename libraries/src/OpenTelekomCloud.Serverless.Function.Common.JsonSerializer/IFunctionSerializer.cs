namespace OpenTelekomCloud.Serverless.Function.Common
{
  using System.IO;
  public interface IFunctionSerializer
  {
    T Deserialize<T>(Stream ins);

    Stream Serialize<T>(T value);
  }
}
