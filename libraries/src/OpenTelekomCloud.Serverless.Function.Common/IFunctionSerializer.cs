#if NET6_0_OR_GREATER
namespace OpenTelekomCloud.Serverless.Function.Common
#else
namespace HC.Serverless.Function.Common
#endif
{
  using System.IO;
  public interface IFunctionSerializer
  {
    T Deserialize<T>(Stream ins);

    Stream Serialize<T>(T value);
  }
}
