using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class CloseCommand(CloseCommandParameters @params) : Command<CloseCommandParameters>(@params);

internal class CloseCommandParameters(BrowsingContext context) : CommandParameters
{
    public BrowsingContext Context { get; } = context;
}
