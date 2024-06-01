using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Network;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(UrlPatternPattern), "pattern")]
[JsonDerivedType(typeof(UrlPatternString), "string")]
public abstract class UrlPattern
{
    public static UrlPatternPattern Patter(string? protocol = default, string? hostname = default, string? port = default, string? pathname = default, string? search = default)
        => new() { Protocol = protocol, Hostname = hostname, Port = port, Pathname = pathname, Search = search };

    public static UrlPatternString String(string pattern) => new UrlPatternString(pattern);

    public static implicit operator UrlPattern(string value) => new UrlPatternString(value);
}

public class UrlPatternPattern : UrlPattern
{
    public string? Protocol { get; set; }

    public string? Hostname { get; set; }

    public string? Port { get; set; }

    public string? Pathname { get; set; }

    public string? Search { get; set; }
}

public class UrlPatternString(string pattern) : UrlPattern
{
    public string Pattern { get; } = pattern;
}
