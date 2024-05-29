using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class ReloadCommand(ReloadParameters parameters) : Command<ReloadParameters>("browsingContext.reload", parameters)
{

}

public class ReloadParameters : CommandParameters
{
    public BrowsingContext Context { get; set; }

    public bool? IgnoreCache { get; set; }

    public ReadinessState? Wait { get; set; }
}
