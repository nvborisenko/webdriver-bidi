namespace OpenQA.Selenium.BiDi;

internal class MessageReceivedEventArgs(string message)
{
    public string Message { get; } = message;
}
