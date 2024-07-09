using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Script;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(NumberLocalValue), "number")]
[JsonDerivedType(typeof(StringLocalValue), "string")]
[JsonDerivedType(typeof(NullLocalValue), "null")]
[JsonDerivedType(typeof(UndefinedLocalValue), "undefined")]
[JsonDerivedType(typeof(ArrayLocalValue), "array")]
[JsonDerivedType(typeof(DateLocalValue), "date")]
[JsonDerivedType(typeof(MapLocalValue), "map")]
[JsonDerivedType(typeof(ObjectLocalValue), "object")]
[JsonDerivedType(typeof(RegExpLocalValue), "regexp")]
[JsonDerivedType(typeof(SetLocalValue), "set")]
public abstract record LocalValue
{
    public static implicit operator LocalValue(int value) { return new NumberLocalValue(value); }
    public static implicit operator LocalValue(string value) { return new StringLocalValue(value); }
}

public abstract record PrimitiveProtocolLocalValue : LocalValue
{

}

public record NumberLocalValue(long Value) : PrimitiveProtocolLocalValue
{
    public static explicit operator NumberLocalValue(int n) => new NumberLocalValue(n);
}

public record StringLocalValue(string Value) : PrimitiveProtocolLocalValue;

public record NullLocalValue : PrimitiveProtocolLocalValue;

public record UndefinedLocalValue : PrimitiveProtocolLocalValue;

public record ArrayLocalValue(IEnumerable<LocalValue> Value) : LocalValue;

public record DateLocalValue(string Value) : LocalValue;

public record MapLocalValue(IDictionary<string, LocalValue> Value) : LocalValue; // seems to implement IDictionary

public record ObjectLocalValue(IDictionary<string, LocalValue> Value) : LocalValue;

public record RegExpLocalValue(RegExpValue Value) : LocalValue;

public record RegExpValue(string Pattern)
{
    public string? Flags { get; set; }
}

public record SetLocalValue(IEnumerable<LocalValue> Value) : LocalValue;
