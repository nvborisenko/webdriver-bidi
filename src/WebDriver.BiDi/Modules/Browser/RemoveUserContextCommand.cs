using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Browser;

internal class RemoveUserContextCommand(RemoveUserContextCommandParameters @params)
    : Command<RemoveUserContextCommandParameters>("browser.removeUserContext", @params)
{
    
}

internal class RemoveUserContextCommandParameters(UserContext userContext) : CommandParameters
{
    public UserContext UserContext { get; } = userContext;
}