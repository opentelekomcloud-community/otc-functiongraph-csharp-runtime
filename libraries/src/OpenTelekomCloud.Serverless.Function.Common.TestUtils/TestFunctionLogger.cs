#if NET6_0_OR_GREATER
namespace OpenTelekomCloud.Serverless.Function.Common
#else
namespace HC.Serverless.Function.Common
#endif
{

  using System;

  /// <summary>
  /// Test Logger implementation
  /// </summary>
  public class TestFunctionLogger : IFunctionLogger
  {
    public void Log(string message)
    {
      Console.WriteLine(message);
    }

    public void Logf(string format, params object[] args)
    {
      Console.WriteLine(format, args);
    }
  }
}