using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class ActivateCommand(ActivateCommand.Parameters @params)
    : Command<ActivateCommand.Parameters>("browsingContext.activate", @params)
{
    internal class Parameters : CommandParameters
    {
        public BrowsingContext Context { get; set; }
    }
}
