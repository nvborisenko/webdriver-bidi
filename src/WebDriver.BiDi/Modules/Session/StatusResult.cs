using Newtonsoft.Json;

namespace OpenQA.Selenium.BiDi.Modules.Session
{
    public class StatusResult
    {
        [JsonProperty("ready")]
        public bool IsReady { get; set; }

        public string Message { get; set; }
    }
}
