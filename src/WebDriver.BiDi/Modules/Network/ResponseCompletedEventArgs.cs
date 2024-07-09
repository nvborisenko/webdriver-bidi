using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public record ResponseCompletedEventArgs(BrowsingContext.BrowsingContext Context, bool IsBlocked, Navigation Navigation, uint RedirectCount, RequestData Request, DateTime Timestamp, ResponseData Response) 
    : BaseParametersEventArgs(Context, IsBlocked, Navigation, RedirectCount, Request, Timestamp);
