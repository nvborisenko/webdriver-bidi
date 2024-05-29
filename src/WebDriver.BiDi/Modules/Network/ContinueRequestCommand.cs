using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class ContinueRequestCommand(ContinueRequestParameters parameters) : Command<ContinueRequestParameters>("network.continueRequest", parameters)
{

}

public class ContinueRequestParameters : CommandParameters
{
    public Request Request { get; set; }

    public string? Url { get; set; }

    public string? Method { get; set; }
}
