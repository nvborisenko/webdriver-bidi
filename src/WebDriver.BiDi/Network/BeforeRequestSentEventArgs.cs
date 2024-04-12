using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Network;

public class BeforeRequestSentEventArgs : EventArgs
{
    [JsonInclude]
    public bool IsBlocked { get; private set; }

    [JsonInclude]
    public RequestData Request { get; private set; }
}

public class RequestData
{
    [JsonInclude, JsonPropertyName("request")]
    public string Id { get; private set; }

    [JsonInclude]
    public string Url { get; private set; }

    public int? BodySize { get; set; }
}
