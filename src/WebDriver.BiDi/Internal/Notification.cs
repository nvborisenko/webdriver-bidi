using System;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Internal;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(NotificationSuccess<object>), "success")]
[JsonDerivedType(typeof(NotificationError), "error")]
[JsonDerivedType(typeof(NotificationEvent), "event")]
internal abstract class Notification
{

}

internal class NotificationSuccess<T> : Notification
{
    public int Id { get; set; }

    public T Result { get; set; }
}

internal class NotificationError : Notification
{
    public int Id { get; set; }

    public string? Error { get; set; }

    public string? Message { get; set; }
}

internal class NotificationEvent : Notification
{
    public string Method { get; set; }

    public object Params { get; set; }
}