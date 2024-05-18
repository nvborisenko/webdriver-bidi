using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System.Threading.Tasks;
using System.Net.Http;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class BaseParametersEventArgs : EventArgs
{
    [JsonInclude]
    public BrowsingContext.BrowsingContext Context { get; private set; }

    [JsonInclude]
    public bool IsBlocked { get; private set; }

    [JsonInclude]
    public Navigation Navigation { get; private set; }

    [JsonInclude]
    public uint RedirectCount { get; private set; }

    [JsonInclude]
    public RequestData Request { get; private set; }

    [JsonInclude]
    public DateTime Timestamp { get; private set; }

    [JsonInclude]
    public List<Intercept>? Intercepts { get; private set; }
}

public class RequestData
{
    [JsonInclude]
    public Request Request { get; private set; }

    [JsonInclude]
    public string Url { get; private set; }

    [JsonInclude]
    public HttpMethod Method { get; private set; }

    public int? BodySize { get; set; }


}