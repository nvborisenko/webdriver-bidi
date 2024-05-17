using System;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public sealed class NetworkModule
{
    private readonly BiDi.Session _session;
    private readonly Broker _broker;

    internal NetworkModule(BiDi.Session session, Broker broker)
    {
        _session = session;
        _broker = broker;
    }

    public async Task<AddInterceptResult> AddInterceptAsync(AddInterceptParameters parameters)
    {
        return await _broker.ExecuteCommandAsync<AddInterceptCommand, AddInterceptResult>(new AddInterceptCommand { Params = parameters }).ConfigureAwait(false);
    }

    public async Task ContinueRequestAsync(ContinueRequestParameters parameters)
    {
        await _broker.ExecuteCommandAsync(new ContinueRequestCommand { Params = parameters }).ConfigureAwait(false);
    }

    public async Task FailRequestAsync(FailRequestParameters parameters)
    {
        await _broker.ExecuteCommandAsync(new FailRequestCommand { Params = parameters }).ConfigureAwait(false);
    }

    public async Task OnBeforeRequestSentAsync(Func<BeforeRequestSentEventArgs, Task> callback, SynchronizationContext syncContext)
    {
        await _session.SubscribeAsync("network.beforeRequestSent").ConfigureAwait(false);

        _broker.RegisterEventHandler("network.beforeRequestSent", new BiDiEventHandler<BeforeRequestSentEventArgs>(syncContext, callback));
    }

    public async Task OnBeforeRequestSentAsync(Action<BeforeRequestSentEventArgs> callback, SynchronizationContext syncContext)
    {
        await _session.SubscribeAsync("network.beforeRequestSent").ConfigureAwait(false);

        _broker.RegisterEventHandler("network.beforeRequestSent", new BiDiEventHandler<BeforeRequestSentEventArgs>(syncContext, callback));
    }
}
