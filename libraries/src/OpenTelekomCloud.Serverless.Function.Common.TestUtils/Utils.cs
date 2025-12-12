#if NET6_0_OR_GREATER
namespace OpenTelekomCloud.Serverless.Function.Common
#else
namespace HC.Serverless.Function.Common
#endif
{
  using System;
  using System.IO;
  public class Utils
  {
    /// <summary>
    /// Get the path to the current project, useful to access test resource files
    /// </summary>
    /// <returns></returns>
    public static string GetPathProjectFolder()
    {
      string pathAssembly = System.Reflection.Assembly.GetExecutingAssembly().Location;
      string folderAssembly = System.IO.Path.GetDirectoryName(pathAssembly);

      string folderProjectLevel = System.IO.Path.GetFullPath(folderAssembly);

      folderProjectLevel = Path.Combine(folderProjectLevel, "../../../../");
      return folderProjectLevel;
    }
  }
}