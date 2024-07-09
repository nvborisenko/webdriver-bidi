using OpenQA.Selenium.BiDi.Modules.Script;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Communication.Json;

internal class ChannelConverter : JsonConverter<Channel>
{
    private readonly Session _session;

    public ChannelConverter(Session session)
    {
        _session = session;
    }

    public override Channel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var id = reader.GetString();

        return new Channel(_session, id!);
    }

    public override void Write(Utf8JsonWriter writer, Channel value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Id);
    }
}
