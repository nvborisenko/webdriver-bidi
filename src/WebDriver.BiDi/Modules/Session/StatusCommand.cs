using System.Text.Json.Serialization;
using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Session;

internal class StatusCommand : Command<CommandParameters>
{
    public override string Method { get; } = "session.status";
}

public class StatusResult
{
    [JsonInclude]
    public bool Ready { get; internal set; }

    [JsonInclude]
    public string Message { get; internal set; }
}