using Newtonsoft.Json;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext
{
    internal class NavigateCommand : Command<NavigateCommandParameters>
    {
        public override string Name { get; } = "browsingContext.navigate";
    }

    internal class NavigateCommandParameters
    {
        [JsonProperty("context")]
        public string? BrowsingContextId { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }

        [JsonProperty("wait")]
        public string Wait { get; set; } = "complete";
    }
}
