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
    public static implicit operator LocalValue(int value) { return new NumberLocalValue { Value = value }; }
    public static implicit operator LocalValue(string value) { return new StringLocalValue { Value = value }; }
}

public abstract class PrimitiveProtocolLocalValue : LocalValue
{

}

public class NumberLocalValue : PrimitiveProtocolLocalValue
{
    public long Value { get; set; }

    public static explicit operator NumberLocalValue(int n) => new NumberLocalValue { Value = n };
}

public class StringLocalValue : PrimitiveProtocolLocalValue
{
    public string Value { get; set; }
}

public class NullLocalValue : PrimitiveProtocolLocalValue
{

}

public class UndefinedLocalValue : PrimitiveProtocolLocalValue
{

}

public class ArrayLocalValue : LocalValue
{
    public IEnumerable<LocalValue> Value { get; set; }
}

public class DateLocalValue : LocalValue
{
    public string Value { get; set; }
}

public class MapLocalValue : LocalValue // seems to implement IDictionary
{
    public IDictionary<string, LocalValue> Value { get; set; }
}

public class ObjectLocalValue : LocalValue
{
    public IDictionary<string, LocalValue> Value { get; set; }
}

public class RegExpLocalValue : LocalValue
{
    public RegExpValue Value { get; set; }
}

public class RegExpValue
{
    public string Pattern { get; set; }

    public string? Flags { get; set; }
}

public class SetLocalValue : LocalValue
{
    public IEnumerable<LocalValue> Value { get; set; }
}