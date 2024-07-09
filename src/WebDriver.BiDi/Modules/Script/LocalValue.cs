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
public abstract class LocalValue
{
    public static implicit operator LocalValue(int value) { return new NumberLocalValue(value); }
    public static implicit operator LocalValue(string value) { return new StringLocalValue(value); }
}

public abstract class PrimitiveProtocolLocalValue : LocalValue
{

}

public class NumberLocalValue(long value) : PrimitiveProtocolLocalValue
{
    public long Value { get; } = value;

    public static explicit operator NumberLocalValue(int n) => new NumberLocalValue(n);
}

public class StringLocalValue(string value) : PrimitiveProtocolLocalValue
{
    public string Value { get; } = value;
}

public class NullLocalValue : PrimitiveProtocolLocalValue
{

}

public class UndefinedLocalValue : PrimitiveProtocolLocalValue
{

}

public class ArrayLocalValue(IEnumerable<LocalValue> value) : LocalValue
{
    public IEnumerable<LocalValue> Value { get; } = value;
}

public class DateLocalValue(string value) : LocalValue
{
    public string Value { get; } = value;
}

public class MapLocalValue(IDictionary<string, LocalValue> value) : LocalValue // seems to implement IDictionary
{
    public IDictionary<string, LocalValue> Value { get; } = value;
}

public class ObjectLocalValue(IDictionary<string, LocalValue> value) : LocalValue
{
    public IDictionary<string, LocalValue> Value { get; } = value;
}

public class RegExpLocalValue(RegExpValue value) : LocalValue
{
    public RegExpValue Value { get; } = value;
}

public class RegExpValue(string pattern)
{
    public string Pattern { get; } = pattern;

    public string? Flags { get; set; }
}

public class SetLocalValue(IEnumerable<LocalValue> value) : LocalValue
{
    public IEnumerable<LocalValue> Value { get; } = value;
}