using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class BaseParametersEventArgs : EventArgs
{
    [JsonInclude]
    public BrowsingContext.BrowsingContext Context { get; internal set; }

    [JsonInclude]
    public bool IsBlocked { get; internal set; }

    [JsonInclude]
    public Navigation Navigation { get; internal set; }

    [JsonInclude]
    public uint RedirectCount { get; internal set; }

    [JsonInclude]
    public RequestData Request { get; internal set; }

    [JsonInclude]
    public DateTime Timestamp { get; internal set; }

    [JsonInclude]
    public List<Intercept>? Intercepts { get; internal set; }
}

public class RequestData
{
    [JsonInclude]
    public Request Request { get; internal set; }

    [JsonInclude]
    public string Url { get; internal set; }

    [JsonInclude]
    public string Method { get; internal set; }

    public int? BodySize { get; set; }


}