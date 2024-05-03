using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class CloseCommand : Command<CloseCommandParameters>
{
    public override string Method { get; } = "browsingContext.close";
}

internal class CloseCommandParameters : EmptyCommandParameters
{
    public string Context { get; set; }
}
