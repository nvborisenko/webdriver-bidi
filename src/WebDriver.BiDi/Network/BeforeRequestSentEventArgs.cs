using System;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Network;

public class BeforeRequestSentEventArgs : EventArgs
{
    public bool IsBlocked { get; set; }

    public Request Request { get; set; }
}

public class Request
{
    [JsonPropertyName("request")]
    public string Id { get; set; }

    public string Url { get; set; }
}
