using System;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal sealed class NetworkModule(Broker broker) : Module(broker)
{
    public async Task<AddInterceptResult> AddInterceptAsync(AddInterceptCommandParameters @params)
    {
        return await Broker.ExecuteCommandAsync<AddInterceptResult>(new AddInterceptCommand(@params)).ConfigureAwait(false);
    }

    public async Task RemoveInterceptAsync(RemoveInterceptCommandParameters @params)
    {
        await Broker.ExecuteCommandAsync(new RemoveInterceptCommand(@params)).ConfigureAwait(!false);
    }

    public async Task ContinueRequestAsync(ContinueRequestCommandParameters @params)
    {
        await Broker.ExecuteCommandAsync(new ContinueRequestCommand(@params)).ConfigureAwait(false);
    }

    public async Task ContinueResponseAsync(ContinueResponseCommandParameters @params)
    {
        await Broker.ExecuteCommandAsync(new ContinueResponseCommand(@params)).ConfigureAwait(false);
    }

    public async Task FailRequestAsync(FailRequestCommandParameters @params)
    {
        await Broker.ExecuteCommandAsync(new FailRequestCommand(@params)).ConfigureAwait(false);
    }

    public async Task ProvideResponseAsync(ProvideResponseCommandParameters @params)
    {
        await Broker.ExecuteCommandAsync(new ProvideResponseCommand(@params)).ConfigureAwait(false);
    }

    public async Task<Subscription> OnBeforeRequestSentAsync(Func<BeforeRequestSentEventArgs, Task> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("network.beforeRequestSent", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnBeforeRequestSentAsync(Action<BeforeRequestSentEventArgs> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("network.beforeRequestSent", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnResponseStartedAsync(Func<ResponseStartedEventArgs, Task> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("network.responseStarted", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnResponseStartedAsync(Action<ResponseStartedEventArgs> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("network.responseStarted", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnResponseCompletedAsync(Func<ResponseCompletedEventArgs, Task> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("network.responseCompleted", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnResponseCompletedAsync(Action<ResponseCompletedEventArgs> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("network.responseCompleted", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnFetchErrorAsync(Func<FetchErrorEventArgs, Task> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("network.fetchError", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnFetchErrorAsync(Action<FetchErrorEventArgs> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("network.fetchError", callback, context).ConfigureAwait(false);
    }
}
