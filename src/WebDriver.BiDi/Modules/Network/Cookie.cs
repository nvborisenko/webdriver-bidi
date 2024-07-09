using System;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public record Cookie(string Name, BytesValue Value, string Domain, string Path, uint Size, bool HttpOnly, bool Secure, SameSite SameSite)
{
    [JsonInclude]
    public DateTime? Expiry { get; internal set; }
}

public enum SameSite
{
    Strict,
    Lax,
    None
}
