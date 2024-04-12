using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Session;

internal class SubscribeCommand : Command<SubscriptionCommandParameters>
{
    public override string Method { get; } = "session.subscribe";
}

internal class SubscriptionCommandParameters
{
    public string[] Events { get; set; }
}
