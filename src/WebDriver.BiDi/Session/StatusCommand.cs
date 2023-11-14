namespace OpenQA.Selenium.BiDi.Session;

internal class StatusCommand : Command<EmptyCommandParameters>
{
    public override string Method { get; } = "session.status";
}
