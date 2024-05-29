using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class ProvideResponseCommand(ProvideResponseParameters parameters) : Command<ProvideResponseParameters>("network.provideResponse", parameters)
{

}

public class ProvideResponseParameters : CommandParameters
{
    public Request Request { get; set; }

    public uint? StatusCode { get; set; }
}
