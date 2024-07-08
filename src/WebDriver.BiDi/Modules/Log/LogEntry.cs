using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Log;

// https://github.com/dotnet/runtime/issues/72604
//[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
//[JsonDerivedType(typeof(ConsoleLogEntry), "console")]
//[JsonDerivedType(typeof(JavascriptLogEntry), "javascript")]
public abstract class BaseLogEntry(Level level, Script.Source source, string text, DateTime timestamp) : EventArgs
{
    public Level Level { get; } = level;

    public Script.Source Source { get; } = source;

    public string Text { get; } = text;

    public DateTime Timestamp { get; } = timestamp;
}

public class ConsoleLogEntry : BaseLogEntry
{
    [JsonConstructor]
    internal ConsoleLogEntry(Level level, Script.Source source, string text, DateTime timestamp, string method, IReadOnlyList<Script.RemoteValue> args)
        : base(level, source, text, timestamp)
    {
        Method = method;
        Args = args;
    }

    public string Method { get; }

    public IReadOnlyList<Script.RemoteValue> Args { get; }
}

public class JavascriptLogEntry : BaseLogEntry
{
    [JsonConstructor]
    internal JavascriptLogEntry(Level level, Script.Source source, string text, DateTime timestamp)
        : base(level, source, text, timestamp)
    {
    }
}

public enum Level
{
    Debug,
    Info,
    Warn,
    Error
}
