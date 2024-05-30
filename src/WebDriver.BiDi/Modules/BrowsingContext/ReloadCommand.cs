using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class ReloadCommand(ReloadCommand.Parameters @params) 
    : Command<ReloadCommand.Parameters>("browsingContext.reload", @params)
{
    internal class Parameters : CommandParameters
    {
        public BrowsingContext Context { get; set; }

        public bool? IgnoreCache { get; set; }

        public ReadinessState? Wait { get; set; }
    }
}
