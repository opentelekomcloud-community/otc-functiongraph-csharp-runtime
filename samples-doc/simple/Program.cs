namespace src
{
  
#if NET6_0_OR_GREATER
  using OpenTelekomCloud.Serverless.Function.Common;
#else
  using HC.Serverless.Function.Common;
#endif

  using System;
  using System.IO;
  using System.Text;

  class Program
  {

    public Stream Handler(Stream input, IFunctionContext context)
    {
      string payload = "";
      if (input != null && input.Length > 0)
      {
        byte[] buffer = new byte[input.Length];
        input.Read(buffer, 0, (int)(input.Length));
        payload = Encoding.UTF8.GetString(buffer);
      }
      var ms = new MemoryStream();
      using (var sw = new StreamWriter(ms))
      {
        sw.WriteLine("CSharp runtime test(v1.0.3)");
        sw.WriteLine("=====================================");
        sw.WriteLine("Request Id:       {0}", context.RequestId);
        sw.WriteLine("Function Name:    {0}", context.FunctionName);
        sw.WriteLine("Function Version: {0}", context.FunctionVersion);
        sw.WriteLine("Project:          {0}", context.ProjectId);
        sw.WriteLine("Package:          {0}", context.PackageName);
        /*
        sw.WriteLine("Security Access Key:       {0}", context.SecurityAccessKey);
        sw.WriteLine("Security Secret Key:       {0}", context.SecuritySecretKey);
        sw.WriteLine("Security Token:            {0}", context.SecurityToken);
        */
        sw.WriteLine("Token:            {0}", context.Token);
        sw.WriteLine("User data(ud-a):  {0}", context.GetUserData("ud-a"));
        sw.WriteLine("User data(ud-notexist): {0}", context.GetUserData("ud-notexist", ""));
        sw.WriteLine("User data(ud-notexist-default): {0}", context.GetUserData("ud-notexist", "default value"));
        sw.WriteLine("=====================================");

        var logger = context.Logger;
        logger.Logf("Hello CSharp runtime test(v1.0.2)");
        sw.WriteLine(payload);
      }
      return new MemoryStream(ms.ToArray());
    }
  }
}
