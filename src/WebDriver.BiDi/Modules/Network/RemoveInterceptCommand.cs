using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class RemoveInterceptCommand(RemoveInterceptCommandParameters @params)
    : Command<RemoveInterceptCommandParameters>("network.removeIntercept", @params)
{
    
}

internal class RemoveInterceptCommandParameters(Intercept intercept) : CommandParameters
{
    public Intercept Intercept { get; } = intercept;
}
