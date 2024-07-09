using System;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public record NavigationInfo(BrowsingContext Context, Navigation Navigation, DateTime Timestamp, string Url)
    : BrowsingContextEventArgs(Context);
