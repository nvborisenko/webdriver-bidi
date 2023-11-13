using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext
{
    internal class NavigateCommand : Command<NavigateCommandParameters>
    {
        public override string Name { get; } = "browsingContext.navigate";
    }

    internal class NavigateCommandParameters
    {
        [JsonPropertyName("context")]
        public string? BrowsingContextId { get; set; }

        public string? Url { get; set; }

        public string Wait { get; set; } = "complete";
    }
}
