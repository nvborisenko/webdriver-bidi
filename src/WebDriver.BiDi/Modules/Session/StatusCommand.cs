namespace OpenQA.Selenium.BiDi.Modules.Session
{
    internal class StatusCommand : Command<EmptyCommandParameters>
    {
        public override string Name { get; } = "session.status";
    }
}
