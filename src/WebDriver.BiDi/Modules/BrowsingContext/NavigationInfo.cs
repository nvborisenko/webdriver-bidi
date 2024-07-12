using System;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public record NavigationInfo(BiDi.Session Session, BrowsingContext Context, Navigation Navigation, DateTime Timestamp, string Url)
    : BrowsingContextEventArgs(Session, Context);
