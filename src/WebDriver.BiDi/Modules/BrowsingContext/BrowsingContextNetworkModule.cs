using System.Threading.Tasks;
using System;
using OpenQA.Selenium.BiDi.Modules.Network;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class BrowsingContextNetworkModule(BrowsingContext context, NetworkModule networkModule)
{
    public async Task<Intercept> OnBeforeRequestSentAsync(InterceptOptions? interceptOptions, Func<BeforeRequestSentEventArgs, Task> callback)
    {
        interceptOptions ??= new();

        interceptOptions.Contexts = [context];

        var interceptResult = await networkModule.AddInterceptAsync([InterceptPhase.BeforeRequestSent], interceptOptions).ConfigureAwait(false);

        await interceptResult.Intercept.OnBeforeRequestSentAsync(context, callback).ConfigureAwait(false);

        return interceptResult.Intercept;
    }

    public async Task<Intercept> OnResponseStartedAsync(InterceptOptions? interceptOptions, Func<ResponseStartedEventArgs, Task> callback)
    {
        interceptOptions ??= new();

        interceptOptions.Contexts = [context];

        var interceptResult = await networkModule.AddInterceptAsync([InterceptPhase.ResponseStarted], interceptOptions).ConfigureAwait(false);

        await interceptResult.Intercept.OnResponseStartedAsync(context, callback).ConfigureAwait(false);

        return interceptResult.Intercept;
    }

    public Task<Subscription> OnBeforeRequestSentAsync(Func<BeforeRequestSentEventArgs, Task> callback)
    {
        return networkModule.OnBeforeRequestSentAsync(callback, context);
    }

    public Task<Subscription> OnBeforeRequestSentAsync(Action<BeforeRequestSentEventArgs> callback)
    {
        return networkModule.OnBeforeRequestSentAsync(callback, context);
    }

    public Task<Subscription> OnResponseStartedAsync(Func<ResponseStartedEventArgs, Task> callback)
    {
        return networkModule.OnResponseStartedAsync(callback, context);
    }

    public Task<Subscription> OnResponseStartedAsync(Action<ResponseStartedEventArgs> callback)
    {
        return networkModule.OnResponseStartedAsync(callback, context);
    }

    public Task<Subscription> OnResponseCompletedAsync(Func<ResponseCompletedEventArgs, Task> callback)
    {
        return networkModule.OnResponseCompletedAsync(callback, context);
    }

    public Task<Subscription> OnResponseCompletedAsync(Action<ResponseCompletedEventArgs> callback)
    {
        return networkModule.OnResponseCompletedAsync(callback, context);
    }

    public Task<Subscription> OnFetchErrorAsync(Func<FetchErrorEventArgs, Task> callback)
    {
        return networkModule.OnFetchErrorAsync(callback, context);
    }

    public Task<Subscription> OnFetchErrorAsync(Action<FetchErrorEventArgs> callback)
    {
        return networkModule.OnFetchErrorAsync(callback, context);
    }
}
