using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Communication;

// https://github.com/dotnet/runtime/issues/72604
//[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
//[JsonDerivedType(typeof(MessageSuccess), "success")]
//[JsonDerivedType(typeof(MessageError), "error")]
//[JsonDerivedType(typeof(MessageEvent), "event")]
internal abstract class Message
{

}

internal class MessageSuccess(int id, JsonElement result) : Message
{
    public int Id { get; } = id;

    public JsonElement Result { get; } = result;
}

internal class MessageError(int id) : Message
{
    public int Id { get; } = id;

    public string? Error { get; set; }

    public string? Message { get; set; }
}

internal class MessageEvent(string method, JsonElement @params) : Message
{
    public string Method { get; } = method;

    public JsonElement Params { get; } = @params;
}