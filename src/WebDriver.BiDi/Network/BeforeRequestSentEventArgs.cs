using System;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Network;

public class BeforeRequestSentEventArgs : EventArgs
{
    public bool IsBlocked { get; set; }

    public RequestData Request { get; set; }
}

public class RequestData
{
    [JsonPropertyName("request")]
    public string Id { get; set; }

    public string Url { get; set; }
}
