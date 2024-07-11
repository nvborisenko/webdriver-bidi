using System.Threading.Tasks;
using System;
using OpenQA.Selenium.BiDi.Modules.Network;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class BrowsingContextNetworkModule
{
    private readonly BrowsingContext _context;
    private readonly NetworkModule _networkModule;

    public BrowsingContextNetworkModule(BrowsingContext context, NetworkModule networkModule)
    {
        _context = context;
        _networkModule = networkModule;
    }

    public async Task<Intercept> OnBeforeRequestSentAsync(InterceptOptions? interceptOptions, Func<BeforeRequestSentEventArgs, Task> callback)
    {
        interceptOptions ??= new();

        interceptOptions.Contexts = [_context];

        var interceptResult = await _networkModule.AddInterceptAsync([InterceptPhase.BeforeRequestSent], interceptOptions).ConfigureAwait(false);

        await interceptResult.Intercept.OnBeforeRequestSentAsync(_context, callback).ConfigureAwait(false);

        return interceptResult.Intercept;
    }

    public async Task<Intercept> OnResponseStartedAsync(InterceptOptions? interceptOptions, Func<ResponseStartedEventArgs, Task> callback)
    {
        interceptOptions ??= new();

        interceptOptions.Contexts = [_context];

        var interceptResult = await _networkModule.AddInterceptAsync([InterceptPhase.ResponseStarted], interceptOptions).ConfigureAwait(false);

        await interceptResult.Intercept.OnResponseStartedAsync(_context, callback).ConfigureAwait(false);

        return interceptResult.Intercept;
    }

    public Task<Subscription> OnBeforeRequestSentAsync(Func<BeforeRequestSentEventArgs, Task> callback)
    {
        return _networkModule.OnBeforeRequestSentAsync(callback, _context);
    }

    public Task<Subscription> OnBeforeRequestSentAsync(Action<BeforeRequestSentEventArgs> callback)
    {
        return _networkModule.OnBeforeRequestSentAsync(callback, _context);
    }

    public Task<Subscription> OnResponseStartedAsync(Func<ResponseStartedEventArgs, Task> callback)
    {
        return _networkModule.OnResponseStartedAsync(callback, _context);
    }

    public Task<Subscription> OnResponseStartedAsync(Action<ResponseStartedEventArgs> callback)
    {
        return _networkModule.OnResponseStartedAsync(callback, _context);
    }

    public Task<Subscription> OnResponseCompletedAsync(Func<ResponseCompletedEventArgs, Task> callback)
    {
        return _networkModule.OnResponseCompletedAsync(callback, _context);
    }

    public Task<Subscription> OnResponseCompletedAsync(Action<ResponseCompletedEventArgs> callback)
    {
        return _networkModule.OnResponseCompletedAsync(callback, _context);
    }

    public Task<Subscription> OnFetchErrorAsync(Func<FetchErrorEventArgs, Task> callback)
    {
        return _networkModule.OnFetchErrorAsync(callback, _context);
    }

    public Task<Subscription> OnFetchErrorAsync(Action<FetchErrorEventArgs> callback)
    {
        return _networkModule.OnFetchErrorAsync(callback, _context);
    }
}
