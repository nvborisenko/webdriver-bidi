using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network
{
    internal class ContinueResponseCommand : Command<ContinueResponseParameters>
    {
        public override string Method { get; } = "network.continueResponse";
    }

    public class ContinueResponseParameters : CommandParameters
    {
        public Request Request { get; set; }

        public uint? StatusCode { get; set; }
    }


}
