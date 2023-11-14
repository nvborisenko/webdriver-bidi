namespace OpenQA.Selenium.BiDi.Browser;

internal class CloseCommand : Command<EmptyCommandParameters>
{
    public override string Method { get; } = "browser.close";
}
