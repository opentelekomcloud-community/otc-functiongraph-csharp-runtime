#if NET6_0_OR_GREATER
namespace OpenTelekomCloud.Serverless.Function.Common
#else
namespace HC.Serverless.Function.Common
#endif
{
  using System;

  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Method, Inherited = false)]
  public sealed class FunctionSerializerAttribute : Attribute
  {
    public Type SerializerType { set; get; }

    public FunctionSerializerAttribute(Type serializerType) => this.SerializerType = serializerType;
  }
}
