using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public record FetchErrorEventArgs(BiDi.Session Session, BrowsingContext.BrowsingContext Context, bool IsBlocked, Navigation Navigation, uint RedirectCount, RequestData Request, DateTime Timestamp, string ErrorText)
    : BaseParametersEventArgs(Session, Context, IsBlocked, Navigation, RedirectCount, Request, Timestamp);
