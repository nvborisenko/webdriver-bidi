namespace OpenQA.Selenium.BiDi.Session;

internal class StatusCommand : Command<EmptyCommandParameters>
{
    public override string Name { get; } = "session.status";
}
