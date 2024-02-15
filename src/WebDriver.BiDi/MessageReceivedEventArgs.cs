namespace OpenQA.Selenium.BiDi;

public class MessageReceivedEventArgs(string message)
{
    public string Message { get; } = message;
}
