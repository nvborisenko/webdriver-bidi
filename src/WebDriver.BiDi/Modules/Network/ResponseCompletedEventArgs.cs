using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public record ResponseCompletedEventArgs(BiDi.Session Session, BrowsingContext.BrowsingContext Context, bool IsBlocked, Navigation Navigation, uint RedirectCount, RequestData Request, DateTime Timestamp, ResponseData Response) 
    : BaseParametersEventArgs(Session, Context, IsBlocked, Navigation, RedirectCount, Request, Timestamp);
