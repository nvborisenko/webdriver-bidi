using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class FailRequestCommand(FailRequestCommandParameters @params) : Command<FailRequestCommandParameters>(@params);

internal class FailRequestCommandParameters(Request request) : CommandParameters
{
    public Request Request { get; } = request;
}
