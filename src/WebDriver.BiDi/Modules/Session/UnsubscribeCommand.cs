using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Session;

internal class UnsubscribeCommand(SubscribeCommandParameters @params)
    : Command<SubscribeCommandParameters>("session.unsubscribe", @params)
{

}
