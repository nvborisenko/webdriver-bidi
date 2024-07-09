using OpenQA.Selenium.BiDi.Modules.Script;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Communication.Json;

internal class HandleConverter : JsonConverter<Handle>
{
    private readonly Session _session;

    public HandleConverter(Session session)
    {
        _session = session;
    }

    public override Handle? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var id = reader.GetString();

        return new Handle(_session, id!);
    }

    public override void Write(Utf8JsonWriter writer, Handle value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Id);
    }
}
