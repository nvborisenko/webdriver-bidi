using OpenQA.Selenium.BiDi.Internal;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class ContinueResponseCommand(ContinueResponseCommand.Parameters @params)
    : Command<ContinueResponseCommand.Parameters>("network.continueResponse", @params)
{
    internal class Parameters(Request request) : CommandParameters
    {
        public Request Request { get; } = request;

        public IEnumerable<SetCookieHeader>? Cookies { get; set; }

        public IEnumerable<AuthCredentials>? Credentials { get; set; }

        public IEnumerable<Header> Headers { get; set; }

        public string? ReasonPhrase { get; set; }

        public uint? StatusCode { get; set; }
    }
}
