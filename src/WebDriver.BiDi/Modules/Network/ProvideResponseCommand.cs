using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class ProvideResponseCommand(ProvideResponseCommand.Parameters @params) 
    : Command<ProvideResponseCommand.Parameters>("network.provideResponse", @params)
{
    internal class Parameters : CommandParameters
    {
        public Request Request { get; set; }

        public uint? StatusCode { get; set; }
    }
}
