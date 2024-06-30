using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class ResponseStartedEventArgs : BaseParametersEventArgs
{
    [JsonConstructor]
    internal ResponseStartedEventArgs(BrowsingContext.BrowsingContext context, bool isBlocked, Navigation navigation, uint redirectCount, RequestData request, DateTime timestamp, ResponseData response)
        : base(context, isBlocked, navigation, redirectCount, request, timestamp)
    {
        Response = response;
    }

    public ResponseData Response { get; }

    public override string ToString()
    {
        return $"Url: {Response.Url}, Protocol: {Response.Protocol}";
    }
}
