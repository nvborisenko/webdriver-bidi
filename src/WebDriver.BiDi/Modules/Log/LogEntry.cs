using System;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Log;

// https://github.com/dotnet/runtime/issues/72604
//[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
//[JsonDerivedType(typeof(ConsoleLogEntry), "console")]
//[JsonDerivedType(typeof(JavascriptLogEntry), "javascript")]
public abstract record BaseLogEntry(BiDi.Session Session, Level Level, Script.Source Source, string Text, DateTime Timestamp)
    : EventArgs(Session);

public record ConsoleLogEntry(BiDi.Session Session, Level Level, Script.Source Source, string Text, DateTime Timestamp, string Method, IReadOnlyList<Script.RemoteValue> Args)
    : BaseLogEntry(Session, Level, Source, Text, Timestamp);

public record JavascriptLogEntry(BiDi.Session Session, Level Level, Script.Source Source, string Text, DateTime Timestamp)
    : BaseLogEntry(Session, Level, Source, Text, Timestamp);

public enum Level
{
    Debug,
    Info,
    Warn,
    Error
}
