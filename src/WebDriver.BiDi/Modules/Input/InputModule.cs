using OpenQA.Selenium.BiDi.Internal;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Input;

public sealed class InputModule
{
    private readonly Broker _broker;

    internal InputModule(Broker broker)
    {
        _broker = broker;
    }

    public async Task PerformActionsAsync(string context, PerformActionsParameters parameters)
    {
        parameters.Context = context;

        await _broker.ExecuteCommandAsync(new PerformActionsCommand() { Params = parameters }).ConfigureAwait(false);
    }
}
