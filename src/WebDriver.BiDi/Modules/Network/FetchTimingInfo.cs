namespace OpenQA.Selenium.BiDi.Modules.Network;

public class FetchTimingInfo(double timeOrigin,
                             double requestTime,
                             double redirectStart,
                             double redirectEnd,
                             double fetchStart,
                             double dnsStart,
                             double dnsEnd,
                             double connectStart,
                             double connectEnd,
                             double tlsStart,
                             double requestStart,
                             double responseStart,
                             double responseEnd)
{
    public double TimeOrigin { get; } = timeOrigin;
    public double RequestTime { get; } = requestTime;
    public double RedirectStart { get; } = redirectStart;
    public double RedirectEnd { get; } = redirectEnd;
    public double FetchStart { get; } = fetchStart;
    public double DnsStart { get; } = dnsStart;
    public double DnsEnd { get; } = dnsEnd;
    public double ConnectStart { get; } = connectStart;
    public double ConnectEnd { get; } = connectEnd;
    public double TlsStart { get; } = tlsStart;
    public double RequestStart { get; } = requestStart;
    public double ResponseStart { get; } = responseStart;
    public double ResponseEnd { get; } = responseEnd;
}
