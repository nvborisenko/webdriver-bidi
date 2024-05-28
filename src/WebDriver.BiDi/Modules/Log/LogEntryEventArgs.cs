using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Log;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(ConsoleLogEntry), "console")]
[JsonDerivedType(typeof(JavascriptLogEntry), "javascript")]
public abstract class LogEntryEventArgs : EventArgs
{
    [JsonInclude]
    public Level Level { get; internal set; }

    [JsonInclude]
    public string Text { get; internal set; }

    [JsonInclude]
    public DateTime Timestamp { get; internal set; }
}

public class ConsoleLogEntry : LogEntryEventArgs
{
    [JsonInclude]
    public string Method { get; internal set; }

    [JsonInclude]
    public IReadOnlyList<Script.RemoteValue> Args { get; internal set; }
}

public class JavascriptLogEntry : LogEntryEventArgs
{

}

public enum Level
{
    Debug,
    Info,
    Warn,
    Error
}
