using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi;

public class Session : IAsyncDisposable
{
    private readonly Transport _transport;
    private readonly Broker _broker;

    private readonly Lazy<Modules.Session.SessionModule> _sessionModule;
    private readonly Lazy<Modules.BrowsingContext.BrowsingContextModule> _browsingContextModule;
    private readonly Lazy<Modules.Browser.BrowserModule> _browserModule;
    private readonly Lazy<Modules.Network.NetworkModule> _networkModule;
    private readonly Lazy<Modules.Input.InputModule> _inputModule;
    private readonly Lazy<Modules.Script.ScriptModule> _scriptModule;
    private readonly Lazy<Modules.Log.LogModule> _logModule;
    private readonly Lazy<Modules.Storage.StorageModule> _storageModule;

    internal Session(string url)
    {
        var uri = new Uri(url);

        _transport = new Transport(new Uri(url));
        _broker = new Broker(this, _transport);

        _sessionModule = new Lazy<Modules.Session.SessionModule>(() => new Modules.Session.SessionModule(_broker));
        _browsingContextModule = new Lazy<Modules.BrowsingContext.BrowsingContextModule>(() => new Modules.BrowsingContext.BrowsingContextModule(_broker));
        _browserModule = new Lazy<Modules.Browser.BrowserModule>(() => new Modules.Browser.BrowserModule(_broker));
        _networkModule = new Lazy<Modules.Network.NetworkModule>(() => new Modules.Network.NetworkModule(_broker));
        _inputModule = new Lazy<Modules.Input.InputModule>(() => new Modules.Input.InputModule(_broker));
        _scriptModule = new Lazy<Modules.Script.ScriptModule>(() => new Modules.Script.ScriptModule(_broker));
        _logModule = new Lazy<Modules.Log.LogModule>(() => new Modules.Log.LogModule(_broker));
        _storageModule = new Lazy<Modules.Storage.StorageModule>(() => new Modules.Storage.StorageModule(_broker));
    }

    internal Modules.Session.SessionModule SessionModule => _sessionModule.Value;
    internal Modules.BrowsingContext.BrowsingContextModule BrowsingContextModule => _browsingContextModule.Value;
    internal Modules.Browser.BrowserModule BrowserModule => _browserModule.Value;
    internal Modules.Network.NetworkModule NetworkModule => _networkModule.Value;
    internal Modules.Input.InputModule InputModule => _inputModule.Value;
    internal Modules.Script.ScriptModule ScriptModule => _scriptModule.Value;
    internal Modules.Log.LogModule LogModule => _logModule.Value;
    internal Modules.Storage.StorageModule StorageModule => _storageModule.Value;

    public Task<Modules.Session.StatusResult> StatusAsync()
    {
        return SessionModule.StatusAsync();
    }

    public async Task<Modules.BrowsingContext.BrowsingContext> CreateBrowsingContextAsync(Modules.BrowsingContext.BrowsingContextType type, Modules.BrowsingContext.BrowsingContextOptions? options = default)
    {
        var createResult = await BrowsingContextModule.CreateAsync(type, options).ConfigureAwait(false);

        return createResult.Context;
    }

    public Task<Modules.Browser.UserContextInfo> CreateBrowserUserContextAsync(Modules.Browser.CreateUserContextOptions? options = default)
    {
        return BrowserModule.CreateUserContextAsync(options);
    }

    public async Task<IReadOnlyList<Modules.Browser.UserContextInfo>> GetBrowserUserContextsAsync(Modules.Browser.GetUserContextsOptions? options = default)
    {
        var result = await BrowserModule.GetUserContextsAsync(options).ConfigureAwait(false);

        return result.UserContexts;
    }

    public async Task<IReadOnlyList<Modules.BrowsingContext.BrowsingContextInfo>> GetTreeAsync(Modules.BrowsingContext.TreeOptions? options = default)
    {
        var result = await BrowsingContextModule.GetTreeAsync(options).ConfigureAwait(false);

        return result.Contexts;
    }

    public Task<Subscription> OnBrowsingContextCreatedAsync(Action<Modules.BrowsingContext.BrowsingContextInfo> callback)
    {
        return BrowsingContextModule.OnContextCreatedAsync(callback);
    }

