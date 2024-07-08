using OpenQA.Selenium.BiDi.Communication;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Session;

internal class SubscribeCommand(SubscribeCommandParameters @params) : Command<SubscribeCommandParameters>(@params);

internal class SubscribeCommandParameters(IEnumerable<string> events) : CommandParameters
{
    public IEnumerable<string> Events { get; } = events;

    public IEnumerable<BrowsingContext.BrowsingContext>? Contexts { get; set; }
}

public class SubscribeOptions : CommandOptions
{
    public IEnumerable<BrowsingContext.BrowsingContext>? Contexts { get; set; }
}
