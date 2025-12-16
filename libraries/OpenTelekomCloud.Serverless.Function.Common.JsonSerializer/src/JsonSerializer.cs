namespace OpenTelekomCloud.Serverless.Function.Common
{
  using Newtonsoft.Json;
  using System;
  using System.IO;
  using System.Text;
  public class JsonSerializer : IFunctionSerializer
  {
    /// <summary>
    /// Deserialize from stream
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ins"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public T Deserialize<T>(Stream ins)
    {
      try
      {
        string str = "";
        if (ins != null && ins.Length > 0L)
        {
          byte[] numArray = new byte[ins.Length];
          ins.Read(numArray, 0, (int)ins.Length);
          str = Encoding.UTF8.GetString(numArray);
        }
        return JsonConvert.DeserializeObject<T>(str);
      }
      catch (Exception ex)
      {
        throw new NotImplementedException(ex.Message);
      }
    }

    /// <summary>
    /// Serialize to stream
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Stream Serialize<T>(T value)
    {
      try
      {
        return (Stream)new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object)value)));
      }
      catch (Exception ex)
      {
        throw new NotImplementedException(ex.Message);
      }
    }

  }
}
