namespace OpenQA.Selenium.BiDi.Modules.Network;

public class SetCookieHeader(string name, BytesValue value)
{
    public string Name { get; } = name;

    public BytesValue Value { get; } = value;

    public string? Domain { get; set; }

    public bool? HttpOnly { get; set; }

    public string? Expiry { get; set; }

    public int? MaxAge { get; set; }

    public string? Path { get; set; }

    public SameSite? SameSite { get; set; }

    public bool? Secure { get; set; }
}
