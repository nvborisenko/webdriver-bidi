using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class BrowsingContextInfo : EventArgs
{
    [JsonInclude]
    public IReadOnlyList<BrowsingContextInfo> Children { get; internal set; }

    [JsonInclude]
    public BrowsingContext Context { get; internal set; }

    [JsonInclude]
    public string Url { get; internal set; }

    [JsonInclude]
    public Browser.UserContext UserContext { get; internal set; }

    [JsonInclude]
    public BrowsingContext Parent { get; internal set; }
}
