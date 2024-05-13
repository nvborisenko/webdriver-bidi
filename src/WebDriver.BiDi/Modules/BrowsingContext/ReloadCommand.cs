using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class ReloadCommand : Command<ReloadParameters>
{
    public override string Method => "browsingContext.reload";
}

public class ReloadParameters : CommandParameters
{
    public BrowsingContext Context { get; set; }

    public bool? IgnoreCache { get; set; }

    public ReadinessState? Wait { get; set; }
}
