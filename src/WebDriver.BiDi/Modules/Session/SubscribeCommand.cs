using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Session;

internal class SubscribeCommand(SubscribeCommand.Parameters @params)
    : Command<SubscribeCommand.Parameters>("session.subscribe", @params)
{
    internal class Parameters : CommandParameters
    {
        public string[] Events { get; set; }
    }
}
