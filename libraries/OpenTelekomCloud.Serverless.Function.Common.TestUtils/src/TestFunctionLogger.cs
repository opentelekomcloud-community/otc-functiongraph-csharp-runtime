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
    public string RequestId { get; set; }

    public string InvokeId { get; set; }

    public void Log(string message)
    {
      Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff01")}] {this.RequestId}:{this.InvokeId} {message}");
    }

    public void Logf(string format, params object[] args) => this.Log(string.Format(format, args));

  }
}