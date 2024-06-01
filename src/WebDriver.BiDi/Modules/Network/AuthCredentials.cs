namespace OpenQA.Selenium.BiDi.Modules.Network;

public class AuthCredentials(string username, string password)
{
    public string Type => "password";
    public string Username { get; } = username;
    public string Password { get; } = password;
}
