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
        var @params = new NavigateCommandParameters(this, url);

        return Session.BrowsingContextModule.NavigateAsync(@params, options);
    }

    public Task<NavigateResult> ReloadAsync(ReloadOptions? options = default)
    {
        var @params = new ReloadCommandParameters(this);

        return Session.BrowsingContextModule.ReloadAsync(@params, options);
    }

    public Task ActivateAsync()
    {
        var @params = new ActivateCommandParameters(this);

        return Session.BrowsingContextModule.ActivateAsync(@params);
    }

    public async Task<IReadOnlyList<Script.NodeRemoteValue>> LocateNodesAsync(Locator locator, NodesOptions? options = default)
    {
        var @params = new LocateNodesCommandParameters(this, locator);

        var result = await Session.BrowsingContextModule.LocateNodesAsync(@params, options).ConfigureAwait(false);

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
        var @params = new CaptureScreenshotCommandParameters(this);

        return Session.BrowsingContextModule.CaptureScreenshotAsync(@params, options);
    }

    public Task<Script.EvaluateResultSuccess> EvaluateAsync(string expression, bool awaitPromise, Script.EvaluateOptions? options = default)
    {
        var @params = new Script.EvaluateCommandParameters(expression, new Script.ContextTarget { Context = Id }, awaitPromise);

        return Session.ScriptModule.EvaluateAsync(@params, options);
    }

    public Task<Script.EvaluateResultSuccess> CallFunctionAsync(string functionDeclaration, bool awaitPromise = true, Script.CallFunctionOptions? options = default)
    {
        var @params = new Script.CallFunctionCommandParameters(functionDeclaration, awaitPromise, new Script.ContextTarget { Context = Id });

        return Session.ScriptModule.CallFunctionAsync(@params, options);
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

    public Task SetViewportAsync(ViewportOptions? options = default)
    {
        var @params = new SetViewportCommandParameters(this);

        return Session.BrowsingContextModule.SetViewportAsync(@params, options);
    }

    public async Task<string> PrintAsync(PrintOptions? options = default)
    {
        var @params = new PrintCommandParameters(this);

        var result = await Session.BrowsingContextModule.PrintAsync(@params, options).ConfigureAwait(false);

        return result.Data;
    }

    public Task HandleUserPromptAsync(UserPromptOptions? options = default)
    {
        var @params = new HandleUserPromptCommandParameters(this);

        return Session.BrowsingContextModule.HandleUserPrompAsync(@params, options);
    }

    public Task<Storage.GetCookiesResult> GetCookiesAsync(Storage.CookiesOptions? options = default)
    {
        var @params = new Storage.GetCookiesCommandParameters();

        options ??= new();

        options.Partition = new Storage.BrowsingContextPartitionDescriptor(this);

        return Session.StorageModule.GetCookiesAsync(@params, options);
    }

    public async Task<Storage.PartitionKey> DeleteCookiesAsync(Storage.CookiesOptions? options = default)
    {
        var @params = new Storage.DeleteCookiesCommandParameters();

        options ??= new();

        options.Partition = new Storage.BrowsingContextPartitionDescriptor(this);

        var res = await Session.StorageModule.DeleteCookiesAsync(@params, options).ConfigureAwait(false);

        return res.PartitionKey;
    }

    public async Task<Storage.PartitionKey> SetCookieAsync(string name, Network.BytesValue value, string domain, Storage.PartialCookieOptions? options = default)
    {
        Storage.PartialCookie partialCookie = new(name, value, domain);

        var @params = new Storage.SetCookieCommandParameters(partialCookie)
        {
            Partition = new Storage.BrowsingContextPartitionDescriptor(this)
        };

        var res = await Session.StorageModule.SetCookieAsync(@params, options).ConfigureAwait(false);

        return res.PartitionKey;
    }

    public async Task<Script.PreloadScript> AddPreloadScriptAsync(string functionDeclaration, Script.PreloadScriptOptions? options = default)
    {
        var @params = new Script.AddPreloadScriptCommandParameters(functionDeclaration);

        options ??= new();

        options.Contexts = [this];

        var res = await Session.ScriptModule.AddPreloadScriptAsync(@params, options).ConfigureAwait(false);

        return res.Script;
    }

    public async Task<IReadOnlyList<Script.RealmInfo>> GetRealmsAsync(Script.RealmsOptions? options = default)
    {
        var @params = new Script.GetRealmsCommandParameters();

        options ??= new();

        options.Context = this;

        var res = await Session.ScriptModule.GetRealmAsync(@params, options).ConfigureAwait(false);

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
