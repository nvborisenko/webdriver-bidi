namespace OpenQA.Selenium.BiDi.Internal;

internal class MessageReceivedEventArgs(string message)
{
    public string Message { get; } = message;
}
