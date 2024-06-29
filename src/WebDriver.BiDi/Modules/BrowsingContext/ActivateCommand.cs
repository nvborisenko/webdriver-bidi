using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class ActivateCommand(ActivateCommandParameters @params)
    : Command<ActivateCommandParameters>("browsingContext.activate", @params)
{

}

internal class ActivateCommandParameters(BrowsingContext context) : CommandParameters
{
    public BrowsingContext Context { get; } = context;
}
