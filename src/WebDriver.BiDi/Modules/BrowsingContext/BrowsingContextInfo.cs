using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class BrowsingContextInfo(IReadOnlyList<BrowsingContextInfo> children, BrowsingContext context, string url, Browser.UserContext userContext)
    : EventArgs
{
    public IReadOnlyList<BrowsingContextInfo> Children { get; } = children;

    public BrowsingContext Context { get; } = context;

    public string Url { get; } = url;

    public Browser.UserContext UserContext { get; } = userContext;

    [JsonInclude]
    public BrowsingContext? Parent { get; internal set; }
}
