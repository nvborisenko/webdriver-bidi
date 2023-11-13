using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.BrowsingContext;

public class CreateResult
{
    [JsonPropertyName("context")]
    public string ContextId { get; set; } = null!;
}
