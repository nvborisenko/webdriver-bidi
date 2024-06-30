using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class BeforeRequestSentEventArgs : BaseParametersEventArgs
{
    [JsonConstructor]
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
}
