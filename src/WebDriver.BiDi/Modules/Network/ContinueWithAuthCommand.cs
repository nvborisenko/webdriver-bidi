using OpenQA.Selenium.BiDi.Communication;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class ContinueWithAuthCommand(ContinueWithAuthParameters @params) : Command<ContinueWithAuthParameters>(@params);

[JsonPolymorphic(TypeDiscriminatorPropertyName = "action")]
[JsonDerivedType(typeof(ContinueWithAuthCredentials), "provideCredentials")]
[JsonDerivedType(typeof(ContinueWithnullAuth), "null")]
[JsonDerivedType(typeof(ContinueWithCancelledAuth), "cancel")]
internal abstract record ContinueWithAuthParameters(Request Request) : CommandParameters;

internal record ContinueWithAuthCredentials(Request Request, AuthCredentials Credentials) : ContinueWithAuthParameters(Request);

internal record ContinueWithnullAuth(Request Request) : ContinueWithAuthParameters(Request);

internal record ContinueWithCancelledAuth(Request Request) : ContinueWithAuthParameters(Request);

public record ContinueWithAuthOptions : CommandOptions;

public record ContinueWithnullAuthOptions : CommandOptions;

public record ContinueWithCancelledAuthOptions : CommandOptions;
