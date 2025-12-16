#if NET6_0_OR_GREATER
namespace OpenTelekomCloud.Serverless.Function.Common
#else
namespace HC.Serverless.Function.Common
#endif
{

  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Test Context implementation to be used in unit tests
  /// </summary>
#if NET6_0_OR_GREATER
  public class TestContextSimple : OpenTelekomCloud.Serverless.Function.Common.IFunctionContext
#else
  public class TestContextSimple : HC.Serverless.Function.Common.IFunctionContext
#endif
  {

    public Dictionary<string, string> UserData { get; set; } = new Dictionary<string, string>();
    private DateTime _funcStartTime = DateTime.Now;

    public int TimeoutSeconds { get; set; } = 30;
    
    public string FunctionName { get; set; } = "TestFunction";

    public string FunctionVersion { get; set; } = "latest";

    public string RequestId => Guid.NewGuid().ToString();

    public string ProjectId { get; set; } = "TestProjectId";

    public string PackageName { get; set; } = "TestPackage";

    public int MemoryLimitInMb { get; set; } = 128;

    
    public int CpuNumber { get; set; } = 1;

    public string AccessKey { get; set; } = "FakeAccessKey";

    public string SecretKey { get; set; } = "FakeSecretKey";

    public string SecurityAccessKey { get; set; } = "FakeSecurityAccessKey";

    public string SecuritySecretKey { get; set; } = "FakeSecuritySecretKey";
    public string WorkflowID { get; set; } = "";

    public string WorkflowRunID { get; set; } = "";

    public string WorkflowStateID { get; set; } = "";

    public string Token { get; set; } = "FakeToken";

    public string SecurityToken { get; set; } = "FakeSecurityToken";
    public int RemainingTimeInMilliSeconds
    {
      get
      {
        return this.TimeoutSeconds * 1000 - Convert.ToInt32(DateTime.Now.Subtract(this._funcStartTime).TotalMilliseconds);
      }
    }

    public IFunctionLogger Logger
    {
      get
      {
        return new TestFunctionLogger()
        {
          RequestId = this.RequestId,
          InvokeId = Guid.NewGuid().ToString()
        };
      }
    }

    public string GetUserData(string key, string defaultValue = "")
    {
      return UserData == null || !UserData.ContainsKey(key) ? defaultValue : UserData[key];
    }

  }
}