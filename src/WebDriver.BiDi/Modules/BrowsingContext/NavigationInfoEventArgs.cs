using System.Text.Json.Serialization;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class NavigationInfoEventArgs : EventArgs
{
    [JsonInclude]
    public string Url { get; private set; }

    public override string ToString()
    {
        return $"Url: {Url}";
    }
}
