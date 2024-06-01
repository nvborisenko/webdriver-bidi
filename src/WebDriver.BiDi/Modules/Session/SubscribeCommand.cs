using OpenQA.Selenium.BiDi.Internal;
using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Modules.Session;

internal class SubscribeCommand(SubscribeCommand.Parameters @params)
    : Command<SubscribeCommand.Parameters>("session.subscribe", @params)
{
    internal class Parameters(IEnumerable<string> events) : CommandParameters
    {
        public IEnumerable<string> Events { get; } = events;

        public IEnumerable<BrowsingContext.BrowsingContext>? Contexts { get; set; }
    }
}
