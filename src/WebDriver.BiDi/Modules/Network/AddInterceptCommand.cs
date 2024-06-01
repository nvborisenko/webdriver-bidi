using System.Collections.Generic;
using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class AddInterceptCommand(AddInterceptCommand.Parameters @params)
    : Command<AddInterceptCommand.Parameters>("network.addIntercept", @params)
{
    internal class Parameters(IEnumerable<InterceptPhase> phases) : CommandParameters
    {
        public IEnumerable<InterceptPhase> Phases { get; set; } = phases;

        public List<BrowsingContext.BrowsingContext>? Contexts { get; set; }

        public List<UrlPattern>? UrlPatterns { get; set; }
    }
}

public class AddInterceptResult
{
    public Intercept Intercept { get; set; }
}

public enum InterceptPhase
{
    BeforeRequestSent,
    ResponseStarted,
    AuthRequired
}
