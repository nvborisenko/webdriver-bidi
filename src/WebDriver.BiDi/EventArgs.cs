using OpenQA.Selenium.BiDi.Modules.BrowsingContext;

namespace OpenQA.Selenium.BiDi;

public abstract class EventArgs : System.EventArgs
{
    public Session Session { get; internal set; }
}

public abstract class BrowsingContextEventArgs(BrowsingContext context) : EventArgs
{
    public BrowsingContext Context { get; } = context;
}