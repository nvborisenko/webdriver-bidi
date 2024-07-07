using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class BrowsingContext
{
    internal BrowsingContext(BiDi.Session session, string id)
    {
        Session = session;
        Id = id;
    }

    internal BiDi.Session Session { get; }

    internal string Id { get; }

    public override bool Equals(object obj)
    {
        if (obj is BrowsingContext browsingContextObj) return browsingContextObj.Id == Id;

        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public Task<NavigateResult> NavigateAsync(string url, NavigateOptions? options = default)
    {
        var @params = new NavigateCommandParameters(this, url)
        {
            Wait = options?.Wait
        };

        return Session.BrowsingContextModule.NavigateAsync(@params);
    }

    public Task<NavigateResult> ReloadAsync(ReloadOptions? options = default)
    {
        var @params = new ReloadCommandParameters(this)
        {
            IgnoreCache = options?.IgnoreCache,
            Wait = options?.Wait
        };

        return Session.BrowsingContextModule.ReloadAsync(@params);
    }

    public Task ActivateAsync()
    {
        var @params = new ActivateCommandParameters(this);

        return Session.BrowsingContextModule.ActivateAsync(@params);
    }

    public async Task<IReadOnlyList<Script.NodeRemoteValue>> LocateNodesAsync(Locator locator, NodesOptions? options = default)
    {
        var @params = new LocateNodesCommandParameters(this, locator)
        {
            MaxNodeCount = options?.MaxNodeCount,
            SerializationOptions = options?.SerializationOptions,
            StartNodes = options?.StartNodes,
        };

        var result = await Session.BrowsingContextModule.LocateNodesAsync(@params).ConfigureAwait(false);

        return result.Nodes;
    }

    public Task PerformActionsAsync(IEnumerable<Input.SourceActions> actions)
    {
        var @params = new Input.PerformActionsCommandParameters(this)
        {
            Actions = actions
        };

        return Session.InputModule.PerformActionsAsync(@params);
    }

    public Task<CaptureScreenshotResult> CaptureScreenshotAsync(CaptureScreenshotOptions? options = default)
    {
        var @params = new CaptureScreenshotCommandParameters(this)
        {
            Origin = options?.Origin,
            Format = options?.Format,
            Clip = options?.Clip,
        };

        return Session.BrowsingContextModule.CaptureScreenshotAsync(@params);
    }

    public Task<Script.EvaluateResultSuccess> EvaluateAsync(string expression, bool awaitPromise, Script.EvaluateOptions? options = default)
    {
        var @params = new Script.EvaluateCommandParameters(expression, new Script.ContextTarget { Context = Id }, awaitPromise)
        {
            ResultOwnership = options?.ResultOwnership,
            SerializationOptions = options?.SerializationOptions,
            UserActivation = options?.UserActivation,
        };

        return Session.ScriptModule.EvaluateAsync(@params);
    }

    public Task<Script.EvaluateResultSuccess> CallFunctionAsync(string functionDeclaration, bool awaitPromise = true, Script.CallFunctionOptions? options = default)
    {
        var @params = new Script.CallFunctionCommandParameters(functionDeclaration, awaitPromise, new Script.ContextTarget { Context = Id })
        {
            Arguments = options?.Arguments,
            ResultOwnership = options?.ResultOwnership,
            SerializationOptions = options?.SerializationOptions,
            This = options?.This,
            UserActivation = options?.UserActivation
        };

        return Session.ScriptModule.CallFunctionAsync(@params);
    }

    public Task CloseAsync()
    {
        var @params = new CloseCommandParameters(this);

        return Session.BrowsingContextModule.CloseAsync(@params);
    }

    public Task TraverseHistoryAsync(int delta)
    {
        var @params = new TraverseHistoryCommandParameters(this, delta);

        return Session.BrowsingContextModule.TraverseHistoryAsync(@params);
    }

    public Task NavigateBackAsync()
    {
        return TraverseHistoryAsync(-1);
    }

    public Task NavigateForwardAsync()
    {
        return TraverseHistoryAsync(1);
    }

    public Task SetViewportAsync(SetViewportOptions? options = default)
    {
        var @params = new SetViewportCommandParameters(this)
        {
            Viewport = options?.Viewport,
            DevicePixelRatio = options?.DevicePixelRatio
        };

        return Session.BrowsingContextModule.SetViewportAsync(@params);
    }

    public async Task<string> PrintAsync(PrintOptions? options = default)
    {
        var @params = new PrintCommandParameters(this)
        {
            Background = options?.Background,
            Margin = options?.Margin,
            Orientation = options?.Orientation,
            Page = options?.Page,
            PageRanges = options?.PageRanges,
            Scale = options?.Scale,
            ShrinkToFit = options?.ShrinkToFit
        };

        var result = await Session.BrowsingContextModule.PrintAsync(@params).ConfigureAwait(false);

        return result.Data;
    }

    public Task<Network.Intercept> AddInterceptAsync(IEnumerable<Network.InterceptPhase> phases, IEnumerable<Network.UrlPattern>? urlPatterns = default)
    {
        var @params = new Network.AddInterceptCommandParameters(phases)
        {
            UrlPatterns = urlPatterns
        };

        return AddInterceptAsync(@params);
    }

    private async Task<Network.Intercept> AddInterceptAsync(Network.AddInterceptCommandParameters @params)
    {
        @params.Contexts = [this];

        var result = await Session.NetworkModule.AddInterceptAsync(@params).ConfigureAwait(false);

        return result.Intercept;
    }

    public Task HandleUserPromptAsync(HandleUserPromptOptions? options = default)
    {
        var @params = new HandleUserPromptCommandParameters(this)
        {
            Accept = options?.Accept,
            UserText = options?.UserText
        };

        return Session.BrowsingContextModule.HandleUserPrompAsync(@params);
    }

    public Task<Storage.GetCookiesResult> GetCookiesAsync(Storage.CookiesOptions? options = default)
    {
        var @params = new Storage.GetCookiesCommandParameters()
        {
            Filter = options?.Filter,
            Partition = new Storage.BrowsingContextPartitionDescriptor(this)
        };

        return Session.StorageModule.GetCookiesAsync(@params);
    }

    public async Task<Storage.PartitionKey> DeleteCookiesAsync(Storage.CookiesOptions? options = default)
    {
        var @params = new Storage.DeleteCookiesCommandParameters()
        {
            Filter = options?.Filter,
            Partition = new Storage.BrowsingContextPartitionDescriptor(this)
        };

        var res = await Session.StorageModule.DeleteCookiesAsync(@params).ConfigureAwait(false);

        return res.PartitionKey;
    }

    public async Task<Storage.PartitionKey> SetCookieAsync(string name, Network.BytesValue value, string domain, Storage.PartialCookieOptions options = default)
    {
        Storage.PartialCookie partialCookie = new(name, value, domain)
        {
            Path = options?.Path,
            HttpOnly = options?.HttpOnly,
            Secure = options?.Secure,
            SameSite = options?.SameSite,
            Expiry = options?.Expiry
        };

        var @params = new Storage.SetCookieCommandParameters(partialCookie)
        {
            Partition = new Storage.BrowsingContextPartitionDescriptor(this)
        };

        var res = await Session.StorageModule.SetCookieAsync(@params).ConfigureAwait(false);

        return res.PartitionKey;
    }

    public async Task<Script.PreloadScript> AddPreloadScriptAsync(string functionDeclaration, Script.PreloadScriptOptions? options = default)
    {
        var @params = new Script.AddPreloadScriptCommandParameters(functionDeclaration)
        {
            Contexts = [this],
            Arguments = options?.Arguments,
            Sandbox = options?.Sandbox
        };

        var res = await Session.ScriptModule.AddPreloadScriptAsync(@params).ConfigureAwait(false);

        return res.Script;
    }

    public async Task<IReadOnlyList<Script.RealmInfo>> GetRealmsAsync(Script.RealmsOptions? options = default)
    {
        var @params = new Script.GetRealmsCommandParameters
        {
            Context = this,
            Type = options?.Type
        };

        var res = await Session.ScriptModule.GetRealmAsync(@params).ConfigureAwait(false);

        return res.Realms;
    }

    public Task<Subscription> OnNavigationStartedAsync(Func<NavigationInfo, Task> callback)
    {
        return Session.BrowsingContextModule.OnNavigationStartedAsync(callback, this);
    }

    public Task<Subscription> OnNavigationStartedAsync(Action<NavigationInfo> callback)
    {
        return Session.BrowsingContextModule.OnNavigationStartedAsync(callback, this);
    }

    public Task<Subscription> OnFragmentNavigatedAsync(Func<NavigationInfo, Task> callback)
    {
        return Session.BrowsingContextModule.OnFragmentNavigatedAsync(callback, this);
    }

    public Task<Subscription> OnFragmentNavigatedAsync(Action<NavigationInfo> callback)
    {
        return Session.BrowsingContextModule.OnFragmentNavigatedAsync(callback, this);
    }

    public Task<Subscription> OnDomContentLoadedAsync(Func<NavigationInfo, Task> callback)
    {
        return Session.BrowsingContextModule.OnDomContentLoadedAsync(callback, this);
    }

    public Task<Subscription> OnLoadAsync(Action<NavigationInfo> callback)
    {
        return Session.BrowsingContextModule.OnLoadAsync(callback, this);
    }

    public Task<Subscription> OnLoadAsync(Func<NavigationInfo, Task> callback)
    {
        return Session.BrowsingContextModule.OnLoadAsync(callback, this);
    }

    public Task<Subscription> OnDownloadWillBeginAsync(Action<NavigationInfo> callback)
    {
        return Session.BrowsingContextModule.OnDownloadWillBeginAsync(callback, this);
    }

    public Task<Subscription> OnDownloadWillBeginAsync(Func<NavigationInfo, Task> callback)
    {
        return Session.BrowsingContextModule.OnDownloadWillBeginAsync(callback, this);
    }

    public Task<Subscription> OnNavigationAbortedAsync(Action<NavigationInfo> callback)
    {
        return Session.BrowsingContextModule.OnNavigationAbortedAsync(callback, this);
    }

    public Task<Subscription> OnNavigationAbortedAsync(Func<NavigationInfo, Task> callback)
    {
        return Session.BrowsingContextModule.OnNavigationAbortedAsync(callback, this);
    }

    public Task<Subscription> OnNavigationFailedAsync(Action<NavigationInfo> callback)
    {
        return Session.BrowsingContextModule.OnNavigationFailedAsync(callback, this);
    }

    public Task<Subscription> OnNavigationFailedAsync(Func<NavigationInfo, Task> callback)
    {
        return Session.BrowsingContextModule.OnNavigationFailedAsync(callback, this);
    }

    public Task<Subscription> OnDomContentLoadedAsync(Action<NavigationInfo> callback)
    {
        return Session.BrowsingContextModule.OnDomContentLoadedAsync(callback, this);
    }

    public Task<Subscription> OnBeforeRequestSentAsync(Func<Network.BeforeRequestSentEventArgs, Task> callback)
    {
        return Session.NetworkModule.OnBeforeRequestSentAsync(callback, this);
    }

    public Task<Subscription> OnBeforeRequestSentAsync(Action<Network.BeforeRequestSentEventArgs> callback)
    {
        return Session.NetworkModule.OnBeforeRequestSentAsync(callback, this);
    }

    public Task<Subscription> OnResponseStartedAsync(Func<Network.ResponseStartedEventArgs, Task> callback)
    {
        return Session.NetworkModule.OnResponseStartedAsync(callback, this);
    }

    public Task<Subscription> OnResponseStartedAsync(Action<Network.ResponseStartedEventArgs> callback)
    {
        return Session.NetworkModule.OnResponseStartedAsync(callback, this);
    }

    public Task<Subscription> OnResponseCompletedAsync(Func<Network.ResponseCompletedEventArgs, Task> callback)
    {
        return Session.NetworkModule.OnResponseCompletedAsync(callback, this);
    }

    public Task<Subscription> OnResponseCompletedAsync(Action<Network.ResponseCompletedEventArgs> callback)
    {
        return Session.NetworkModule.OnResponseCompletedAsync(callback, this);
    }

    public Task<Subscription> OnFetchErrorAsync(Func<Network.FetchErrorEventArgs, Task> callback)
    {
        return Session.NetworkModule.OnFetchErrorAsync(callback, this);
    }

    public Task<Subscription> OnFetchErrorAsync(Action<Network.FetchErrorEventArgs> callback)
    {
        return Session.NetworkModule.OnFetchErrorAsync(callback, this);
    }

    public Task<Subscription> OnLogEntryAddedAsync(Func<Log.BaseLogEntry, Task> callback)
    {
        return Session.LogModule.OnEntryAddedAsync(callback, this);
    }

    public Task<Subscription> OnLogEntryAddedAsync(Action<Log.BaseLogEntry> callback)
    {
        return Session.LogModule.OnEntryAddedAsync(callback, this);
    }
}
