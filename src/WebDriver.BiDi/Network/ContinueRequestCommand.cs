using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Network
{
    internal class ContinueRequestCommand : Command<ContinueRequestParameters>
    {
        public override string Method { get; } = "network.continueRequest";
    }

    public class ContinueRequestParameters
    {
        public string Request { get; set; }

        public string? Url { get; set; }

        public string? Method { get; set; }
    }


}
