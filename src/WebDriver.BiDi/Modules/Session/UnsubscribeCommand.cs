using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Session;

internal class UnsubscribeCommand(SubscribeCommand.Parameters @params)
    : Command<SubscribeCommand.Parameters>("session.unsubscribe", @params)
{

}
