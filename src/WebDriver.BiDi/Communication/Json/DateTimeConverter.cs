﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Communication.Json;

internal class DateTimeConverter : JsonConverter<DateTime>
{
    static readonly DateTime s_epoch = new(1970, 1, 1, 0, 0, 0);
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var unixTime = reader.GetUInt64();

        return s_epoch.AddMilliseconds(unixTime);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue((long)(value - s_epoch).TotalMilliseconds);
    }
}
