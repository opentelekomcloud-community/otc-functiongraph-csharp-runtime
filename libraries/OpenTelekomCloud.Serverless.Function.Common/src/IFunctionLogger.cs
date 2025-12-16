#if NET6_0_OR_GREATER
namespace OpenTelekomCloud.Serverless.Function.Common
#else
namespace HC.Serverless.Function.Common
#endif
{
  public interface IFunctionLogger
  {
    /// <summary>
    /// Records user input logs 
    /// </summary>
    /// <param name="message"></param>
    void Log(string message);

    /// <summary>
    /// Records user input logs
    /// <example>
    /// Usage of format:
    /// <code>
    /// Logger.Logf("hello CSharp runtime test({0})", version);
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="format">format</param>
    /// <param name="args">arguments</param>
    void Logf(string format, params object[] args);
  }
}
