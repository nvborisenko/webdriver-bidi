using OpenQA.Selenium.BiDi.Modules.Script;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Communication.Json;

internal class PreloadScriptConverter : JsonConverter<PreloadScript>
{
    private readonly Session _session;

    public PreloadScriptConverter(Session session)
    {
        _session = session;
    }

    public override PreloadScript? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var id = reader.GetString();

        return new PreloadScript(_session, id);
    }

    public override void Write(Utf8JsonWriter writer, PreloadScript value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Id);
    }
}
