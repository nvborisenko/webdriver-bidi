using OpenQA.Selenium.BiDi.Communication;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Session;

internal sealed class SessionModule(Broker broker) : Module(broker)
{
    public async Task<StatusResult> StatusAsync()
    {
        return await Broker.ExecuteCommandAsync<StatusResult>(new StatusCommand()).ConfigureAwait(false);
    }

    public async Task SubscribeAsync(SubscribeCommandParameters @params)
    {
        await Broker.ExecuteCommandAsync(new SubscribeCommand(@params)).ConfigureAwait(false);
    }

    public async Task UnsubscribeAsync(SubscribeCommandParameters @params)
    {
        await Broker.ExecuteCommandAsync(new UnsubscribeCommand(@params)).ConfigureAwait(false);
    }

    public async Task<NewResult> NewAsync(NewCommandParameters @params)
    {
        return await Broker.ExecuteCommandAsync<NewResult>(new NewCommand(@params)).ConfigureAwait(false);
    }
}
