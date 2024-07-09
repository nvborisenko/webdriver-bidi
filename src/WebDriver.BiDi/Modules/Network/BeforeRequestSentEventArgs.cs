using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public record BeforeRequestSentEventArgs(BrowsingContext.BrowsingContext Context, bool IsBlocked, Navigation Navigation, uint RedirectCount, RequestData Request, DateTime Timestamp, Initiator Initiator)
    : BaseParametersEventArgs(Context, IsBlocked, Navigation, RedirectCount, Request, Timestamp);
