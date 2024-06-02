using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Script;

public abstract class LocalValue
{
    public static implicit operator LocalValue(int value) { return new NumberLocalValue { Value = value }; }
    public static implicit operator LocalValue(string value) { return new StringLocalValue { Value = value }; }
}

public abstract class PrimitiveProtocolLocalValue : LocalValue
{
    public abstract string Type { get; }
}

public class NumberLocalValue : PrimitiveProtocolLocalValue
{
    public override string Type => "number";

    public long Value { get; set; }

    public static explicit operator NumberLocalValue(int n) => new NumberLocalValue { Value = n };
}

public class StringLocalValue : PrimitiveProtocolLocalValue
{
    public override string Type => "string";

    public string Value { get; set; }
}

public class NullLocalValue : PrimitiveProtocolLocalValue
{
    public override string Type => "null";
}

public class UndefinedLocalValue : PrimitiveProtocolLocalValue
{
    public override string Type => "undefined";
}

public class ArrayLocalValue : LocalValue
{
    public string Type => "array";

    public IEnumerable<LocalValue> Value { get; set; }
}

public class DateLocalValue : LocalValue
{
    public string Type => "date";

    public string Value { get; set; }
}

public class MapLocalValue : LocalValue // seems to implement IDictionary
{
    public string Type => "map";

    public IDictionary<string, LocalValue> Value { get; set; }
}

public class ObjectLocalValue : LocalValue
{
    public string Type => "object";

    public IDictionary<string, LocalValue> Value { get; set; }
}

public class RegExpLocalValue : LocalValue
{
    public string Type => "regexp";

    public RegExpValue Value { get; set; }
}

public class RegExpValue
{
    public string Pattern { get; set; }

    public string? Flags { get; set; }
}

public class SetLocalValue : LocalValue
{
    public string Type => "set";

    public IEnumerable<LocalValue> Value { get; set; }
}