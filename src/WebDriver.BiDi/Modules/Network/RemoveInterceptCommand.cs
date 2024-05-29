using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class RemoveInterceptCommand(RemoveInterceptParameters parameters) : Command<RemoveInterceptParameters>("network.removeIntercept", parameters)
{

}

public class RemoveInterceptParameters : CommandParameters
{
    public Intercept Intercept { get; set; }
}
