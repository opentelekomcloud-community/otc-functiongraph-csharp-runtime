namespace OpenTelekomCloud.Serverless.Function.Common
{
  using System;

  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Method, Inherited = false)]
  public sealed class FunctionSerializerAttribute : Attribute
  {
    public Type SerializerType { set; get; }

    public FunctionSerializerAttribute(Type serializerType) => this.SerializerType = serializerType;
  }
}
