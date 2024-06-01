namespace OpenQA.Selenium.BiDi.Modules.Network;

public class AuthChallenge(string scheme, string realm)
{
    public string Scheme { get; } = scheme;
    public string Realm { get; } = realm;
}
