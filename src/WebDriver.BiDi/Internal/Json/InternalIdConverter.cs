using OpenQA.Selenium.BiDi.Modules.Script;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Internal.Json;

internal class InternalIdConverter : JsonConverter<InternalId>
{
    private readonly Session _session;

    public InternalIdConverter(Session session)
    {
        _session = session;
    }

    public override InternalId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var id = reader.GetString();

        return new InternalId(_session, id);
    }

    public override void Write(Utf8JsonWriter writer, InternalId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Id);
    }
}
