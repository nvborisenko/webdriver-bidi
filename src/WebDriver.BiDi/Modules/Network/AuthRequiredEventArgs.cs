using System;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public record AuthRequiredEventArgs(BiDi.Session Session, BrowsingContext.BrowsingContext Context, bool IsBlocked, BrowsingContext.Navigation Navigation, uint RedirectCount, RequestData Request, DateTimeOffset Timestamp, ResponseData Response) :
    BaseParametersEventArgs(Session, Context, IsBlocked, Navigation, RedirectCount, Request, Timestamp);
