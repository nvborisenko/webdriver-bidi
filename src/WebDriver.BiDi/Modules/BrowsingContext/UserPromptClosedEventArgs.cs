using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public record UserPromptClosedEventArgs(BiDi.Session Session, BrowsingContext Context, bool Accepted)
    : BrowsingContextEventArgs(Session, Context)
{
    [JsonInclude]
    public string? UserText { get; internal set; }
}
