namespace OpenQA.Selenium.BiDi
{
    public class MessageReceivedEventArgs
    {
        public MessageReceivedEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
