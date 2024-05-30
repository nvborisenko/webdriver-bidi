using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Input;

internal class ReleaseActionsCommand(ReleaseActionsCommand.Parameters @params)
    : Command<ReleaseActionsCommand.Parameters>("input.releaseActions", @params)
{
    internal class Parameters : CommandParameters
    {
        public string Context { get; set; }
    }
}
