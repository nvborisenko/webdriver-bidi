using OpenQA.Selenium.BiDi.Internal;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Session;

public sealed class SessionModule
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
        return await _broker.ExecuteCommandAsync<StatusCommand, StatusResult>(new StatusCommand()).ConfigureAwait(false);
    }

    internal async Task SubscribeAsync(SubscriptionCommandParameters parameters)
    {
        await _broker.ExecuteCommandAsync(new SubscribeCommand() { Params = parameters }).ConfigureAwait(false);
    }
}
