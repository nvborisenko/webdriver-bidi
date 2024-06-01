﻿using OpenQA.Selenium.BiDi.Internal;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class ProvideResponseCommand(ProvideResponseCommand.Parameters @params)
    : Command<ProvideResponseCommand.Parameters>("network.provideResponse", @params)
{
    internal class Parameters(Request request) : CommandParameters
    {
        public Request Request { get; } = request;

        public BytesValue? Body { get; set; }

        public IEnumerable<SetCookieHeader>? Coookies { get; set; }

        public IEnumerable<Header>? Headers { get; set; }

        public string? ReasonPhrase { get; set; }

        public uint? StatusCode { get; set; }
    }
}
