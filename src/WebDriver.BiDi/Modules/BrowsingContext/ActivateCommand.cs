using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class ActivateCommand : Command<ActivateParameters>
{
    public override string Method { get; } = "browsingContext.activate";
}

internal class ActivateParameters : EmptyCommandParameters
{
    public string Context { get; set; }
}
