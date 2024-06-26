﻿using OpenQA.Selenium.BiDi.Communication;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class ProvideResponseCommand(ProvideResponseCommandParameters @params) : Command<ProvideResponseCommandParameters>(@params);

internal class ProvideResponseCommandParameters(Request request) : CommandParameters
{
    public Request Request { get; } = request;

    public BytesValue? Body { get; set; }

    public IEnumerable<SetCookieHeader>? Cookies { get; set; }

    public IEnumerable<Header>? Headers { get; set; }

    public string? ReasonPhrase { get; set; }

    public uint? StatusCode { get; set; }
}
