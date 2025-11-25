/// <summary>
/// Test Context implementation to be used in unit tests
/// </summary>
public class TestContext : OpenTelekomCloud.Serverless.Function.Common.IFunctionContext
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

  public OpenTelekomCloud.Serverless.Function.Common.IFunctionLogger Logger => new TestLogger();

  public string GetUserData(string key, string defaultValue = "")
  {
    if (key == "Key")
    {
      return "Value";
    }
    return defaultValue;
  }

}

/// <summary>
/// Test Logger implementation
/// </summary>
  public class TestLogger : OpenTelekomCloud.Serverless.Function.Common.IFunctionLogger
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