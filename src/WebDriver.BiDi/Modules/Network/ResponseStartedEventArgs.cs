using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class ResponseStartedEventArgs(BrowsingContext.BrowsingContext context, bool isBlocked, Navigation navigation, uint redirectCount, RequestData request, DateTime timestamp, ResponseData response)
    : BaseParametersEventArgs(context, isBlocked, navigation, redirectCount, request, timestamp)
{
    public ResponseData Response { get; } = response;

    public Task ContinueAsync(uint? statusCode = default)
    {
        return Request.Request.ContinueResponseAsync(statusCode);
    }

    public Task ProvideAsync(uint? statusCoode = default)
    {
        return Request.Request.ProvideResponseAsync(statusCoode);
    }

    public override string ToString()
    {
        return $"Url: {Response.Url}, Protocol: {Response.Protocol}";
    }
}
