using System.Collections.Generic;
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

    [JsonInclude]
    public NodeProperties? Value { get; internal set; }
}

public class NodeProperties
{
    [JsonInclude]
    public uint NodeType { get; internal set; }

    [JsonInclude]
    public uint ChildNodeCount { get; internal set; }

    [JsonInclude]
    public IReadOnlyDictionary<string, string>? Attributes { get; internal set; }

    [JsonInclude]
    public IReadOnlyList<NodeRemoteValue>? Children { get; internal set; }

    [JsonInclude]
    public string? LocalName { get; internal set; }

    [JsonInclude]
    public Mode? Mode { get; internal set; }

    [JsonInclude]
    public string? NamespaceUri { get; internal set; }

    [JsonInclude]
    public string? NodeValue { get; internal set; }

    [JsonInclude]
    public NodeRemoteValue? ShadowRoot { get; internal set; }
}

public enum Mode
{
    Open,
    Closed
}