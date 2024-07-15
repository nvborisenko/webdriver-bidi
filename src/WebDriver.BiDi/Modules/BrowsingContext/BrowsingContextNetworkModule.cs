using System.Threading.Tasks;
using System;
using OpenQA.Selenium.BiDi.Modules.Network;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class BrowsingContextNetworkModule(BrowsingContext context, NetworkModule networkModule)
{
    public async Task<Intercept> OnBeforeRequestSentAsync(InterceptOptions? interceptOptions, Func<BeforeRequestSentEventArgs, Task> callback, SubscriptionOptions? options = default)
    {
        interceptOptions ??= new();

        interceptOptions.Contexts = [context];

        var interceptResult = await networkModule.AddInterceptAsync([InterceptPhase.BeforeRequestSent], interceptOptions).ConfigureAwait(false);

        await interceptResult.Intercept.OnBeforeRequestSentAsync(callback, new BrowsingContextsSubscriptionOptions(options) { Contexts = [context] }).ConfigureAwait(false);

        return interceptResult.Intercept;
    }

    public async Task<Intercept> OnResponseStartedAsync(InterceptOptions? interceptOptions, Func<ResponseStartedEventArgs, Task> callback, SubscriptionOptions? options = default)
    {
        interceptOptions ??= new();

        interceptOptions.Contexts = [context];

        var interceptResult = await networkModule.AddInterceptAsync([InterceptPhase.ResponseStarted], interceptOptions).ConfigureAwait(false);

        await interceptResult.Intercept.OnResponseStartedAsync(callback, new BrowsingContextsSubscriptionOptions(options) { Contexts = [context] }).ConfigureAwait(false);

        return interceptResult.Intercept;
    }

    public async Task<Intercept> OnAuthRequiredAsync(InterceptOptions? interceptOptions, Func<AuthRequiredEventArgs, Task> callback, SubscriptionOptions? options = default)
    {
        interceptOptions ??= new();

        interceptOptions.Contexts = [context];

        var interceptResult = await networkModule.AddInterceptAsync([InterceptPhase.AuthRequired], interceptOptions).ConfigureAwait(false);

        await interceptResult.Intercept.OnAuthRequiredAsync(callback, new BrowsingContextsSubscriptionOptions(options) { Contexts = [context] }).ConfigureAwait(false);

        return interceptResult.Intercept;
    }

    public Task<Subscription> OnBeforeRequestSentAsync(Func<BeforeRequestSentEventArgs, Task> callback, SubscriptionOptions? options = default)
    {
        return networkModule.OnBeforeRequestSentAsync(callback, new BrowsingContextsSubscriptionOptions(options) { Contexts = [context] });
    }

    public Task<Subscription> OnBeforeRequestSentAsync(Action<BeforeRequestSentEventArgs> callback, SubscriptionOptions? options = default)
    {
        return networkModule.OnBeforeRequestSentAsync(callback, new BrowsingContextsSubscriptionOptions(options) { Contexts = [context] });
    }

    public Task<Subscription> OnResponseStartedAsync(Func<ResponseStartedEventArgs, Task> callback, SubscriptionOptions? options = default)
    {
        return networkModule.OnResponseStartedAsync(callback, new BrowsingContextsSubscriptionOptions(options) { Contexts = [context] });
    }

    public Task<Subscription> OnResponseStartedAsync(Action<ResponseStartedEventArgs> callback, SubscriptionOptions? options = default)
    {
        return networkModule.OnResponseStartedAsync(callback, new BrowsingContextsSubscriptionOptions(options) { Contexts = [context] });
    }

    public Task<Subscription> OnResponseCompletedAsync(Func<ResponseCompletedEventArgs, Task> callback, SubscriptionOptions? options = default)
    {
        return networkModule.OnResponseCompletedAsync(callback, new BrowsingContextsSubscriptionOptions(options) { Contexts = [context] });
    }

    public Task<Subscription> OnResponseCompletedAsync(Action<ResponseCompletedEventArgs> callback, SubscriptionOptions? options = default)
    {
        return networkModule.OnResponseCompletedAsync(callback, new BrowsingContextsSubscriptionOptions(options) { Contexts = [context] });
    }

    public Task<Subscription> OnFetchErrorAsync(Func<FetchErrorEventArgs, Task> callback, SubscriptionOptions? options = default)
    {
        return networkModule.OnFetchErrorAsync(callback, new BrowsingContextsSubscriptionOptions(options) { Contexts = [context] });
    }

    public Task<Subscription> OnFetchErrorAsync(Action<FetchErrorEventArgs> callback, SubscriptionOptions? options = default)
    {
        return networkModule.OnFetchErrorAsync(callback, new BrowsingContextsSubscriptionOptions(options) { Contexts = [context] });
    }

    public Task<Subscription> OnAuthRequiredAsync(Func<AuthRequiredEventArgs, Task> callback, SubscriptionOptions? options = default)
    {
        return networkModule.OnAuthRequiredAsync(callback, new BrowsingContextsSubscriptionOptions(options) { Contexts = [context] });
    }

    public Task<Subscription> OnAuthRequiredAsync(Action<AuthRequiredEventArgs> callback, SubscriptionOptions? options = default)
    {
        return networkModule.OnAuthRequiredAsync(callback, new BrowsingContextsSubscriptionOptions(options) { Contexts = [context] });
    }
}
