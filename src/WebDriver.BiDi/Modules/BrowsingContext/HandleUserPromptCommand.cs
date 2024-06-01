using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

class HandleUserPromptCommand(HandleUserPromptCommand.Parameters @params)
    : Command<HandleUserPromptCommand.Parameters>("browsingContext.handleUserPrompt", @params)
{
    internal class Parameters(BrowsingContext context) : CommandParameters
    {
        public BrowsingContext Context { get; } = context;

        public bool? Accept { get; set; }

        public string? UserText { get; set; }
    }
}
