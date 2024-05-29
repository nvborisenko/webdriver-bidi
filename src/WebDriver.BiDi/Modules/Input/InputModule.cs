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

    public async Task PerformActionsAsync(PerformActionsParameters parameters)
    {
        await _broker.ExecuteCommandAsync(new PerformActionsCommand(parameters)).ConfigureAwait(false);
    }

    public async Task ReleaseActionsAsync(ReleaseActionsParameters parameters)
    {
        await _broker.ExecuteCommandAsync(new ReleaseActionsCommand(parameters));
    }
}
