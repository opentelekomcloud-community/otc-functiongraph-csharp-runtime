namespace src
{

  using System;
  public partial class Program
  {
    public static void Main(string[] args)
    {
      // Call FunctionGraph function using SDK signing with Access Key and Secret Key
      //callFunctionGraphAKSK();

      Console.WriteLine("Calling FunctionGraph function with Access Key and Secret Key...");
      callFunctionGraphUserNamePasswordAsync().GetAwaiter().GetResult();

      Console.WriteLine("###################################################################");
      // Call FunctionGraph function using User Name and Password

      Console.WriteLine("Calling FunctionGraph function with User Name and Password...");
      callFunctionGraphUserNamePasswordAsync().GetAwaiter().GetResult();
    }
  }
}