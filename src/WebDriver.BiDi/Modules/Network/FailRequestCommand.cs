using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class FailRequestCommand(FailRequestCommand.Parameters @params)
    : Command<FailRequestCommand.Parameters>("network.failRequest", @params)
{
    internal class Parameters(Request request) : CommandParameters
    {
        public Request Request { get; } = request;
    }
}
