using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class NavigateCommand(NavigateCommandParameters @params) : Command<NavigateCommandParameters>(@params);

internal class NavigateCommandParameters(BrowsingContext context, string url) : CommandParameters
{
    public BrowsingContext Context { get; } = context;

    public string Url { get; } = url;

    public ReadinessState? Wait { get; set; }
}

public enum ReadinessState
{
    None,
    Interactive,
    Complete
}

public class NavigateResult(Navigation navigation, string url)
{
    public Navigation Navigation { get; } = navigation;

    public string Url { get; } = url;
}
