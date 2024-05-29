using System;
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
        return await _broker.ExecuteCommandAsync<AddInterceptResult>(new AddInterceptCommand(parameters)).ConfigureAwait(false);
    }

    public async Task RemoveInterceptAsync(RemoveInterceptParameters parameters)
    {
        await _broker.ExecuteCommandAsync(new RemoveInterceptCommand(parameters)).ConfigureAwait(!false);
    }

    public async Task ContinueRequestAsync(ContinueRequestParameters parameters)
    {
        await _broker.ExecuteCommandAsync(new ContinueRequestCommand(parameters)).ConfigureAwait(false);
    }

    public async Task ContinueResponseAsync(ContinueResponseParameters parameters)
    {
        await _broker.ExecuteCommandAsync(new ContinueResponseCommand(parameters)).ConfigureAwait(false);
    }

    public async Task FailRequestAsync(FailRequestParameters parameters)
    {
        await _broker.ExecuteCommandAsync(new FailRequestCommand(parameters)).ConfigureAwait(false);
    }

    public async Task ProvideResponseAsync(ProvideResponseParameters parameters)
    {
        await _broker.ExecuteCommandAsync(new ProvideResponseCommand(parameters)).ConfigureAwait(false);
    }

    public async Task OnBeforeRequestSentAsync(Func<BeforeRequestSentEventArgs, Task> callback)
    {
        await _session.SubscribeAsync("network.beforeRequestSent").ConfigureAwait(false);

        _broker.RegisterEventHandler("network.beforeRequestSent", new BiDiEventHandler<BeforeRequestSentEventArgs>(callback));
    }

    public async Task OnBeforeRequestSentAsync(Action<BeforeRequestSentEventArgs> callback)
    {
        await _session.SubscribeAsync("network.beforeRequestSent").ConfigureAwait(false);

        _broker.RegisterEventHandler("network.beforeRequestSent", new BiDiEventHandler<BeforeRequestSentEventArgs>(callback));
    }

    public async Task OnResponseStartedAsync(Func<ResponseStartedEventArgs, Task> callback)
    {
        await _session.SubscribeAsync("network.responseStarted").ConfigureAwait(false);

        _broker.RegisterEventHandler("network.responseStarted", new BiDiEventHandler<ResponseStartedEventArgs>(callback));
    }

    public async Task OnResponseStartedAsync(Action<ResponseStartedEventArgs> callback)
    {
        await _session.SubscribeAsync("network.responseStarted").ConfigureAwait(false);

        _broker.RegisterEventHandler("network.responseStarted", new BiDiEventHandler<ResponseStartedEventArgs>(callback));
    }
}
