﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public record ResponseData(string Url,
                          string Protocol,
                          int Status, // TODO: should be unit
                          string StatusText,
                          bool FromCache,
                          IReadOnlyList<Header> Headers,
                          string MymeType,
                          uint BytesReceived,
                          uint? HeadersSize,
                          uint? BodySize,
                          ResponseContent Content)
{
    [JsonInclude]
    public IReadOnlyList<AuthChallenge>? AuthChallenges { get; internal set; }
}
