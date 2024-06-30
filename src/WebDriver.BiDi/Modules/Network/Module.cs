using System;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal sealed class Module
{
    private readonly BiDi.Session _session;
    private readonly Broker _broker;

    internal Module(BiDi.Session session, Broker broker)
    {
        _session = session;
        _broker = broker;
    }

    public async Task<AddInterceptResult> AddInterceptAsync(AddInterceptCommandParameters @params)
    {
        return await _broker.ExecuteCommandAsync<AddInterceptResult>(new AddInterceptCommand(@params)).ConfigureAwait(false);
    }

    public async Task RemoveInterceptAsync(RemoveInterceptCommandParameters @params)
    {
        await _broker.ExecuteCommandAsync(new RemoveInterceptCommand(@params)).ConfigureAwait(!false);
    }

    public async Task ContinueRequestAsync(ContinueRequestCommandParameters @params)
    {
        await _broker.ExecuteCommandAsync(new ContinueRequestCommand(@params)).ConfigureAwait(false);
    }

    public async Task ContinueResponseAsync(ContinueResponseCommandParameters @params)
    {
        await _broker.ExecuteCommandAsync(new ContinueResponseCommand(@params)).ConfigureAwait(false);
    }

    public async Task FailRequestAsync(FailRequestCommandParameters @params)
    {
        await _broker.ExecuteCommandAsync(new FailRequestCommand(@params)).ConfigureAwait(false);
    }

    public async Task ProvideResponseAsync(ProvideResponseCommandParameters @params)
    {
        await _broker.ExecuteCommandAsync(new ProvideResponseCommand(@params)).ConfigureAwait(false);
    }

    public async Task<Subscription> OnBeforeRequestSentAsync(Func<BeforeRequestSentEventArgs, Task> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("network.beforeRequestSent", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnBeforeRequestSentAsync(Action<BeforeRequestSentEventArgs> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("network.beforeRequestSent", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnResponseStartedAsync(Func<ResponseStartedEventArgs, Task> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("network.responseStarted", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnResponseStartedAsync(Action<ResponseStartedEventArgs> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("network.responseStarted", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnResponseCompletedAsync(Func<ResponseCompletedEventArgs, Task> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("network.responseCompleted", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnResponseCompletedAsync(Action<ResponseCompletedEventArgs> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("network.responseCompleted", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnFetchErrorAsync(Func<FetchErrorEventArgs, Task> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("network.fetchError", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnFetchErrorAsync(Action<FetchErrorEventArgs> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("network.fetchError", callback, context).ConfigureAwait(false);
    }
}
