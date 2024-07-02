using System;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class NavigationInfo(BrowsingContext context, Navigation navigation, DateTime timestamp, string url) : EventArgs
{
    public BrowsingContext Context { get; } = context;

    public Navigation Navigation { get; } = navigation;

    public DateTime Timestamp { get; } = timestamp;

    public string Url { get; } = url;

    public override string ToString()
    {
        return $"Url: {Url}";
    }
}
