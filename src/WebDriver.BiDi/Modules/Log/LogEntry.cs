using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Log;

// https://github.com/dotnet/runtime/issues/72604
//[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
//[JsonDerivedType(typeof(ConsoleLogEntry), "console")]
//[JsonDerivedType(typeof(JavascriptLogEntry), "javascript")]
public abstract record BaseLogEntry(Level Level, Script.Source Source, string Text, DateTime Timestamp)
    : EventArgs;

public record ConsoleLogEntry(Level Level, Script.Source Source, string Text, DateTime Timestamp, string Method, IReadOnlyList<Script.RemoteValue> Args)
    : BaseLogEntry(Level, Source, Text, Timestamp);

public record JavascriptLogEntry(Level level, Script.Source source, string text, DateTime timestamp)
    : BaseLogEntry(level, source, text, timestamp);

public enum Level
{
    Debug,
    Info,
    Warn,
    Error
}
