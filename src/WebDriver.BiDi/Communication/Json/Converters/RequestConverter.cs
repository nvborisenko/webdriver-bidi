using OpenQA.Selenium.BiDi.Modules.Network;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Communication.Json.Converters;

internal class RequestConverter : JsonConverter<Request>
{
    private readonly Session _session;

    public RequestConverter(Session session)
    {
        _session = session;
    }

    public override Request? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var id = reader.GetString();

        return new Request(_session, id!);
    }

    public override void Write(Utf8JsonWriter writer, Request value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Id);
    }
}
