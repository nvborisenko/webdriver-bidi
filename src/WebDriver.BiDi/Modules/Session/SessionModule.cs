using OpenQA.Selenium.BiDi.Internal;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Session;

internal sealed class SessionModule
{
    private readonly BiDi.Session _session;
    private readonly Broker _broker;

    internal SessionModule(BiDi.Session session, Broker broker)
    {
        _session = session;
        _broker = broker;
    }

    public async Task<StatusResult> StatusAsync()
    {
        return await _broker.ExecuteCommandAsync<StatusResult>(new StatusCommand()).ConfigureAwait(false);
    }

    internal async Task SubscribeAsync(SubscribeCommand.Parameters @params)
    {
        await _broker.ExecuteCommandAsync(new SubscribeCommand(@params)).ConfigureAwait(false);
    }
}
