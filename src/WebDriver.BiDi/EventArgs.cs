namespace OpenQA.Selenium.BiDi;

public abstract class EventArgs : System.EventArgs
{
    public Session Session { get; internal set; }
}
