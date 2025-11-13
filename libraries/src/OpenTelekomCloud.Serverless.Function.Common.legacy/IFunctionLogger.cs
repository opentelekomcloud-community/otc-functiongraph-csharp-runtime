namespace HC.Serverless.Function.Common
{
  public interface IFunctionLogger
  {
    void Log(string message);

    /// <summary>
    /// Records user input logs
    /// This method will output the content to the standard output in the format of "time-request ID-output content". 
    /// <example>
    /// For example:
    /// <code>
    /// 2017-10-25T09:10:03.328Z 473d369d-101a-445e-a7a8-315cca788f86 test log output
    /// </code>
    /// Usage of format:
    /// <code>
    /// var logger = context.Logger;
    /// logger.Logf("Hello CSharp runtime test({0})", version);
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="format">format</param>
    /// <param name="args">arguments</param>
    void Logf(string format, params object[] args);
  }
}
