namespace OpenTelekomCloud.Serverless.Function.Common
{
  /// <summary>
  /// The Context interface allows functions to obtain the function execution context,
  /// such as the AccessKey/SecretKey delegated by the user, 
  /// the current request ID, 
  /// the memory space allocated for function execution, 
  /// the number of CPUs, and so on.
  /// </summary>
  public interface IFunctionContext
  {
    /// <summary>
    /// Gets the request ID associated with the request.
    /// <para>
    /// This is the same ID returned to the client that called invoke(). This ID
    /// is reused for retries on the same request.
    /// </para>
    /// </summary>
    string RequestId { get; }

    /// <summary>
    /// Project Id
    /// </summary>
    string ProjectId { get; }

    /// <summary>
    /// Package name of the function
    /// </summary>
    string PackageName { get; }

    /// <summary>
    /// Name of the function
    /// </summary>
    string FunctionName { get; }

    /// <summary>
    /// Function version
    /// </summary>
    string FunctionVersion { get; }

    /// <summary>
    /// Allocated memory.
    /// </summary>
    int MemoryLimitInMb { get; }

    /// <summary>
    /// CPU resources used by the function.
    /// </summary>
    int CpuNumber { get; }

    /// <summary>
    /// Obtains the AccessKey (valid for 24 hours) with an agency. If you use this method, you need to configure an agency for the function.
    /// 
    /// <para>
    /// NOTE:
    /// FunctionGraph has stopped maintaining the getAccessKey API in the Runtime SDK. You cannot use this API to obtain a temporary AK.
    /// </para>
    /// </summary>
    string AccessKey { get; }

    /// <summary>
    /// Obtains the SecretKey (valid for 24 hours) with an agency. If you use this method, you need to configure an agency for the function.
    /// 
    /// <para>
    /// NOTE:
    /// FunctionGraph has stopped maintaining the getAccessKey API in the Runtime SDK. You cannot use this API to obtain a temporary AK.
    /// </para>
    /// </summary>
    string SecretKey { get; }

    /// <summary>
    /// Obtains the SecurityAccessKey (valid for 24 hours) with an agency. 
    /// <para>If you use this method, you need to configure an agency for the function.</para>
    /// </summary>
    string SecurityAccessKey { get; }

    /// <summary>
    /// Obtains the SecuritySecretKey (valid for 24 hours) with an agency. 
    /// <para>If you use this method, you need to configure an agency for the function.</para>
    /// </summary>
    string SecuritySecretKey { get; }

    /// <summary>
    /// //TODO
    /// </summary>
    string WorkflowID { get; }

    /// <summary>
    /// //TODO
    /// </summary>
    string WorkflowRunID { get; }

    /// <summary>
    /// //TODO
    /// </summary>
    string WorkflowStateID { get; }

    /// <summary>
    /// Obtains the Token (valid for 24 hours) with an agency. 
    /// <para>If you use this method, you need to configure an agency for the function.</para>
    /// </summary>
    string Token { get; }

    /// <summary>
    /// Obtains the SecurityToken (valid for 24 hours) with an agency. 
    /// <para>If you use this method, you need to configure an agency for the function.</para>
    /// </summary>
    ///  /// NOT AVAILABLE in DLL 
    string SecurityToken { get; }

    /// <summary>
    /// get environment variable
    /// </summary>
    /// <param name="key">name of environment variable to get</param>
    /// <param name="defvalue">default value</param>
    /// <returns>value of environment variable</returns>
    string GetUserData(string key, string defvalue = "");

    /// <summary>
    /// The remaining running time of the function
    /// </summary>
    int RemainingTimeInMilliSeconds { get; }

    /// <summary>
    /// Obtains the logger method provided by the context. 
    /// By default, information such as the time and request ID is output.
    /// </summary>
    IFunctionLogger Logger { get; }

  }
}
