#if NET6_0_OR_GREATER
namespace OpenTelekomCloud.Serverless.Function.Common
#else
namespace HC.Serverless.Function.Common
#endif
{

  using System;

  /// <summary>
  /// Test Context implementation to be used in unit tests
  /// </summary>
#if NET6_0_OR_GREATER
  public class TestContextSimple : OpenTelekomCloud.Serverless.Function.Common.IFunctionContext
#else
  public class TestContextSimple : HC.Serverless.Function.Common.IFunctionContext
#endif
  {
    public string FunctionName => "TestFunction";

    public string FunctionVersion => "latest";

    public string RequestId => Guid.NewGuid().ToString();

    public string ProjectId => "TestProjectId";

    public string PackageName => "TestPackage";

    public int MemoryLimitInMb => 128;

    public int CpuNumber => 1;

    public string AccessKey => "FakeAccessKey";

    public string SecretKey => "FakeSecretKey";

    public string SecurityAccessKey => "FakeSecurityAccessKey";

    public string SecuritySecretKey => "FakeSecuritySecretKey";

    public string WorkflowID => "";

    public string WorkflowRunID => "";

    public string WorkflowStateID => "";

    public string Token => "FakeToken";

    public string SecurityToken => "FakeSecurityToken";

    public int RemainingTimeInMilliSeconds => 60000;

    public IFunctionLogger Logger => new TestFunctionLogger();

    public string GetUserData(string key, string defaultValue = "")
    {
      if (key == "Key")
      {
        return "Value";
      }
      return defaultValue;
    }

  }
}