    public Task<Subscription> OnBrowsingContextCreatedAsync(Func<Modules.BrowsingContext.BrowsingContextInfo, Task> callback)
    {
        return BrowsingContextModule.OnContextCreatedAsync(callback);
    }

    public Task<Subscription> OnBrowsingContextDestroyedAsync(Action<Modules.BrowsingContext.BrowsingContextInfo> callback)
    {
        return BrowsingContextModule.OnContextDestroyedAsync(callback);
    }

    public Task<Subscription> OnBrowsingContextDestroyedAsync(Func<Modules.BrowsingContext.BrowsingContextInfo, Task> callback)
    {
        return BrowsingContextModule.OnContextDestroyedAsync(callback);
    }

    public Task<Subscription> OnBeforeRequestSentAsync(Func<Modules.Network.BeforeRequestSentEventArgs, Task> callback)
    {
        return NetworkModule.OnBeforeRequestSentAsync(callback);
    }

    public async Task<Modules.Network.Intercept> OnBeforeRequestSentAsync(Modules.Network.InterceptOptions? interceptOptions, Func<Modules.Network.BeforeRequestSentEventArgs, Task> callback)
    {
        var interceptResult = await NetworkModule.AddInterceptAsync([Modules.Network.InterceptPhase.BeforeRequestSent], interceptOptions).ConfigureAwait(false);

        await interceptResult.Intercept.OnBeforeRequestSentAsync(callback).ConfigureAwait(false);

        return interceptResult.Intercept;
    }

    public Task<Subscription> OnBeforeRequestSentAsync(Action<Modules.Network.BeforeRequestSentEventArgs> callback)
    {
        return NetworkModule.OnBeforeRequestSentAsync(callback);
    }

    public Task<Subscription> OnResponseStartedAsync(Func<Modules.Network.ResponseStartedEventArgs, Task> callback)
    {
        return NetworkModule.OnResponseStartedAsync(callback);
    }

    public async Task<Modules.Network.Intercept> OnResponseStartedAsync(Modules.Network.InterceptOptions? interceptOptions, Func<Modules.Network.ResponseStartedEventArgs, Task> callback)
    {
        var interceptResult = await NetworkModule.AddInterceptAsync([Modules.Network.InterceptPhase.ResponseStarted], interceptOptions).ConfigureAwait(false);

        await interceptResult.Intercept.OnResponseStartedAsync(callback).ConfigureAwait(false);

        return interceptResult.Intercept;
    }

    public Task<Subscription> OnResponseStartedAsync(Action<Modules.Network.ResponseStartedEventArgs> callback)
    {
        return NetworkModule.OnResponseStartedAsync(callback);
    }

    public Task<Subscription> OnUserPromptOpenedAsync(Func<Modules.BrowsingContext.UserPromptOpenedEventArgs, Task> callback)
    {
        return BrowsingContextModule.OnUserPromptOpenedAsync(callback);
    }

    public Task<Subscription> OnUserPromptOpenedAsync(Action<Modules.BrowsingContext.UserPromptOpenedEventArgs> callback)
    {
        return BrowsingContextModule.OnUserPromptOpenedAsync(callback);
    }

    public Task<Subscription> OnUserPromptClosedAsync(Func<Modules.BrowsingContext.UserPromptClosedEventArgs, Task> callback)
    {
        return BrowsingContextModule.OnUserPromptClosedAsync(callback);
    }

    public Task<Subscription> OnUserPromptClosedAsync(Action<Modules.BrowsingContext.UserPromptClosedEventArgs> callback)
    {
        return BrowsingContextModule.OnUserPromptClosedAsync(callback);
    }

    public Task<Subscription> OnLogEntryAddedAsync(Func<Modules.Log.BaseLogEntry, Task> callback)
    {
        return LogModule.OnEntryAddedAsync(callback);
    }

    public Task<Subscription> OnLogEntryAddedAsync(Action<Modules.Log.BaseLogEntry> callback)
    {
        return LogModule.OnEntryAddedAsync(callback);
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

    public async Task EndAsync(Modules.Session.EndOptions? options = default)
    {
        await _broker.ExecuteCommandAsync(new Modules.Session.EndCommand(), options).ConfigureAwait(false);

        await _broker.DisposeAsync().ConfigureAwait(false);

        _transport?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await EndAsync().ConfigureAwait(false);
    }
}
