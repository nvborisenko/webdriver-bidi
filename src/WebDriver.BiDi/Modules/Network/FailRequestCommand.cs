using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class FailRequestCommand : Command<FailRequestParameters>
{
    public override string Method => "network.failRequest";
}

public class FailRequestParameters : CommandParameters
{
    public Request Request { get; set; }
}
