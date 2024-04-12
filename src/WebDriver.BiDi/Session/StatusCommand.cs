using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Session;

internal class StatusCommand : Command<EmptyCommandParameters>
{
    public override string Method { get; } = "session.status";
}

public class StatusResult
{
    [JsonInclude]
    public bool Ready { get; private set; }

    [JsonInclude]
    public string Message { get; private set; }
}