using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class RequestData(Request request, string url, string method, IReadOnlyList<Header> headers, IReadOnlyList<Cookie> cookies, uint headersSize, uint? bodySize, FetchTimingInfo timings)
{
    public Request Request { get; } = request;
    public string Url { get; } = url;
    public string Method { get; } = method;
    public IReadOnlyList<Header> Headers { get; } = headers;
    public IReadOnlyList<Cookie> Cookies { get; } = cookies;
    public uint HeadersSize { get; } = headersSize;
    public uint? BodySize { get; } = bodySize;
    public FetchTimingInfo Timings { get; } = timings;
}
