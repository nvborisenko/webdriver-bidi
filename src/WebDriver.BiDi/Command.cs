using Newtonsoft.Json;

namespace OpenQA.Selenium.BiDi
{
    public abstract class Command
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public abstract class Command<TParameters> : Command
        where TParameters : new()
    {
        [JsonProperty("method")]
        public abstract string Name { get; }

        [JsonProperty("params")]
        public TParameters Parameters { get; set; } = new TParameters();
    }
}
