using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

internal class NavigateCommand(NavigateCommand.Parameters @params)
    : Command<NavigateCommand.Parameters>("browsingContext.navigate", @params)
{
    internal class Parameters : CommandParameters
    {
        public BrowsingContext Context { get; set; }

        public string Url { get; set; }

        public ReadinessState? Wait { get; set; }
    }
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
