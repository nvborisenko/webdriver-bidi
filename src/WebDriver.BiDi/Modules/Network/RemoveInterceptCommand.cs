using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class RemoveInterceptCommand(RemoveInterceptCommand.Parameters @params)
    : Command<RemoveInterceptCommand.Parameters>("network.removeIntercept", @params)
{
    internal class Parameters : CommandParameters
    {
        public Intercept Intercept { get; set; }
    }
}
