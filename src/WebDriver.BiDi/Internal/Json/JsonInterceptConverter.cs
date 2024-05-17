using OpenQA.Selenium.BiDi.Modules.Network;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Internal.Json;

internal class JsonInterceptConverter : JsonConverter<Intercept>
{
    private readonly Session _session;

    public JsonInterceptConverter(Session session)
    {
        _session = session;
    }

    public override Intercept? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var id = reader.GetString();

        return new Intercept(_session, id);
    }

    public override void Write(Utf8JsonWriter writer, Intercept value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Id);
    }
}
