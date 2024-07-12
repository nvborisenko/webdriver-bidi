using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public abstract record BaseParametersEventArgs(BiDi.Session Session, BrowsingContext.BrowsingContext Context, bool IsBlocked, BrowsingContext.Navigation Navigation, uint RedirectCount, RequestData Request, DateTime Timestamp)
    : BrowsingContextEventArgs(Session, Context)
{
    [JsonInclude]
    public IReadOnlyList<Intercept>? Intercepts { get; internal set; }
}

