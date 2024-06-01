using System;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class Cookie(string name, BytesValue value, string domain, string path, uint size, bool httpOnly, bool secure, SameSite sameSite)
{
    public string Name { get; } = name;

    public BytesValue Value { get; } = value;

    public string Domain { get; } = domain;

    public string Path { get; } = path;

    public uint Size { get; } = size;

    public bool HttpOnly { get; } = httpOnly;

    public bool Secure { get; } = secure;

    public SameSite SameSite { get; } = sameSite;

    [JsonInclude]
    public DateTime? Expiry { get; internal set; }
}

public enum SameSite
{
    Strict,
    Lax,
    None
}