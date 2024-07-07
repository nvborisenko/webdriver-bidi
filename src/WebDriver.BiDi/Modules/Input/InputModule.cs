using OpenQA.Selenium.BiDi.Communication;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Input;

internal sealed class InputModule(Broker broker) : Module(broker)
{
    public async Task PerformActionsAsync(PerformActionsCommandParameters @params)
    {
        await Broker.ExecuteCommandAsync(new PerformActionsCommand(@params)).ConfigureAwait(false);
    }

    public async Task ReleaseActionsAsync(ReleaseActionsCommandParameters @params)
    {
        await Broker.ExecuteCommandAsync(new ReleaseActionsCommand(@params));
    }
}
