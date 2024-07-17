using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class BrowsingContext
{
    internal BrowsingContext(BiDi.Session session, string id)
    {
        _session = session;
        Id = id;

        _logModule = new Lazy<BrowsingContextLogModule>(() => new BrowsingContextLogModule(this, _session.Log));
        _networkModule = new Lazy<BrowsingContextNetworkModule>(() => new BrowsingContextNetworkModule(this, _session.Network));
        _scriptModule = new Lazy<BrowsingContextScriptModule>(() => new BrowsingContextScriptModule(this, _session.ScriptModule));
        _storageModule = new Lazy<BrowsingContextStorageModule>(() => new BrowsingContextStorageModule(this, _session.Storage));
        _inputModule = new Lazy<BrowsingContextInputModule>(() => new BrowsingContextInputModule(this, _session.InputModule));
    }

    private readonly BiDi.Session _session;

    private readonly Lazy<BrowsingContextLogModule> _logModule;
    private readonly Lazy<BrowsingContextNetworkModule> _networkModule;
    private readonly Lazy<BrowsingContextScriptModule> _scriptModule;
    private readonly Lazy<BrowsingContextStorageModule> _storageModule;
    private readonly Lazy<BrowsingContextInputModule> _inputModule;

    internal string Id { get; }

    public BrowsingContextLogModule Log => _logModule.Value;

    public BrowsingContextNetworkModule Network => _networkModule.Value;

    public BrowsingContextScriptModule Script => _scriptModule.Value;

    public BrowsingContextStorageModule Storage => _storageModule.Value;

    public BrowsingContextInputModule Input => _inputModule.Value;

    public Task<NavigateResult> NavigateAsync(string url, NavigateOptions? options = default)
    {
        return _session.BrowsingContext.NavigateAsync(this, url, options);
    }

    public Task<NavigateResult> ReloadAsync(ReloadOptions? options = default)
    {
        return _session.BrowsingContext.ReloadAsync(this, options);
    }

    public Task ActivateAsync(ActivateOptions? options = default)
    {
        return _session.BrowsingContext.ActivateAsync(this, options);
    }

    public Task<IReadOnlyList<Script.NodeRemoteValue>> LocateNodesAsync(Locator locator, NodesOptions? options = default)
    {
        return _session.BrowsingContext.LocateNodesAsync(this, locator, options);
    }

    public Task<CaptureScreenshotResult> CaptureScreenshotAsync(CaptureScreenshotOptions? options = default)
    {
        return _session.BrowsingContext.CaptureScreenshotAsync(this, options);
    }

    public Task CloseAsync(CloseOptions? options = default)
    {
        return _session.BrowsingContext.CloseAsync(this, options);
    }

    public Task TraverseHistoryAsync(int delta, TraverseHistoryOptions? options = default)
    {
        return _session.BrowsingContext.TraverseHistoryAsync(this, delta, options);
    }

    public Task NavigateBackAsync(TraverseHistoryOptions? options = default)
    {
        return TraverseHistoryAsync(-1, options);
    }

    public Task NavigateForwardAsync(TraverseHistoryOptions? options = default)
    {
        return TraverseHistoryAsync(1, options);
    }

    public Task SetViewportAsync(ViewportOptions? options = default)
    {
        return _session.BrowsingContext.SetViewportAsync(this, options);
    }

    public async Task<string> PrintAsync(PrintOptions? options = default)
    {
        var result = await _session.BrowsingContext.PrintAsync(this, options).ConfigureAwait(false);

        return result.Data;
    }

    public Task HandleUserPromptAsync(UserPromptOptions? options = default)
    {
        return _session.BrowsingContext.HandleUserPromptAsync(this, options);
    }

    public Task<Subscription> OnNavigationStartedAsync(Func<NavigationInfo, Task> handler, SubscriptionOptions? options = default)
    {
        return _session.BrowsingContext.OnNavigationStartedAsync(handler, new BrowsingContextsSubscriptionOptions(options) { Contexts = [this] });
    }

    public Task<Subscription> OnNavigationStartedAsync(Action<NavigationInfo> handler, SubscriptionOptions? options = default)
    {
        return _session.BrowsingContext.OnNavigationStartedAsync(handler, new BrowsingContextsSubscriptionOptions(options) { Contexts = [this] });
    }

    public Task<Subscription> OnFragmentNavigatedAsync(Func<NavigationInfo, Task> handler, SubscriptionOptions? options = default)
    {
        return _session.BrowsingContext.OnFragmentNavigatedAsync(handler, new BrowsingContextsSubscriptionOptions(options) { Contexts = [this] });
    }

    public Task<Subscription> OnFragmentNavigatedAsync(Action<NavigationInfo> handler, SubscriptionOptions? options = default)
    {
        return _session.BrowsingContext.OnFragmentNavigatedAsync(handler, new BrowsingContextsSubscriptionOptions(options) { Contexts = [this] });
    }

    public Task<Subscription> OnDomContentLoadedAsync(Func<NavigationInfo, Task> handler, SubscriptionOptions? options = default)
    {
        return _session.BrowsingContext.OnDomContentLoadedAsync(handler, new BrowsingContextsSubscriptionOptions(options) { Contexts = [this] });
    }

    public Task<Subscription> OnLoadAsync(Action<NavigationInfo> handler, SubscriptionOptions? options = default)
    {
        return _session.BrowsingContext.OnLoadAsync(handler, new BrowsingContextsSubscriptionOptions(options) { Contexts = [this] });
    }

    public Task<Subscription> OnLoadAsync(Func<NavigationInfo, Task> handler, SubscriptionOptions? options = default)
    {
        return _session.BrowsingContext.OnLoadAsync(handler, new BrowsingContextsSubscriptionOptions(options) { Contexts = [this] });
    }

    public Task<Subscription> OnDownloadWillBeginAsync(Action<NavigationInfo> handler, SubscriptionOptions? options = default)
    {
        return _session.BrowsingContext.OnDownloadWillBeginAsync(handler, new BrowsingContextsSubscriptionOptions(options) { Contexts = [this] });
    }

    public Task<Subscription> OnDownloadWillBeginAsync(Func<NavigationInfo, Task> handler, SubscriptionOptions? options = default)
    {
        return _session.BrowsingContext.OnDownloadWillBeginAsync(handler, new BrowsingContextsSubscriptionOptions(options) { Contexts = [this] });
    }

    public Task<Subscription> OnNavigationAbortedAsync(Action<NavigationInfo> handler, SubscriptionOptions? options = default)
    {
        return _session.BrowsingContext.OnNavigationAbortedAsync(handler, new BrowsingContextsSubscriptionOptions(options) { Contexts = [this] });
    }

    public Task<Subscription> OnNavigationAbortedAsync(Func<NavigationInfo, Task> handler, SubscriptionOptions? options = default)
    {
        return _session.BrowsingContext.OnNavigationAbortedAsync(handler, new BrowsingContextsSubscriptionOptions(options) { Contexts = [this] });
    }

    public Task<Subscription> OnNavigationFailedAsync(Action<NavigationInfo> handler, SubscriptionOptions? options = default)
    {
        return _session.BrowsingContext.OnNavigationFailedAsync(handler, new BrowsingContextsSubscriptionOptions(options) { Contexts = [this] });
    }

    public Task<Subscription> OnNavigationFailedAsync(Func<NavigationInfo, Task> handler, SubscriptionOptions? options = default)
    {
        return _session.BrowsingContext.OnNavigationFailedAsync(handler, new BrowsingContextsSubscriptionOptions(options) { Contexts = [this] });
    }

    public Task<Subscription> OnDomContentLoadedAsync(Action<NavigationInfo> handler, SubscriptionOptions? options = default)
    {
        return _session.BrowsingContext.OnDomContentLoadedAsync(handler, new BrowsingContextsSubscriptionOptions(options) { Contexts = [this] });
    }

    public override bool Equals(object? obj)
    {
        if (obj is BrowsingContext browsingContextObj) return browsingContextObj.Id == Id;

        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
