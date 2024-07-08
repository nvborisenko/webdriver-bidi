using OpenQA.Selenium.BiDi.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Session;

internal sealed class SessionModule(Broker broker) : Module(broker)
{
    public async Task<StatusResult> StatusAsync()
    {
        return await Broker.ExecuteCommandAsync<StatusResult>(new StatusCommand()).ConfigureAwait(false);
    }

    public async Task SubscribeAsync(IEnumerable<string> events, SubscribeOptions? options = default)
    {
        var @params = new SubscribeCommandParameters(events);

        if (options is not null)
        {
            @params.Contexts = options.Contexts;
        }

        await Broker.ExecuteCommandAsync(new SubscribeCommand(@params)).ConfigureAwait(false);
    }

    public async Task UnsubscribeAsync(IEnumerable<string> events, SubscribeOptions? options = default)
    {
        var @params = new SubscribeCommandParameters(events);

        if (options is not null)
        {
            @params.Contexts = options.Contexts;
        }

        await Broker.ExecuteCommandAsync(new UnsubscribeCommand(@params)).ConfigureAwait(false);
    }

    public async Task<NewResult> NewAsync(CapabilitiesRequest capabilitiesRequest)
    {
        var @params = new NewCommandParameters(capabilitiesRequest);

        return await Broker.ExecuteCommandAsync<NewResult>(new NewCommand(@params)).ConfigureAwait(false);
    }
}
