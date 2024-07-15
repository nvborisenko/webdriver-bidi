using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Network;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(BasicAuthCredentials), "password")]
public abstract record AuthCredentials;

public record BasicAuthCredentials(string Username, string Password) : AuthCredentials;
