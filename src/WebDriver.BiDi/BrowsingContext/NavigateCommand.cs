using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.BrowsingContext;

internal class NavigateCommand : Command<NavigateCommandParameters>
{
    public override string Method { get; } = "browsingContext.navigate";
}

internal class NavigateCommandParameters
{
    [JsonPropertyName("context")]
    public string? BrowsingContextId { get; set; }

    public string? Url { get; set; }

    public ReadinessState Wait { get; set; } = ReadinessState.Complete;
}

public enum ReadinessState
{
    None,
    Interactive,
    Complete
}
