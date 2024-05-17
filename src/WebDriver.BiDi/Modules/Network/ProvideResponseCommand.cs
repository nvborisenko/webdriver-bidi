using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class ProvideResponseCommand : Command<ProvideResponseParameters>
{
    public override string Method => "network.provideResponse";
}

public class ProvideResponseParameters : CommandParameters
{
    public Request Request { get; set; }

    public uint? StatusCode { get; set; }
}
