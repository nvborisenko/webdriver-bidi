using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Network;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(StringValue), "string")]
[JsonDerivedType(typeof(Base64Value), "base64")]
public abstract class BytesValue
{
    public static implicit operator BytesValue(string value) => new StringValue(value);
}

public class StringValue(string value) : BytesValue
{
    public string Value { get; } = value;
}

public class Base64Value(string value) : BytesValue
{
    public string Value { get; } = value;
}