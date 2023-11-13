using Newtonsoft.Json;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext
{
    internal class CloseCommand : Command<CloseCommandParameters>
    {
        public override string Name { get; } = "browsingContext.close";
    }

    internal class CloseCommandParameters
    {
        [JsonProperty("context")]
        public string? Context { get; set; }
    }
}
