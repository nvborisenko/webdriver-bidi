using System.Collections.Generic;
using System.Text.Json.Serialization;
using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class AddInterceptCommand(AddInterceptParameters parameters) : Command<AddInterceptParameters>("network.addIntercept", parameters)
{

}

public class AddInterceptParameters : CommandParameters
{
    public List<InterceptPhase> Phases { get; set; } = [];

    public List<BrowsingContext.BrowsingContext>? Contexts { get; set; }

    public List<UrlPattern>? UrlPatterns { get; set; }
}

public class AddInterceptResult
{
    public Intercept Intercept { get; set; }
}

public enum InterceptPhase
{
    BeforeRequestSent,
    ResponseStarted,
    AuthRequired
}

[JsonDerivedType(typeof(UrlPatternPattern))]
[JsonDerivedType(typeof(UrlPatternString))]
public abstract class UrlPattern
{
    public abstract string Type { get; }

    public static UrlPatternPattern Patter(string? protocol = default, string? hostname = default, string? port = default, string? pathname = default, string? search = default)
        => new() { Protocol = protocol, Hostname = hostname, Port = port, Pathname = pathname, Search = search };

    public static UrlPatternString String(string pattern)
    {
        return new UrlPatternString { Pattern = pattern };
    }

    public static implicit operator UrlPattern(string value) { return new UrlPatternString { Pattern = value }; }
}

public class UrlPatternPattern : UrlPattern
{
    public override string Type => "pattern";

    public string? Protocol { get; set; }

    public string? Hostname { get; set; }

    public string? Port { get; set; }

    public string? Pathname { get; set; }

    public string? Search { get; set; }
}

public class UrlPatternString : UrlPattern
{
    public override string Type { get; } = "string";

    public string Pattern { get; set; }
}
