using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(CssLocator), "css")]
[JsonDerivedType(typeof(InnerTextLocator), "innerText")]
[JsonDerivedType(typeof(XPathLocator), "xpath")]
public abstract record Locator
{
    public static CssLocator Css(string value)
        => new(value);

    public static InnerTextLocator InnerText(string value, bool? ignoreCase = default, MatchType? matchType = default, uint? maxDepth = default)
        => new(value) { IgnoreCase = ignoreCase, MatchType = matchType, MaxDepth = maxDepth };

    public static XPathLocator XPath(string value)
        => new(value);
}

public record CssLocator(string Value) : Locator;

public record InnerTextLocator(string Value) : Locator
{
    public bool? IgnoreCase { get; set; }

    public MatchType? MatchType { get; set; }

    public uint? MaxDepth { get; set; }
}

public enum MatchType
{
    Full,
    Partial
}

public record XPathLocator(string Value) : Locator;