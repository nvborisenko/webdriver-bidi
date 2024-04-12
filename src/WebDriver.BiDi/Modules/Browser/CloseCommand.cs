using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Browser;

internal class CloseCommand : Command<EmptyCommandParameters>
{
    public override string Method { get; } = "browser.close";
}
