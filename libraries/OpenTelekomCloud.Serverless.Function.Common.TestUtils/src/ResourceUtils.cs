
namespace OpenTelekomCloud.Serverless.Function.Common.TestUtils
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Reflection;

  /// <summary>
  /// Utility methods to access embedded resources in test assemblies
  /// </summary>
  public static class ResourceUtils
  {
    /// <summary>
    /// Get embedded resource stream from assembly
    /// </summary>
    public static Stream GetResourceStream(this Assembly assembly, string resourceFolder, string resourceFileName)
    {

      string[] nameParts = assembly.FullName.Split(',');

      string resourceName = nameParts[0] + "." + resourceFolder + "." + resourceFileName;

      var resources = new List<string>(assembly.GetManifestResourceNames());
      if (resources.Contains(resourceName))
        return assembly.GetManifestResourceStream(resourceName);
      else
        return null;
    }

    /// <summary>
    /// Get embedded resource content as string from assembly
    /// </summary>  
    public static string GetResourceAsString(this Assembly assembly, string folder, string fileName)
    {
      string fileContent;
      using (StreamReader sr = new StreamReader(GetResourceStream(assembly, folder, fileName)))
      {
        fileContent = sr.ReadToEnd();
      }
      return fileContent;
    }

    /// <summary>
    /// Get embedded resource stream from type's assembly
    /// </summary>
    public static Stream GetResourceStream(this Type type, string resourceFolder, string resourceFileName)
    {
      var assembly = type.GetTypeInfo().Assembly;
      return assembly.GetResourceStream(resourceFolder, resourceFileName);
    }

    /// <summary>
    /// Get embedded resource content as string from type's assembly
    /// </summary>  
    public static string GetResourceAsString(this Type type, string folder, string fileName)
    {
      var assembly = type.GetTypeInfo().Assembly;
      return assembly.GetResourceAsString(folder, fileName);
    }
  }

  /// <summary>
  /// Exception for ResourceUtils errors
  /// </summary>
  public class ResourceUtilsException : Exception
  {
    public ResourceUtilsException(string message) : base(message) { }
  }
}