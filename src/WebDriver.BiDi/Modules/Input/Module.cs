using OpenQA.Selenium.BiDi.Internal;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Input;

internal sealed class Module
{
    private readonly Broker _broker;

    internal Module(Broker broker)
    {
        _broker = broker;
    }

    public async Task PerformActionsAsync(PerformActionsCommandParameters @params)
    {
        await _broker.ExecuteCommandAsync(new PerformActionsCommand(@params)).ConfigureAwait(false);
    }

    public async Task ReleaseActionsAsync(ReleaseActionsCommandParameters @params)
    {
        await _broker.ExecuteCommandAsync(new ReleaseActionsCommand(@params));
    }
}
