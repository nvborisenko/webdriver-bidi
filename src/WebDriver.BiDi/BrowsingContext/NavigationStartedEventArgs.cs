using System;

namespace OpenQA.Selenium.BiDi.BrowsingContext;

public class NavigationStartedEventArgs : EventArgs
{
    public string Url { get; set; }

    public override string ToString()
    {
        return $"Url: {Url}";
    }
}
