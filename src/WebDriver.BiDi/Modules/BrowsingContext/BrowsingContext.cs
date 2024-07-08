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
        return Session.BrowsingContextModule.NavigateAsync(this, url, options);
    }

    public Task<NavigateResult> ReloadAsync(ReloadOptions? options = default)
    {
        return Session.BrowsingContextModule.ReloadAsync(this, options);
    }

    public Task ActivateAsync()
    {
        return Session.BrowsingContextModule.ActivateAsync(this);
    }

    public async Task<IReadOnlyList<Script.NodeRemoteValue>> LocateNodesAsync(Locator locator, NodesOptions? options = default)
    {
        var result = await Session.BrowsingContextModule.LocateNodesAsync(this, locator, options).ConfigureAwait(false);

        return result.Nodes;
    }

    public Task PerformActionsAsync(IEnumerable<Input.SourceActions> actions)
    {
        var options = new Input.PerformActionsOptions
        {
            Actions = actions
        };

        return Session.InputModule.PerformActionsAsync(this, options);
    }

    public Task<CaptureScreenshotResult> CaptureScreenshotAsync(CaptureScreenshotOptions? options = default)
    {
        return Session.BrowsingContextModule.CaptureScreenshotAsync(this, options);
    }

    public Task<Script.EvaluateResultSuccess> EvaluateAsync(string expression, bool awaitPromise, Script.EvaluateOptions? options = default)
    {
        return Session.ScriptModule.EvaluateAsync(expression, awaitPromise, new Script.ContextTarget { Context = Id }, options);
    }

    public Task<Script.EvaluateResultSuccess> CallFunctionAsync(string functionDeclaration, bool awaitPromise, Script.CallFunctionOptions? options = default)
    {
        return Session.ScriptModule.CallFunctionAsync(functionDeclaration, awaitPromise, new Script.ContextTarget { Context = Id }, options);
    }

    public Task CloseAsync()
    {
        return Session.BrowsingContextModule.CloseAsync(this);
    }

    public Task TraverseHistoryAsync(int delta)
    {
        return Session.BrowsingContextModule.TraverseHistoryAsync(this, delta);
    }

    public Task NavigateBackAsync()
    {
        return TraverseHistoryAsync(-1);
    }

    public Task NavigateForwardAsync()
    {
        return TraverseHistoryAsync(1);
    }

    public Task SetViewportAsync(ViewportOptions? options = default)
    {
        return Session.BrowsingContextModule.SetViewportAsync(this, options);
    }

    public async Task<string> PrintAsync(PrintOptions? options = default)
    {
        var result = await Session.BrowsingContextModule.PrintAsync(this, options).ConfigureAwait(false);

        return result.Data;
    }

    public Task HandleUserPromptAsync(UserPromptOptions? options = default)
    {
        return Session.BrowsingContextModule.HandleUserPrompAsync(this, options);
    }

    public Task<Storage.GetCookiesResult> GetCookiesAsync(Storage.CookiesOptions? options = default)
    {
        options ??= new();

        options.Partition = new Storage.BrowsingContextPartitionDescriptor(this);

        return Session.StorageModule.GetCookiesAsync(options);
    }

    public async Task<Storage.PartitionKey> DeleteCookiesAsync(Storage.CookiesOptions? options = default)
    {
        options ??= new();

        options.Partition = new Storage.BrowsingContextPartitionDescriptor(this);

        var res = await Session.StorageModule.DeleteCookiesAsync(options).ConfigureAwait(false);

        return res.PartitionKey;
    }

    public async Task<Storage.PartitionKey> SetCookieAsync(Storage.PartialCookie cookie, Storage.SetCookieOptions? options = default)
    {
        options ??= new();

        options.Partition = new Storage.BrowsingContextPartitionDescriptor(this);

        var res = await Session.StorageModule.SetCookieAsync(cookie, options).ConfigureAwait(false);

        return res.PartitionKey;
    }

    public async Task<Script.PreloadScript> AddPreloadScriptAsync(string functionDeclaration, Script.PreloadScriptOptions? options = default)
    {
        options ??= new();

        options.Contexts = [this];

        var res = await Session.ScriptModule.AddPreloadScriptAsync(functionDeclaration, options).ConfigureAwait(false);

        return res.Script;
    }

    public async Task<IReadOnlyList<Script.RealmInfo>> GetRealmsAsync(Script.RealmsOptions? options = default)
    {
        options ??= new();

        options.Context = this;

        var res = await Session.ScriptModule.GetRealmAsync(options).ConfigureAwait(false);

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
