using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class ReloadCommand(ReloadCommandParameters @params)
    : Command<ReloadCommandParameters>("browsingContext.reload", @params)
{

}

internal class ReloadCommandParameters(BrowsingContext context) : CommandParameters
{
    public BrowsingContext Context { get; } = context;

    public bool? IgnoreCache { get; set; }

    public ReadinessState? Wait { get; set; }
}
