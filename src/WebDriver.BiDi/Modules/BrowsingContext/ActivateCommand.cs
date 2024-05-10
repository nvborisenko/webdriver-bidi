using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class ActivateCommand : Command<ActivateParameters>
{
    public override string Method { get; } = "browsingContext.activate";
}

public class ActivateParameters : CommandParameters
{
    public BrowsingContext Context { get; set; }
}
