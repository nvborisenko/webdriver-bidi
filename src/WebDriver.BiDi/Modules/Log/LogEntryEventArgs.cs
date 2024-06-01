using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Log;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(ConsoleLogEntry), "console")]
[JsonDerivedType(typeof(JavascriptLogEntry), "javascript")]
public abstract class BaseLogEntryEventArgs(Level level, string text, DateTime timestamp) : EventArgs
{
    public Level Level { get; } = level;

    public string Text { get; } = text;

    public DateTime Timestamp { get; } = timestamp;
}

public class ConsoleLogEntry : BaseLogEntryEventArgs
{
    [JsonConstructor]
    internal ConsoleLogEntry(Level level, string text, DateTime timestamp, string method, IReadOnlyList<Script.RemoteValue> args)
        : base(level, text, timestamp)
    {
        Method = method;
        Args = args;
    }

    public string Method { get; }

    public IReadOnlyList<Script.RemoteValue> Args { get; }
}

public class JavascriptLogEntry : BaseLogEntryEventArgs
{
    [JsonConstructor]
    internal JavascriptLogEntry(Level level, string text, DateTime timestamp)
        : base(level, text, timestamp)
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
