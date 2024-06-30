using OpenQA.Selenium.BiDi.Communication;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Session;

internal sealed class Module
{
    private readonly BiDi.Session _session;
    private readonly Broker _broker;

    internal Module(BiDi.Session session, Broker broker)
    {
        _session = session;
        _broker = broker;
    }

    public async Task<StatusResult> StatusAsync()
    {
        return await _broker.ExecuteCommandAsync<StatusResult>(new StatusCommand()).ConfigureAwait(false);
    }

    public async Task SubscribeAsync(SubscribeCommandParameters @params)
    {
        await _broker.ExecuteCommandAsync(new SubscribeCommand(@params)).ConfigureAwait(false);
    }

    public async Task UnsubscribeAsync(SubscribeCommandParameters @params)
    {
        await _broker.ExecuteCommandAsync(new UnsubscribeCommand(@params)).ConfigureAwait(false);
    }

    public async Task<NewResult> NewAsync(NewCommandParameters @params)
    {
        return await _broker.ExecuteCommandAsync<NewResult>(new NewCommand(@params)).ConfigureAwait(false);
    }
}
