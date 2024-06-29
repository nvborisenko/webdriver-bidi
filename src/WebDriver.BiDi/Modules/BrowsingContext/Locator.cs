using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(CssLocator), "css")]
[JsonDerivedType(typeof(InnerTextLocator), "innerText")]
[JsonDerivedType(typeof(XPathLocator), "xpath")]
public abstract class Locator
{
    public static CssLocator Css(string value)
        => new(value);

    public static InnerTextLocator InnerText(string value, bool? ignoreCase = default, MatchType? matchType = default, uint? maxDepth = default)
        => new(value) { IgnoreCase = ignoreCase, MatchType = matchType, MaxDepth = maxDepth };

    public static XPathLocator XPath(string value)
        => new(value);
}

public class CssLocator(string value) : Locator
{
    public string Value { get; } = value;
}

public class InnerTextLocator(string value) : Locator
{
    public string Value { get; } = value;

    public bool? IgnoreCase { get; set; }

    public MatchType? MatchType { get; set; }

    public uint? MaxDepth { get; set; }
}

public enum MatchType
{
    Full,
    Partial
}

public class XPathLocator(string value) : Locator
{
    public string Value { get; } = value;
}