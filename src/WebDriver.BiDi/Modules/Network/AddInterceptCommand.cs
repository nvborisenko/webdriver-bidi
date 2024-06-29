using System.Collections.Generic;
using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class AddInterceptCommand(AddInterceptCommandParameters @params)
    : Command<AddInterceptCommandParameters>("network.addIntercept", @params)
{

}

internal class AddInterceptCommandParameters(IEnumerable<InterceptPhase> phases) : CommandParameters
{
    public IEnumerable<InterceptPhase> Phases { get; set; } = phases;

    public IEnumerable<BrowsingContext.BrowsingContext>? Contexts { get; set; }

    public IEnumerable<UrlPattern>? UrlPatterns { get; set; }
}

public class AddInterceptResult(Intercept intercept)
{
    public Intercept Intercept { get; } = intercept;
}

public enum InterceptPhase
{
    BeforeRequestSent,
    ResponseStarted,
    AuthRequired
}
