using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

[JsonDerivedType(typeof(CssLocator))]
[JsonDerivedType(typeof(InnerTextLocator))]
[JsonDerivedType(typeof(XPathLocator))]
public abstract class Locator
{
    public abstract string Type { get; }

    public static CssLocator Css(string value)
        => new() { Value = value };

    public static InnerTextLocator InnerText(string value, bool? ignoreCase = default, MatchType? matchType = default, uint? maxDepth = default)
        => new() { Value = value, IgnoreCase = ignoreCase, MatchType = matchType, MaxDepth = maxDepth };

    public static XPathLocator XPath(string value)
        => new() { Value = value };
}

public class CssLocator : Locator
{
    public override string Type => "css";

    public string Value { get; set; }
}

public class InnerTextLocator : Locator
{
    public override string Type => "innerText";

    public string Value { get; set; }

    public bool? IgnoreCase { get; set; }

    public MatchType? MatchType { get; set; }

    public uint? MaxDepth { get; set; }
}

public enum MatchType
{
    Full,
    Partial
}

public class XPathLocator : Locator
{
    public override string Type => "xpath";

    public string Value { get; set; }
}