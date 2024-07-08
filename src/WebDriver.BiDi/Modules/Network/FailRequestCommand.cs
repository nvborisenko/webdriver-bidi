using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal class FailRequestCommand(FailRequestCommandParameters @params) : Command<FailRequestCommandParameters>(@params);

internal class FailRequestCommandParameters(Request request) : CommandParameters
{
    public Request Request { get; } = request;
}

public class FailRequestOptions : CommandOptions;
