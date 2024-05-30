using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class ContinueRequestCommand(ContinueRequestCommand.Parameters @params) 
    : Command<ContinueRequestCommand.Parameters>("network.continueRequest", @params)
{
    internal class Parameters : CommandParameters
    {
        public Request Request { get; set; }

        public string? Url { get; set; }

        public string? Method { get; set; }
    }
}
