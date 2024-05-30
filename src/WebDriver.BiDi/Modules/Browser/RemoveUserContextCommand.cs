using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Browser;

internal class RemoveUserContextCommand(RemoveUserContextCommand.Parameters @params)
    : Command<RemoveUserContextCommand.Parameters>("browser.removeUserContext", @params)
{
    internal class Parameters(UserContext userContext) : CommandParameters
    {
        public UserContext UserContext { get; } = userContext;
    }
}
