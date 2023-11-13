using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Session
{
    public class StatusResult
    {
        [JsonPropertyName("ready")]
        public bool IsReady { get; set; }

        public string Message { get; set; }
    }
}
