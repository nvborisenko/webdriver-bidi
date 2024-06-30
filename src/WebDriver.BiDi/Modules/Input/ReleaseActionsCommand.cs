using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Input;

internal class ReleaseActionsCommand(ReleaseActionsCommandParameters @params) : Command<ReleaseActionsCommandParameters>(@params);

internal class ReleaseActionsCommandParameters(BrowsingContext.BrowsingContext context) : CommandParameters
{
    public BrowsingContext.BrowsingContext Context { get; } = context;
}
