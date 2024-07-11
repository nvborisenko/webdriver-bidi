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

        _logModule = new Lazy<BrowsingContextLogModule>(() => new BrowsingContextLogModule(this, Session.Log));
        _networkModule = new Lazy<BrowsingContextNetworkModule>(() => new BrowsingContextNetworkModule(this, Session.Network));
        _scriptModule = new Lazy<BrowsingContextScriptModule>(() => new BrowsingContextScriptModule(this, Session.ScriptModule));
        _storageModule = new Lazy<BrowsingContextStorageModule>(() => new BrowsingContextStorageModule(this, Session.Storage));
    }

    internal BiDi.Session Session { get; }

    internal string Id { get; }

    private readonly Lazy<BrowsingContextLogModule> _logModule;
    private readonly Lazy<BrowsingContextNetworkModule> _networkModule;
    private readonly Lazy<BrowsingContextScriptModule> _scriptModule;
    private readonly Lazy<BrowsingContextStorageModule> _storageModule;

    public BrowsingContextLogModule Log => _logModule.Value;

    public BrowsingContextNetworkModule Network => _networkModule.Value;

    public BrowsingContextScriptModule Script => _scriptModule.Value;

    public BrowsingContextStorageModule Storage => _storageModule.Value;

    public Task<NavigateResult> NavigateAsync(string url, NavigateOptions? options = default)
    {
        return Session.BrowsingContext.NavigateAsync(this, url, options);
    }

    public Task<NavigateResult> ReloadAsync(ReloadOptions? options = default)
    {
        return Session.BrowsingContext.ReloadAsync(this, options);
    }

    public Task ActivateAsync(ActivateOptions? options = default)
    {
        return Session.BrowsingContext.ActivateAsync(this, options);
    }

    public Task<IReadOnlyList<Script.NodeRemoteValue>> LocateNodesAsync(Locator locator, NodesOptions? options = default)
    {
        return Session.BrowsingContext.LocateNodesAsync(this, locator, options);
    }

    public Task PerformActionsAsync(IEnumerable<Input.SourceActions> actions, Input.PerformActionsOptions? options = default)
    {
        options ??= new();

        options.Actions = actions;

        return Session.InputModule.PerformActionsAsync(this, options);
    }

    public Task<CaptureScreenshotResult> CaptureScreenshotAsync(CaptureScreenshotOptions? options = default)
    {
        return Session.BrowsingContext.CaptureScreenshotAsync(this, options);
    }

    public Task CloseAsync(CloseOptions? options = default)
    {
        return Session.BrowsingContext.CloseAsync(this, options);
    }

    public Task TraverseHistoryAsync(int delta, TraverseHistoryOptions? options = default)
    {
        return Session.BrowsingContext.TraverseHistoryAsync(this, delta, options);
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
        return Session.BrowsingContext.SetViewportAsync(this, options);
    }

    public async Task<string> PrintAsync(PrintOptions? options = default)
    {
        var result = await Session.BrowsingContext.PrintAsync(this, options).ConfigureAwait(false);

        return result.Data;
    }

    public Task HandleUserPromptAsync(UserPromptOptions? options = default)
    {
        return Session.BrowsingContext.HandleUserPrompAsync(this, options);
    }

    public Task<Subscription> OnNavigationStartedAsync(Func<NavigationInfo, Task> callback)
    {
        return Session.BrowsingContext.OnNavigationStartedAsync(callback, this);
    }

    public Task<Subscription> OnNavigationStartedAsync(Action<NavigationInfo> callback)
    {
        return Session.BrowsingContext.OnNavigationStartedAsync(callback, this);
    }

    public Task<Subscription> OnFragmentNavigatedAsync(Func<NavigationInfo, Task> callback)
    {
        return Session.BrowsingContext.OnFragmentNavigatedAsync(callback, this);
    }

    public Task<Subscription> OnFragmentNavigatedAsync(Action<NavigationInfo> callback)
    {
        return Session.BrowsingContext.OnFragmentNavigatedAsync(callback, this);
    }

    public Task<Subscription> OnDomContentLoadedAsync(Func<NavigationInfo, Task> callback)
    {
        return Session.BrowsingContext.OnDomContentLoadedAsync(callback, this);
    }

    public Task<Subscription> OnLoadAsync(Action<NavigationInfo> callback)
    {
        return Session.BrowsingContext.OnLoadAsync(callback, this);
    }

    public Task<Subscription> OnLoadAsync(Func<NavigationInfo, Task> callback)
    {
        return Session.BrowsingContext.OnLoadAsync(callback, this);
    }

    public Task<Subscription> OnDownloadWillBeginAsync(Action<NavigationInfo> callback)
    {
        return Session.BrowsingContext.OnDownloadWillBeginAsync(callback, this);
    }

    public Task<Subscription> OnDownloadWillBeginAsync(Func<NavigationInfo, Task> callback)
    {
        return Session.BrowsingContext.OnDownloadWillBeginAsync(callback, this);
    }

    public Task<Subscription> OnNavigationAbortedAsync(Action<NavigationInfo> callback)
    {
        return Session.BrowsingContext.OnNavigationAbortedAsync(callback, this);
    }

    public Task<Subscription> OnNavigationAbortedAsync(Func<NavigationInfo, Task> callback)
    {
        return Session.BrowsingContext.OnNavigationAbortedAsync(callback, this);
    }

    public Task<Subscription> OnNavigationFailedAsync(Action<NavigationInfo> callback)
    {
        return Session.BrowsingContext.OnNavigationFailedAsync(callback, this);
    }

    public Task<Subscription> OnNavigationFailedAsync(Func<NavigationInfo, Task> callback)
    {
        return Session.BrowsingContext.OnNavigationFailedAsync(callback, this);
    }

    public Task<Subscription> OnDomContentLoadedAsync(Action<NavigationInfo> callback)
    {
        return Session.BrowsingContext.OnDomContentLoadedAsync(callback, this);
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
