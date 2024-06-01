using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class ResponseData(string url,
                          string protocol,
                          uint status,
                          string statusText,
                          bool fromCache,
                          IReadOnlyList<Header> headers,
                          string mymeType,
                          uint bytesReceived,
                          uint? headersSize,
                          uint? bodySize,
                          ResponseContent content)
{
    public string Url { get; } = url;
    public string Protocol { get; } = protocol;
    public uint Status { get; } = status;
    public string StatusText { get; } = statusText;
    public bool FromCache { get; } = fromCache;
    public IReadOnlyList<Header> Headers { get; } = headers;
    public string MymeType { get; } = mymeType;
    public uint BytesReceived { get; } = bytesReceived;
    public uint? HeadersSize { get; } = headersSize;
    public uint? BodySize { get; } = bodySize;
    public ResponseContent Content { get; } = content;

    [JsonInclude]
    public IReadOnlyList<AuthChallenge>? AuthChallenges { get; internal set; }
}
