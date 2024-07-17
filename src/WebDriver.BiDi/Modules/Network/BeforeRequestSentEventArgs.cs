using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public record BeforeRequestSentEventArgs(BiDi.Session Session, BrowsingContext.BrowsingContext Context, bool IsBlocked, Navigation Navigation, ulong RedirectCount, RequestData Request, DateTimeOffset Timestamp, Initiator Initiator)
    : BaseParametersEventArgs(Session, Context, IsBlocked, Navigation, RedirectCount, Request, Timestamp);
