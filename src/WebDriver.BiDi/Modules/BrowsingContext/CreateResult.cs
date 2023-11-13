using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext
{
    public class CreateResult
    {
        [JsonPropertyName("context")]
        public string ContextId { get; set; } = null!;
    }
}
