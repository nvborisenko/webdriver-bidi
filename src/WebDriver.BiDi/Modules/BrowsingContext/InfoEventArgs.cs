using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class InfoEventArgs : EventArgs
{
    // TODO: Make it as shared reference to BrowserContext
    [JsonInclude]
    public string Context { get; private set; }

    [JsonInclude]
    public string Url { get; private set; }
}
