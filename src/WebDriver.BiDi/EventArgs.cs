using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi;

public abstract record EventArgs(Session Session)
{
    [JsonIgnore]
    public Session Session { get; internal set; } = Session;
}

public abstract record BrowsingContextEventArgs(Session Session, BrowsingContext Context)
    : EventArgs(Session);
