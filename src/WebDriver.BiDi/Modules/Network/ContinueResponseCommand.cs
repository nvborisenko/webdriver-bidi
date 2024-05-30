using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class ContinueResponseCommand(ContinueResponseCommand.Parameters @params)
    : Command<ContinueResponseCommand.Parameters>("network.continueResponse", @params)
{
    internal class Parameters : CommandParameters
    {
        public Request Request { get; set; }

        public uint? StatusCode { get; set; }
    }
}
