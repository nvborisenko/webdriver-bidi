using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Script;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(NumberRemoteValue), "number")]
[JsonDerivedType(typeof(StringRemoteValue), "string")]
[JsonDerivedType(typeof(NullRemoteValue), "null")]
[JsonDerivedType(typeof(UndefinedRemoteValue), "undefined")]
[JsonDerivedType(typeof(SymbolRemoteValue), "symbol")]
[JsonDerivedType(typeof(ObjectRemoteValue), "object")]
[JsonDerivedType(typeof(FunctionRemoteValue), "function")]
[JsonDerivedType(typeof(RegExpRemoteValue), "regexp")]
[JsonDerivedType(typeof(DateRemoteValue), "date")]
[JsonDerivedType(typeof(MapRemoteValue), "map")]
[JsonDerivedType(typeof(SetRemoteValue), "set")]
[JsonDerivedType(typeof(WeakMapRemoteValue), "weakmap")]
[JsonDerivedType(typeof(WeakSetRemoteValue), "weakset")]
[JsonDerivedType(typeof(GeneratorRemoteValue), "generator")]
[JsonDerivedType(typeof(ErrorRemoteValue), "error")]
[JsonDerivedType(typeof(PromiseRemoteValue), "proxy")]
[JsonDerivedType(typeof(PromiseRemoteValue), "promise")]
[JsonDerivedType(typeof(TypedArrayRemoteValue), "typedarray")]
[JsonDerivedType(typeof(ArrayBufferRemoteValue), "arraybuffer")]
[JsonDerivedType(typeof(NodeListRemoteValue), "nodelist")]
[JsonDerivedType(typeof(HtmlCollectionRemoteValue), "htmlcollection")]
[JsonDerivedType(typeof(NodeRemoteValue), "node")]
[JsonDerivedType(typeof(WindowProxyRemoteValue), "window")]
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

public class SymbolRemoteValue : RemoteValue
{
    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }
}

public class ArrayRemoteValue : RemoteValue
{
    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }

    public IReadOnlyList<RemoteValue>? Value { get; set; }
}

public class ObjectRemoteValue : RemoteValue
{
    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }

    public IDictionary<string, RemoteValue>? Value { get; set; }
}

public class FunctionRemoteValue : RemoteValue
{
    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }
}

public class RegExpRemoteValue : RemoteValue
{
    public RegExpValue Value { get; set; }

    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }
}

public class DateRemoteValue : RemoteValue
{
    public string Value { get; set; }

    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }
}

public class MapRemoteValue : RemoteValue
{
    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }

    public IDictionary<string, RemoteValue>? Value { get; set; }
}

public class SetRemoteValue : RemoteValue
{
    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }

    public IReadOnlyList<RemoteValue>? Value { get; set; }
}

public class WeakMapRemoteValue : RemoteValue
{
    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }
}

public class WeakSetRemoteValue : RemoteValue
{
    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }
}

public class GeneratorRemoteValue : RemoteValue
{
    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }
}

public class ErrorRemoteValue : RemoteValue
{
    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }
}

public class ProxyRemoteValue : RemoteValue
{
    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }
}

public class PromiseRemoteValue : RemoteValue
{
    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }
}

public class TypedArrayRemoteValue : RemoteValue
{
    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }
}

public class ArrayBufferRemoteValue : RemoteValue
{
    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }
}

public class NodeListRemoteValue : RemoteValue
{
    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }

    public IReadOnlyList<RemoteValue>? Value { get; set; }
}

public class HtmlCollectionRemoteValue : RemoteValue
{
    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }

    public IReadOnlyList<RemoteValue>? Value { get; set; }
}

public class NodeRemoteValue : RemoteValue
{
    [JsonInclude]
    public string? SharedId { get; internal set; }

    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }

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

public class WindowProxyRemoteValue : RemoteValue
{

    public Handle? Handle { get; set; }

    public InternalId? InternalId { get; set; }

    [JsonInclude]
    public WindowProxyProperties Value { get; internal set; }
}

public class WindowProxyProperties
{
    public BrowsingContext.BrowsingContext Context { get; set; }
}

public enum Mode
{
    Open,
    Closed
}