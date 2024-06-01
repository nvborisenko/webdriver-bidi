using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class BaseParametersEventArgs(BrowsingContext.BrowsingContext context, bool isBlocked, BrowsingContext.Navigation navigation, uint redirectCount, RequestData request, DateTime timestamp)
    : EventArgs
{
    public BrowsingContext.BrowsingContext Context { get; } = context;

    public bool IsBlocked { get; } = isBlocked;

    public BrowsingContext.Navigation Navigation { get; } = navigation;

    public uint RedirectCount { get; } = redirectCount;

    public RequestData Request { get; } = request;

    public DateTime Timestamp { get; } = timestamp;

    [JsonInclude]
    public List<Intercept>? Intercepts { get; internal set; }
}

