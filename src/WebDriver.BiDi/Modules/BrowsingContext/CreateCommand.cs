using Newtonsoft.Json;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext
{
    internal class CreateCommand : Command<CreateCommandParameters>
    {
        public override string Name { get; } = "browsingContext.create";
    }

    internal class CreateCommandParameters
    {
        [JsonProperty("type")]
        public string Type { get; } = "tab";
    }
}
