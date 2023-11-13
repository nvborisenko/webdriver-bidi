using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.BrowsingContext;

internal class NavigateCommand : Command<NavigateCommandParameters>
{
    public override string Name { get; } = "browsingContext.navigate";
}

internal class NavigateCommandParameters
{
    [JsonPropertyName("context")]
    public string? BrowsingContextId { get; set; }

    public string? Url { get; set; }

    public NavigateWait Wait { get; set; } = NavigateWait.Complete;
}

public enum NavigateWait
{
    None,
    Interactive,
    Complete
}
