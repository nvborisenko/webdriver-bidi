using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.Browser;

internal class RemoveUserContextCommand(RemoveUserContextCommandParameters @params) : Command<RemoveUserContextCommandParameters>(@params);

internal class RemoveUserContextCommandParameters(UserContext userContext) : CommandParameters
{
    public UserContext UserContext { get; } = userContext;
}
