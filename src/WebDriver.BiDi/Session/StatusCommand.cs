namespace OpenQA.Selenium.BiDi.Session;

internal class StatusCommand : Command<EmptyCommandParameters>
{
    public override string Method { get; } = "session.status";
}

public class StatusResult
{
    public bool Ready { get; set; }

    public string Message { get; set; }
}