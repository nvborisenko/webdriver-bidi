using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class ResponseCompletedEventArgs : BaseParametersEventArgs
{
    internal ResponseCompletedEventArgs(BrowsingContext.BrowsingContext context, bool isBlocked, Navigation navigation, uint redirectCount, RequestData request, DateTime timestamp, ResponseData response)
        : base(context, isBlocked, navigation, redirectCount, request, timestamp)
    {
        Response = response;
    }

    public ResponseData Response { get; }
}
