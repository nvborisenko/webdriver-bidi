using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class NavigateCommand(NavigateCommandParameters parameters) : Command<NavigateCommandParameters>("browsingContext.navigate", parameters)
{

}

public class NavigateCommandParameters : CommandParameters
{
    public BrowsingContext Context { get; set; }

    public string Url { get; set; }

    public ReadinessState? Wait { get; set; }
}

public enum ReadinessState
{
    None,
    Interactive,
    Complete
}

public class NavigateResult
{
    public Navigation Navigation { get; set; }

    public string Url { get; set; }

}