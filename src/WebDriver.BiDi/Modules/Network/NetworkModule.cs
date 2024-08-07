﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public sealed class NetworkModule(Broker broker) : Module(broker)
{
    public async Task<Intercept> AddInterceptAsync(IEnumerable<InterceptPhase> phases, AddInterceptOptions? options = default)
    {
        var @params = new AddInterceptCommandParameters(phases);

        if (options is not null)
        {
            @params.Contexts = options.Contexts;
            @params.UrlPatterns = options.UrlPatterns;
        }

        var result = await Broker.ExecuteCommandAsync<AddInterceptResult>(new AddInterceptCommand(@params), options).ConfigureAwait(false);

        return result.Intercept;
    }

    public async Task RemoveInterceptAsync(Intercept intercept, RemoveInterceptOptions? options = default)
    {
        var @params = new RemoveInterceptCommandParameters(intercept);

        await Broker.ExecuteCommandAsync(new RemoveInterceptCommand(@params), options).ConfigureAwait(!false);
    }

    internal async Task ContinueRequestAsync(Request request, ContinueRequestOptions? options = default)
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

    internal async Task ContinueResponseAsync(Request request, ContinueResponseOptions? options = default)
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

    internal async Task FailRequestAsync(Request request, FailRequestOptions? options = default)
    {
        var @params = new FailRequestCommandParameters(request);

        await Broker.ExecuteCommandAsync(new FailRequestCommand(@params), options).ConfigureAwait(false);
    }

    internal async Task ProvideResponseAsync(Request request, ProvideResponseOptions? options = default)
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

    internal async Task ContinueWithAuthAsync(Request request, AuthCredentials credentials, ContinueWithAuthOptions? options = default)
    {
        await Broker.ExecuteCommandAsync(new ContinueWithAuthCommand(new ContinueWithAuthCredentials(request, credentials)), options).ConfigureAwait(false);
    }

    internal async Task ContinueWithAuthAsync(Request request, ContinueWithDefaultAuthOptions? options = default)
    {
        await Broker.ExecuteCommandAsync(new ContinueWithAuthCommand(new ContinueWithDefaultAuth(request)), options).ConfigureAwait(false);
    }

    internal async Task ContinueWithAuthAsync(Request request, ContinueWithCancelledAuthOptions? options = default)
    {
        await Broker.ExecuteCommandAsync(new ContinueWithAuthCommand(new ContinueWithCancelledAuth(request)), options).ConfigureAwait(false);
    }

    public async Task<Subscription> OnBeforeRequestSentAsync(Func<BeforeRequestSentEventArgs, Task> handler, SubscriptionOptions? options = default)
    {
        return await Broker.SubscribeAsync("network.beforeRequestSent", handler, options).ConfigureAwait(false);
    }

    public async Task<Subscription> OnBeforeRequestSentAsync(Action<BeforeRequestSentEventArgs> handler, SubscriptionOptions? options = default)
    {
        return await Broker.SubscribeAsync("network.beforeRequestSent", handler, options).ConfigureAwait(false);
    }

    public async Task<Intercept> OnBeforeRequestSentAsync(AddInterceptOptions? interceptOptions, Func<BeforeRequestSentEventArgs, Task> handler, SubscriptionOptions? options = default)
    {
        var intercept = await AddInterceptAsync([InterceptPhase.BeforeRequestSent], interceptOptions).ConfigureAwait(false);

        await intercept.OnBeforeRequestSentAsync(handler, options).ConfigureAwait(false);

        return intercept;
    }

    public async Task<Subscription> OnResponseStartedAsync(Func<ResponseStartedEventArgs, Task> handler, SubscriptionOptions? options = default)
    {
        return await Broker.SubscribeAsync("network.responseStarted", handler, options).ConfigureAwait(false);
    }

    public async Task<Subscription> OnResponseStartedAsync(Action<ResponseStartedEventArgs> handler, SubscriptionOptions? options = default)
    {
        return await Broker.SubscribeAsync("network.responseStarted", handler, options).ConfigureAwait(false);
    }

    public async Task<Intercept> OnResponseStartedAsync(AddInterceptOptions? interceptOptions, Func<ResponseStartedEventArgs, Task> handler, SubscriptionOptions? options = default)
    {
        var intercept = await AddInterceptAsync([InterceptPhase.ResponseStarted], interceptOptions).ConfigureAwait(false);

        await intercept.OnResponseStartedAsync(handler, options).ConfigureAwait(false);

        return intercept;
    }

    public async Task<Subscription> OnResponseCompletedAsync(Func<ResponseCompletedEventArgs, Task> handler, SubscriptionOptions? options = default)
    {
        return await Broker.SubscribeAsync("network.responseCompleted", handler, options).ConfigureAwait(false);
    }

    public async Task<Subscription> OnResponseCompletedAsync(Action<ResponseCompletedEventArgs> handler, SubscriptionOptions? options = default)
    {
        return await Broker.SubscribeAsync("network.responseCompleted", handler, options).ConfigureAwait(false);
    }

    public async Task<Subscription> OnFetchErrorAsync(Func<FetchErrorEventArgs, Task> handler, SubscriptionOptions? options = default)
    {
        return await Broker.SubscribeAsync("network.fetchError", handler, options).ConfigureAwait(false);
    }

    public async Task<Subscription> OnFetchErrorAsync(Action<FetchErrorEventArgs> handler, SubscriptionOptions? options = default)
    {
        return await Broker.SubscribeAsync("network.fetchError", handler, options).ConfigureAwait(false);
    }

    internal async Task<Subscription> OnAuthRequiredAsync(Func<AuthRequiredEventArgs, Task> handler, SubscriptionOptions? options = default)
    {
        return await Broker.SubscribeAsync("network.authRequired", handler, options).ConfigureAwait(false);
    }

    public async Task<Intercept> OnAuthRequiredAsync(AddInterceptOptions? interceptOptions, Func<AuthRequiredEventArgs, Task> handler, SubscriptionOptions? options = default)
    {
        var intercept = await AddInterceptAsync([InterceptPhase.AuthRequired], interceptOptions).ConfigureAwait(false);

        await intercept.OnAuthRequiredAsync(handler, options).ConfigureAwait(false);

        return intercept;
    }
}
