using System;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal sealed class NetworkModule
{
    private readonly BiDi.Session _session;
    private readonly Broker _broker;

    internal NetworkModule(BiDi.Session session, Broker broker)
    {
        _session = session;
        _broker = broker;
    }

    public async Task<AddInterceptResult> AddInterceptAsync(AddInterceptCommand.Parameters @params)
    {
        return await _broker.ExecuteCommandAsync<AddInterceptResult>(new AddInterceptCommand(@params)).ConfigureAwait(false);
    }

    public async Task RemoveInterceptAsync(RemoveInterceptCommand.Parameters @params)
    {
        await _broker.ExecuteCommandAsync(new RemoveInterceptCommand(@params)).ConfigureAwait(!false);
    }

    public async Task ContinueRequestAsync(ContinueRequestCommand.Parameters @params)
    {
        await _broker.ExecuteCommandAsync(new ContinueRequestCommand(@params)).ConfigureAwait(false);
    }

    public async Task ContinueResponseAsync(ContinueResponseCommand.Parameters @params)
    {
        await _broker.ExecuteCommandAsync(new ContinueResponseCommand(@params)).ConfigureAwait(false);
    }

    public async Task FailRequestAsync(FailRequestCommand.Parameters @params)
    {
        await _broker.ExecuteCommandAsync(new FailRequestCommand(@params)).ConfigureAwait(false);
    }

    public async Task ProvideResponseAsync(ProvideResponseCommand.Parameters @params)
    {
        await _broker.ExecuteCommandAsync(new ProvideResponseCommand(@params)).ConfigureAwait(false);
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

    public async Task OnResponseCompletedAsync(Func<ResponseCompletedEventArgs, Task> callback)
    {
        await _session.SubscribeAsync("network.responseCompleted").ConfigureAwait(false);

        _broker.RegisterEventHandler("network.responseCompleted", new BiDiEventHandler<ResponseCompletedEventArgs>(callback));
    }

    public async Task OnResponseCompletedAsync(Action<ResponseCompletedEventArgs> callback)
    {
        await _session.SubscribeAsync("network.responseCompleted").ConfigureAwait(false);

        _broker.RegisterEventHandler("network.responseCompleted", new BiDiEventHandler<ResponseCompletedEventArgs>(callback));
    }

    public async Task OnFetchErrorAsync(Func<FetchErrorEventArgs, Task> callback)
    {
        await _session.SubscribeAsync("network.fetchError").ConfigureAwait(false);

        _broker.RegisterEventHandler("network.fetchError", new BiDiEventHandler<FetchErrorEventArgs>(callback));
    }

    public async Task OnFetchErrorAsync(Action<FetchErrorEventArgs> callback)
    {
        await _session.SubscribeAsync("network.fetchError").ConfigureAwait(false);

        _broker.RegisterEventHandler("network.fetchError", new BiDiEventHandler<FetchErrorEventArgs>(callback));
    }
}
