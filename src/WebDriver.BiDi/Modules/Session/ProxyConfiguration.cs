using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Session;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "proxyType")]
[JsonDerivedType(typeof(AutodetectProxyConfiguration), "autodetect")]
[JsonDerivedType(typeof(DirectProxyConfiguration), "direct")]
[JsonDerivedType(typeof(ManualProxyConfiguration), "manual")]
[JsonDerivedType(typeof(PacProxyConfiguration), "pac")]
[JsonDerivedType(typeof(SystemProxyConfiguration), "system")]
public abstract class ProxyConfiguration
{
}

public class AutodetectProxyConfiguration : ProxyConfiguration
{

}

public class DirectProxyConfiguration : ProxyConfiguration
{

}

public class ManualProxyConfiguration : ProxyConfiguration
{
    public string? FtpProxy { get; set; }

    public string? HttpProxy { get; set; }

    public string? SslProxy { get; set; }

    public string? SocksProxy { get; set; }

    public uint? SocksVersion { get; set; }
}

public class PacProxyConfiguration(string proxyAutoconfigUrl) : ProxyConfiguration
{
    public string PproxyAutoconfigUrl { get; } = proxyAutoconfigUrl;
}

public class SystemProxyConfiguration : ProxyConfiguration
{

}