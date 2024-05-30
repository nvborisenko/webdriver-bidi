using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Script;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(NumberRemoteValue), "number")]
[JsonDerivedType(typeof(StringRemoteValue), "string")]
[JsonDerivedType(typeof(NullRemoteValue), "null")]
[JsonDerivedType(typeof(UndefinedRemoteValue), "undefined")]
[JsonDerivedType(typeof(NodeRemoteValue), "node")]
public abstract class RemoteValue
{

}

public abstract class PrimitiveProtocolRemoteValue : RemoteValue
{

}

public class NumberRemoteValue : PrimitiveProtocolRemoteValue
{
    public long Value { get; set; }
}

public class StringRemoteValue : PrimitiveProtocolRemoteValue
{
    public string Value { get; set; }
}

public class NullRemoteValue : PrimitiveProtocolRemoteValue
{

}

public class UndefinedRemoteValue : PrimitiveProtocolRemoteValue
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