using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public record BrowsingContextInfo(IReadOnlyList<BrowsingContextInfo> Children, BrowsingContext Context, string Url, Browser.UserContext UserContext)
    : BrowsingContextEventArgs(Context)
{
    [JsonInclude]
    public BrowsingContext? Parent { get; internal set; }
}
