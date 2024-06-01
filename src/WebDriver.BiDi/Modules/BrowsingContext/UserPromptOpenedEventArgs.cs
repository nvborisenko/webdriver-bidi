using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class UserPromptOpenedEventArgs(BrowsingContext context, UserPromptType type, string message) : EventArgs
{
    public BrowsingContext Context { get; } = context;

    public UserPromptType Type { get; } = type;

    public string Message { get; } = message;

    [JsonInclude]
    public string? DefaultValue { get; internal set; }
}

public enum UserPromptType
{
    Alert,
    Confirm,
    Prompt,
    BeforeUnload
}
