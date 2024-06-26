﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Communication;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(MessageSuccess), "success")]
[JsonDerivedType(typeof(MessageError), "error")]
[JsonDerivedType(typeof(MessageEvent), "event")]
internal abstract class Message
{

}

internal class MessageSuccess : Message
{
    public int Id { get; set; }

    public JsonElement Result { get; set; }
}

internal class MessageError : Message
{
    public int Id { get; set; }

    public string? Error { get; set; }

    public string? Message { get; set; }
}

internal class MessageEvent : Message
{
    public string Method { get; set; }

    public JsonElement Params { get; set; }
}