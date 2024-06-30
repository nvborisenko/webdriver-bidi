using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi;

public class Session : IAsyncDisposable
{
    private readonly Transport _transport;
    private readonly Broker _broker;

    private readonly Lazy<Modules.Session.Module> _sessionModule;
    private readonly Lazy<Modules.BrowsingContext.Module> _browsingContextModule;
    private readonly Lazy<Modules.Browser.Module> _browserModule;
    private readonly Lazy<Modules.Network.Module> _networkModule;
    private readonly Lazy<Modules.Input.Module> _inputModule;
    private readonly Lazy<Modules.Script.Module> _scriptModule;
    private readonly Lazy<Modules.Log.Module> _logModule;
    private readonly Lazy<Modules.Storage.Module> _storageModule;

    internal Session(string url)
    {
        var uri = new Uri(url);

        _transport = new Transport(new Uri(url));
        _broker = new Broker(this, _transport);

        _sessionModule = new Lazy<Modules.Session.Module>(() => new Modules.Session.Module(this, _broker));
        _browsingContextModule = new Lazy<Modules.BrowsingContext.Module>(() => new Modules.BrowsingContext.Module(this, _broker));
        _browserModule = new Lazy<Modules.Browser.Module>(() => new Modules.Browser.Module(_broker));
        _networkModule = new Lazy<Modules.Network.Module>(() => new Modules.Network.Module(this, _broker));
        _inputModule = new Lazy<Modules.Input.Module>(() => new Modules.Input.Module(_broker));
        _scriptModule = new Lazy<Modules.Script.Module>(() => new Modules.Script.Module(_broker));
        _logModule = new Lazy<Modules.Log.Module>(() => new Modules.Log.Module(this, _broker));
        _storageModule = new Lazy<Modules.Storage.Module>(() => new Modules.Storage.Module(_broker));
    }

    internal Modules.Session.Module SessionModule => _sessionModule.Value;

    internal Modules.BrowsingContext.Module BrowsingContextModule => _browsingContextModule.Value;

    internal Modules.Browser.Module BrowserModule => _browserModule.Value;

    internal Modules.Network.Module NetworkModule => _networkModule.Value;

    internal Modules.Input.Module InputModule => _inputModule.Value;

    internal Modules.Script.Module ScriptModule => _scriptModule.Value;

    internal Modules.Log.Module LogModule => _logModule.Value;

    internal Modules.Storage.Module StorageModule => _storageModule.Value;

    public Task<Modules.Session.StatusResult> StatusAsync()
    {
        return SessionModule.StatusAsync();
    }

    public async Task<Modules.BrowsingContext.BrowsingContext> CreateBrowsingContextAsync()
    {
        var createResult = await BrowsingContextModule.CreateAsync(new Modules.BrowsingContext.CreateCommandParameters(Modules.BrowsingContext.BrowsingContextType.Tab)).ConfigureAwait(false);

        return createResult.Context;
    }

    

    public async Task<Modules.Network.Intercept> AddInterceptAsync(IEnumerable<Modules.Network.InterceptPhase> phases, IEnumerable<Modules.Network.UrlPattern>? urlPatterns = default)
    {
        var @params = new Modules.Network.AddInterceptCommandParameters(phases)
        {
            UrlPatterns = urlPatterns
        };

        var result = await NetworkModule.AddInterceptAsync(@params).ConfigureAwait(false);

        return result.Intercept;
    }

    public Task<Modules.Browser.UserContextInfo> CreateBrowserUserContextAsync()
    {
        return BrowserModule.CreateUserContextAsync();
    }

    public async Task<IReadOnlyList<Modules.Browser.UserContextInfo>> GetBrowserUserContextsAsync()
    {
        var result = await BrowserModule.GetUserContextsAsync().ConfigureAwait(false);

        return result.UserContexts;
    }

    public async Task<IReadOnlyList<Modules.BrowsingContext.BrowsingContextInfo>> GetTreeAsync(uint? maxDepth = default, Modules.BrowsingContext.BrowsingContext? context = default)
    {
        var @params = new Modules.BrowsingContext.GetTreeCommandParameters
        {
            MaxDepth = maxDepth,
            Root = context
        };

        var result = await BrowsingContextModule.GetTreeAsync(@params).ConfigureAwait(false);

        return result.Contexts;
    }

    public Task OnBrowsingContextCreatedAsync(Action<Modules.BrowsingContext.BrowsingContextInfo> callback)
    {
        return BrowsingContextModule.OnContextCreatedAsync(callback);
    }

    public Task OnBrowsingContextCreatedAsync(Func<Modules.BrowsingContext.BrowsingContextInfo, Task> callback)
    {
        return BrowsingContextModule.OnContextCreatedAsync(callback);
    }

    public Task OnBrowsingContextDestroyedAsync(Action<Modules.BrowsingContext.BrowsingContextInfo> callback)
    {
        return BrowsingContextModule.OnContextDestroyedAsync(callback);
    }

    public Task OnBrowsingContextDestroyedAsync(Func<Modules.BrowsingContext.BrowsingContextInfo, Task> callback)
    {
        return BrowsingContextModule.OnContextDestroyedAsync(callback);
    }

    public Task OnBeforeRequestSentAsync(Func<Modules.Network.BeforeRequestSentEventArgs, Task> callback)
    {
        return NetworkModule.OnBeforeRequestSentAsync(callback);
    }

    public Task OnBeforeRequestSentAsync(Action<Modules.Network.BeforeRequestSentEventArgs> callback)
    {
        return NetworkModule.OnBeforeRequestSentAsync(callback);
    }

    public Task OnResponseStartedAsync(Func<Modules.Network.ResponseStartedEventArgs, Task> callback)
    {
        return NetworkModule.OnResponseStartedAsync(callback);
    }

    public Task OnResponseStartedAsync(Action<Modules.Network.ResponseStartedEventArgs> callback)
    {
        return NetworkModule.OnResponseStartedAsync(callback);
    }

    public Task OnUserPromptOpenedAsync(Func<Modules.BrowsingContext.UserPromptOpenedEventArgs, Task> callback)
    {
        return BrowsingContextModule.OnUserPromptOpenedAsync(callback);
    }

    public Task OnUserPromptOpenedAsync(Action<Modules.BrowsingContext.UserPromptOpenedEventArgs> callback)
    {
        return BrowsingContextModule.OnUserPromptOpenedAsync(callback);
    }

    public Task OnUserPromptClosedAsync(Func<Modules.BrowsingContext.UserPromptClosedEventArgs, Task> callback)
    {
        return BrowsingContextModule.OnUserPromptClosedAsync(callback);
    }

    public Task OnUserPromptClosedAsync(Action<Modules.BrowsingContext.UserPromptClosedEventArgs> callback)
    {
        return BrowsingContextModule.OnUserPromptClosedAsync(callback);
    }

    public static async Task<Session> ConnectAsync(string url)
    {
        var session = new Session(url);

        await session.ConnectAsync().ConfigureAwait(false);

        return session;
    }

    private async Task ConnectAsync()
    {
        await _broker.ConnectAsync(default).ConfigureAwait(false);
    }

    public async Task EndAsync()
    {
        await _broker.ExecuteCommandAsync(new Modules.Session.EndCommand()).ConfigureAwait(false);

        await _broker.DisposeAsync().ConfigureAwait(false);

        _transport?.Dispose();
    }

    internal Task SubscribeAsync(params string[] events)
    {
        return SessionModule.SubscribeAsync(new Modules.Session.SubscribeCommandParameters(events));
    }

    public async ValueTask DisposeAsync()
    {
        await EndAsync().ConfigureAwait(false);
    }
}
