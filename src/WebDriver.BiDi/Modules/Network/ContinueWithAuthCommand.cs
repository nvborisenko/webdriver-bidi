using OpenQA.Selenium.BiDi.Communication;
using OpenQA.Selenium.Internal;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class ContinueWithAuthCommand(ContinueWithAuthParameters @params) : Command<ContinueWithAuthParameters>(@params);

[JsonPolymorphic(TypeDiscriminatorPropertyName = "action")]
[JsonDerivedType(typeof(ContinueWithAuthCredentials), "provideCredentials")]
[JsonDerivedType(typeof(ContinueWithDefaultAuth), "default")]
[JsonDerivedType(typeof(ContinueWithCancelledAuth), "cancel")]
internal abstract record ContinueWithAuthParameters(Request Request) : CommandParameters;

internal record ContinueWithAuthCredentials(Request Request, AuthCredentials Credentials) : ContinueWithAuthParameters(Request);

internal record ContinueWithDefaultAuth(Request Request) : ContinueWithAuthParameters(Request);

internal record ContinueWithCancelledAuth(Request Request) : ContinueWithAuthParameters(Request);

public record ContinueWithAuthOptions : CommandOptions
{
    public static ContinueWithDefaultAuthOptions Default() => new();

    public static ContinueWithCancelledAuthOptions Cancel() => new();
}

public record ContinueWithDefaultAuthOptions : CommandOptions;

public record ContinueWithCancelledAuthOptions : CommandOptions;
