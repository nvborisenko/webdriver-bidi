using OpenQA.Selenium.BiDi.Communication;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class ContinueResponseCommand(ContinueResponseCommandParameters @params) : Command<ContinueResponseCommandParameters>(@params);

internal class ContinueResponseCommandParameters(Request request) : CommandParameters
{
    public Request Request { get; } = request;

    public IEnumerable<SetCookieHeader>? Cookies { get; set; }

    public IEnumerable<AuthCredentials>? Credentials { get; set; }

    public IEnumerable<Header> Headers { get; set; }

    public string? ReasonPhrase { get; set; }

    public uint? StatusCode { get; set; }
}
