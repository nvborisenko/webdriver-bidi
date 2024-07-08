using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.Network;

internal sealed class NetworkModule(Broker broker) : Module(broker)
{
    public async Task<AddInterceptResult> AddInterceptAsync(IEnumerable<InterceptPhase> phases, InterceptOptions? options = default)
    {
        var @params = new AddInterceptCommandParameters(phases);

        if (options is not null)
        {
            @params.Contexts = options.Contexts;
            @params.UrlPatterns = options.UrlPatterns;
        }

        return await Broker.ExecuteCommandAsync<AddInterceptResult>(new AddInterceptCommand(@params), options).ConfigureAwait(false);
    }

    public async Task RemoveInterceptAsync(Intercept intercept, RemoveInterceptOptions? options = default)
    {
        var @params = new RemoveInterceptCommandParameters(intercept);

        await Broker.ExecuteCommandAsync(new RemoveInterceptCommand(@params), options).ConfigureAwait(!false);
    }

    public async Task ContinueRequestAsync(Request request, RequestOptions? options = default)
    {
        var @params = new ContinueRequestCommandParameters(request);

        if (options is not null)
        {
            @params.Body = options.Body;
            @params.Cookies = options.Cookies;
            @params.Headers = options.Headers;
            @params.Method = options.Method;
            @params.Url = options.Url;
        }

        await Broker.ExecuteCommandAsync(new ContinueRequestCommand(@params), options).ConfigureAwait(false);
    }

    public async Task ContinueResponseAsync(Request request, ContinueResponseOptions? options = default)
    {
        var @params = new ContinueResponseCommandParameters(request);

        if (options is not null)
        {
            @params.Cookies = options.Cookies;
            @params.Credentials = options.Credentials;
            @params.Headers = options.Headers;
            @params.ReasonPhrase = options.ReasonPhrase;
            @params.StatusCode = options.StatusCode;
        }

        await Broker.ExecuteCommandAsync(new ContinueResponseCommand(@params), options).ConfigureAwait(false);
    }

    public async Task FailRequestAsync(Request request, FailRequestOptions? options = default)
    {
        var @params = new FailRequestCommandParameters(request);

        await Broker.ExecuteCommandAsync(new FailRequestCommand(@params), options).ConfigureAwait(false);
    }

    public async Task ProvideResponseAsync(Request request, ProvideResponseOptions? options = default)
    {
        var @params = new ProvideResponseCommandParameters(request);

        if (options is not null)
        {
            @params.Body = options.Body;
            @params.Cookies = options.Cookies;
            @params.Headers = options.Headers;
            @params.ReasonPhrase = options.ReasonPhrase;
            @params.StatusCode = options.StatusCode;
        }

        await Broker.ExecuteCommandAsync(new ProvideResponseCommand(@params), options).ConfigureAwait(false);
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
