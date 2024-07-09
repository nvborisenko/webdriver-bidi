using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public record UserPromptClosedEventArgs(BrowsingContext Context, bool Accepted)
    : BrowsingContextEventArgs(Context)
{
    [JsonInclude]
    public string? UserText { get; internal set; }
}
