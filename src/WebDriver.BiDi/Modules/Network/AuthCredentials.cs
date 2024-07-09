namespace OpenQA.Selenium.BiDi.Modules.Network;

public record AuthCredentials(string Username, string Password)
{
    public string Type => "password";
}
