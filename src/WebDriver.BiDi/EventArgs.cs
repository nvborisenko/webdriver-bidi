using OpenQA.Selenium.BiDi.Modules.BrowsingContext;

namespace OpenQA.Selenium.BiDi;

public abstract record EventArgs
{
    public Session Session { get; internal set; }
}

public abstract record BrowsingContextEventArgs(BrowsingContext Context) : EventArgs;