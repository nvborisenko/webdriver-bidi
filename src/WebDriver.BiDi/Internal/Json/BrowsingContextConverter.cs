using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Internal.Json;

internal class BrowsingContextConverter : JsonConverter<BrowsingContext>
{
    private readonly Session _session;

    public BrowsingContextConverter(Session session)
    {
        _session = session;
    }

    public override BrowsingContext? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var id = reader.GetString();

        return new BrowsingContext(_session, id);
    }

    public override void Write(Utf8JsonWriter writer, BrowsingContext value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Id);
    }
}
