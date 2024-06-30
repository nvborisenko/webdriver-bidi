using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.Session;

internal class UnsubscribeCommand(SubscribeCommandParameters @params) : Command<SubscribeCommandParameters>(@params);
