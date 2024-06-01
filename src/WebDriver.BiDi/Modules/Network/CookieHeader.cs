namespace OpenQA.Selenium.BiDi.Modules.Network;

public class CookieHeader(string name, BytesValue value)
{
    public string Name { get; } = name;
    public BytesValue Value { get; } = value;
}
