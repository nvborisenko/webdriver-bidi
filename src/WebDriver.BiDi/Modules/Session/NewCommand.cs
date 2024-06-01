using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Session;

internal class NewCommand(NewCommand.Parameters @params)
    : Command<NewCommand.Parameters>("session.new", @params)
{
    internal class Parameters(CapabilitiesRequest capabilities) : CommandParameters
    {
        public CapabilitiesRequest Capabilities { get; } = capabilities;
    }
}

public class NewResult(string sessionId, Capability capability)
{
public string SessionId { get; } = sessionId;

public Capability Capability { get; } = capability;
}

public class Capability(bool acceptInsecureCerts, string browserName, string browserVersion, string platformName, bool setWindowRect, string userAgent)
{
public bool AcceptInsecureCerts { get; } = acceptInsecureCerts;

public string BrowserName { get; } = browserName;

public string BrowserVersion { get; } = browserVersion;

public string PlatformName { get; } = platformName;

public bool SetWindowRect { get; } = setWindowRect;

public string UserAgent { get; } = userAgent;

public ProxyConfiguration? Proxy { get; set; }

public string? WebSocketUrl { get; set; }
}