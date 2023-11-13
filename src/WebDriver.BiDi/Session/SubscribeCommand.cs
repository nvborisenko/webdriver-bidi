using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Session;

internal class SubscribeCommand : Command<SubscriptionCommandParameters>
{
    public override string Name { get; } = "session.subscribe";
}

internal class SubscriptionCommandParameters
{
    public string[] Events { get; set; }
}
