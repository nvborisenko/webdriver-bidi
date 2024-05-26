using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class BrowsingContextInfoEventArgs : EventArgs
{
    [JsonInclude]
    public BrowsingContext Context { get; internal set; }

    [JsonInclude]
    public string Url { get; internal set; }
}
