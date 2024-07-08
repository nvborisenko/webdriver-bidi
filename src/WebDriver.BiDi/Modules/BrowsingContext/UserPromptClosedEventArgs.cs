using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class UserPromptClosedEventArgs(BrowsingContext context, bool accepted) 
    : BrowsingContextEventArgs(context)
{
    public bool Accepted { get; } = accepted;

    [JsonInclude]
    public string? UserText { get; internal set; }
}
