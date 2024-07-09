using OpenQA.Selenium.BiDi.Modules.Script;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Communication.Json;

internal class RealmConverter : JsonConverter<Realm>
{
    private readonly Session _session;

    public RealmConverter(Session session)
    {
        _session = session;
    }

    public override Realm? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var id = reader.GetString();

        return new Realm(_session, id!);
    }

    public override void Write(Utf8JsonWriter writer, Realm value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Id);
    }
}
