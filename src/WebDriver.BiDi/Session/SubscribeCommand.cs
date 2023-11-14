using System.Collections.Generic;

namespace OpenQA.Selenium.BiDi.Session;

internal class SubscribeCommand : Command<SubscriptionCommandParameters>
{
    public override string Method { get; } = "session.subscribe";
}

internal class SubscriptionCommandParameters
{
    public string[] Events { get; set; }
}
