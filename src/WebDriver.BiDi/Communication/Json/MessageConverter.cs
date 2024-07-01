using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Communication.Json;

// https://github.com/dotnet/runtime/issues/72604
internal class MessageConverter : JsonConverter<Message>
{
    public override Message? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonDocument = JsonDocument.ParseValue(ref reader);

        switch (jsonDocument.RootElement.GetProperty("type").ToString())
        {
            case "success":
                var successId = jsonDocument.RootElement.GetProperty("id").GetInt32();
                JsonElement successResult = jsonDocument.RootElement.GetProperty("result");
                return new MessageSuccess { Id = successId, Result = successResult };

            case "error":
                var errorId = jsonDocument.RootElement.GetProperty("id").GetInt32();
                JsonElement errorResult = jsonDocument.RootElement.GetProperty("result");
                return new MessageError { Id = errorId, Error = jsonDocument.RootElement.GetProperty("error").GetString(), Message = jsonDocument.RootElement.GetProperty("message").GetString() };

            case "event":
                var method = jsonDocument.RootElement.GetProperty("method").GetString()!;
                var @params = jsonDocument.RootElement.GetProperty("params");
                return new MessageEvent() { Method = method, Params = @params };
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, Message value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
