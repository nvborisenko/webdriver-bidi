using OpenQA.Selenium.BiDi.Modules.Browser;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Communication.Json;

internal class BrowserUserContextConverter : JsonConverter<UserContext>
{
    private readonly Session _session;

    public BrowserUserContextConverter(Session session)
    {
        _session = session;
    }

    public override UserContext? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var id = reader.GetString();

        return new UserContext(_session, id);
    }

    public override void Write(Utf8JsonWriter writer, UserContext value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Id);
    }
}
