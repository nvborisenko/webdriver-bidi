using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class RemoveInterceptCommand : Command<RemoveInterceptParameters>
{
    public override string Method => "network.removeIntercept";
}

public class RemoveInterceptParameters : CommandParameters
{
    public Intercept Intercept { get; set; }
}
