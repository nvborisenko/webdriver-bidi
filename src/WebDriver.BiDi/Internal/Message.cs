using System;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Internal;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(MessageSuccess<object>), "success")]
[JsonDerivedType(typeof(MessageError), "error")]
[JsonDerivedType(typeof(MessageEvent<object>), "event")]
internal abstract class Message
{

}

internal class MessageSuccess<T> : Message
{
    public int Id { get; set; }

    public T Result { get; set; }
}

internal class MessageError : Message
{
    public int Id { get; set; }

    public string? Error { get; set; }

    public string? Message { get; set; }
}

internal class MessageEvent<T> : Message
{
    public string Method { get; set; }

    public T Params { get; set; }
}