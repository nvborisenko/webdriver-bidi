namespace OpenQA.Selenium.BiDi.Modules.Browser
{
    internal class CloseCommand : Command<EmptyCommandParameters>
    {
        public override string Name { get; } = "browser.close";
    }
}
