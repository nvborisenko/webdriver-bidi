using System;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class NavigationInfoEventArgs : EventArgs
{
    [JsonInclude]
    public BrowsingContext Context { get; internal set; }

    [JsonInclude]
    public Navigation Navigation { get; internal set; }

    [JsonInclude]
    public DateTime Timestamp { get; internal set; }

    [JsonInclude]
    public string Url { get; internal set; }

    public override string ToString()
    {
        return $"Url: {Url}";
    }
}
