using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class CloseCommand(CloseCommand.Parameters @params)
    : Command<CloseCommand.Parameters>("browsingContext.close", @params)
{
    internal class Parameters(BrowsingContext context) : CommandParameters
    {
        public BrowsingContext Context { get; } = context;
    }
}
