using Newtonsoft.Json;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext
{
    public class CreateResult
    {
        [JsonProperty("context")]
        public string ContextId { get; set; } = null!;
    }
}
