using System;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Script;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(NumberLocalValue), "number")]
[JsonDerivedType(typeof(StringLocalValue), "string")]
[JsonDerivedType(typeof(NullLocalValue), "null")]
[JsonDerivedType(typeof(UndefinedLocalValue), "undefined")]
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