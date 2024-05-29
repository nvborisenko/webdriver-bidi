using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Session;

internal class SubscribeCommand(SubscriptionCommandParameters parameters) : Command<SubscriptionCommandParameters>("session.subscribe", parameters)
{

}

internal class SubscriptionCommandParameters : CommandParameters
{
    public string[] Events { get; set; }
}
