using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class FailRequestCommand(FailRequestParameters parameters) : Command<FailRequestParameters>("network.failRequest", parameters)
{

}

public class FailRequestParameters : CommandParameters
{
    public Request Request { get; set; }
}
