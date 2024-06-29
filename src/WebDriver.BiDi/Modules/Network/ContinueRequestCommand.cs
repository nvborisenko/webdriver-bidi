using OpenQA.Selenium.BiDi.Internal;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class ContinueRequestCommand(ContinueRequestCommandParameters @params)
    : Command<ContinueRequestCommandParameters>("network.continueRequest", @params)
{
    
}

internal class ContinueRequestCommandParameters(Request request) : CommandParameters
{
    public Request Request { get; } = request;

    public BytesValue? Body { get; set; }

    public IEnumerable<CookieHeader>? Cookies { get; set; }

    public IEnumerable<Header>? Headers { get; set; }

    public string? Method { get; set; }

    public string? Url { get; set; }
}
