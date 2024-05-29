using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class ContinueResponseCommand(ContinueResponseParameters parameters) : Command<ContinueResponseParameters>("network.continueResponse", parameters)
{

}

public class ContinueResponseParameters : CommandParameters
{
    public Request Request { get; set; }

    public uint? StatusCode { get; set; }
}
