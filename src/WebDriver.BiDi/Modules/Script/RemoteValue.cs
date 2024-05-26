using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Script;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(NumberValue), "number")]
[JsonDerivedType(typeof(StringValue), "string")]
[JsonDerivedType(typeof(NullValue), "null")]
[JsonDerivedType(typeof(UndefinedValue), "undefined")]
[JsonDerivedType(typeof(NodeRemoteValue), "node")]
public abstract class RemoteValue
{

}

public abstract class PrimitiveProtocolValue : RemoteValue
{

}

public class NumberValue : PrimitiveProtocolValue
{
    public long Value { get; set; }
}

public class StringValue : PrimitiveProtocolValue
{
    public string Value { get; set; }
}

public class NullValue : PrimitiveProtocolValue
{

}

public class UndefinedValue : PrimitiveProtocolValue
{

}

public class NodeRemoteValue : RemoteValue
{
    [JsonInclude]
    public string SharedId { get; internal set; }
}
