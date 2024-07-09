using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public abstract record BaseParametersEventArgs(BrowsingContext.BrowsingContext Context, bool IsBlocked, BrowsingContext.Navigation Navigation, uint RedirectCount, RequestData Request, DateTime Timestamp)
    : BrowsingContextEventArgs(Context)
{
    [JsonInclude]
    public IReadOnlyList<Intercept>? Intercepts { get; internal set; }
}

