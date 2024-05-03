using System;
using System.Collections.Generic;
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

    public async Task AddInterceptAsync(List<InterceptPhase> phases, List<UrlPattern> urlPatterns = default)
    {
        var parameters = new AddInterceptParameters
        {
            Phases = phases,
            UrlPatterns = urlPatterns
        };

        await AddInterceptAsync(parameters).ConfigureAwait(false);
    }

    public async Task AddInterceptAsync(AddInterceptParameters parameters)
    {
        await _broker.ExecuteCommandAsync(new AddInterceptCommand { Params = parameters }).ConfigureAwait(false);
    }

    public async Task ContinueRequestAsync(ContinueRequestParameters parameters)
    {
        await _broker.ExecuteCommandAsync(new ContinueRequestCommand { Params = parameters }).ConfigureAwait(false);
    }

    public async Task OnBeforeRequestSentAsync(Func<BeforeRequestSentEventArgs, Task> callback)
    {
        var syncContext = SynchronizationContext.Current;

        await _session.SubscribeAsync("network.beforeRequestSent").ConfigureAwait(false);

        _broker.RegisterEventHandler("network.beforeRequestSent", new BiDiEventHandler<BeforeRequestSentEventArgs>(syncContext, callback));
    }

    public async Task OnBeforeRequestSentAsync(Action<BeforeRequestSentEventArgs> callback)
    {
        var syncContext = SynchronizationContext.Current;

        await _session.SubscribeAsync("network.beforeRequestSent").ConfigureAwait(false);

        _broker.RegisterEventHandler("network.beforeRequestSent", new BiDiEventHandler<BeforeRequestSentEventArgs>(syncContext, callback));
    }
}
