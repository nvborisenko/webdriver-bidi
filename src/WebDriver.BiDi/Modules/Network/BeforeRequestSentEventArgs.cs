using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class BeforeRequestSentEventArgs : BaseParametersEventArgs
{
    internal BeforeRequestSentEventArgs(BrowsingContext.BrowsingContext context, bool isBlocked, Navigation navigation, uint redirectCount, RequestData request, DateTime timestamp, Initiator initiator)
        : base(context, isBlocked, navigation, redirectCount, request, timestamp)
    {
        Initiator = initiator;
    }

    public Initiator Initiator { get; }

    public override string ToString()
    {
        return $"{Request.Method} {Request.Url}";
    }

    public Task ContinueAsync(string? method = default)
    {
        return Request.Request.ContinueAsync(method);
    }

    public Task FailAsync()
    {
        return Request.Request.FailAsync();
    }

    public Task ProvideAsync(uint? statusCode = default)
    {
        return Request.Request.ProvideResponseAsync(statusCode);
    }
}
