using OpenQA.Selenium.BiDi.Modules.Script;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Communication.Json;

// https://github.com/dotnet/runtime/issues/72604
internal class EvaluateResultConverter : JsonConverter<EvaluateResult>
{
    public override EvaluateResult? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonDocument = JsonDocument.ParseValue(ref reader);

        switch (jsonDocument.RootElement.GetProperty("type").ToString())
        {
            case "success":
                JsonElement successResult = jsonDocument.RootElement.GetProperty("result");
                return new EvaluateResultSuccess(JsonSerializer.Deserialize<RemoteValue>(successResult, options)!);

            case "exception":
                var errorResult = jsonDocument.RootElement.GetProperty("result").GetInt32();
                return new EvaluateResultException { ExceptionDetails = JsonSerializer.Deserialize<ExceptionDetails>(errorResult, options)! };
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, EvaluateResult value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
