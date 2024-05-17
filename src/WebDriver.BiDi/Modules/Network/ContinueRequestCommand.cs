using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network
{
    internal class ContinueRequestCommand : Command<ContinueRequestParameters>
    {
        public override string Method { get; } = "network.continueRequest";
    }

    public class ContinueRequestParameters : CommandParameters
    {
        public Request Request { get; set; }

        public string? Url { get; set; }

        public string? Method { get; set; }
    }


}
