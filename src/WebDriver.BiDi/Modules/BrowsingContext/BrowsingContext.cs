﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class BrowsingContext
{
    readonly BiDi.Session _session;

    internal BrowsingContext(BiDi.Session session, string id)
    {
        _session = session;
        Id = id;
    }

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

    public Task<NavigateResult> NavigateAsync(string url, ReadinessState wait = ReadinessState.Complete)
    {
        var @params = new NavigateCommandParameters(this, url) { Wait = wait };

        return _session.BrowsingContextModule.NavigateAsync(@params);
    }

    public Task<NavigateResult> ReloadAsync(bool? ignoreCache = default, ReadinessState? wait = default)
    {
        var @params = new ReloadCommandParameters(this)
        {
            IgnoreCache = ignoreCache,
            Wait = wait
        };

        return _session.BrowsingContextModule.ReloadAsync(@params);
    }

    public Task ActivateAsync()
    {
        var @params = new ActivateCommandParameters(this);

        return _session.BrowsingContextModule.ActivateAsync(@params);
    }

    public async Task<IReadOnlyList<Script.NodeRemoteValue>> LocateNodesAsync(Locator locator)
    {
        var @params = new LocateNodesCommandParameters(this, locator);

        var result = await _session.BrowsingContextModule.LocateNodesAsync(@params).ConfigureAwait(false);

        return result.Nodes;
    }

    public Task PerformActionsAsync(Input.SourceActions action)
    {
        return PerformActionsAsync([action]);
    }

    public Task PerformActionsAsync(List<Input.SourceActions> actions)
    {
        var @params = new Input.PerformActionsCommandParameters { Context = this, Actions = actions };

        return _session.InputModule.PerformActionsAsync(@params);
    }

    public Task<CaptureScreenshotResult> CaptureScreenshotAsync(Origin? origin = default, ImageFormat? imageFormat = default, ClipRectangle? clip = default)
    {
        var @params = new CaptureScreenshotCommandParameters(this)
        {
            Origin = origin,
            Format = imageFormat,
            Clip = clip
        };

        return _session.BrowsingContextModule.CaptureScreenshotAsync(@params);
    }

    public Task<Script.EvaluateResultSuccess> EvaluateAsync(string expression, bool awaitPromise = true)
    {
        var @params = new Script.EvaluateCommandParameters(expression, new Script.ContextTarget { Context = Id }, awaitPromise);

        return _session.ScriptModule.EvaluateAsync(@params);
    }

    public Task<Script.EvaluateResultSuccess> CallFunctionAsync(string functionDeclaration, params Script.LocalValue[] arguments)
    {
        return CallFunctionAsync(functionDeclaration, awaitPromise: true, arguments: arguments);
    }

    public Task<Script.EvaluateResultSuccess> CallFunctionAsync(string functionDeclaration, bool awaitPromise = true, IEnumerable<Script.LocalValue>? arguments = default)
    {
        var @params = new Script.CallFunctionCommandParameters(functionDeclaration, awaitPromise, new Script.ContextTarget { Context = Id })
        {
            Arguments = arguments
        };

        return _session.ScriptModule.CallFunctionAsync(@params);
    }

    public Task CloseAsync()
    {
        var @params = new CloseCommandParameters(this);

        return _session.BrowsingContextModule.CloseAsync(@params);
    }

    public Task TraverseHistoryAsync(int delta)
    {
        var @params = new TraverseHistoryCommandParameters(this, delta);

        return _session.BrowsingContextModule.TraverseHistoryAsync(@params);
    }

    public Task NavigateBackAsync()
    {
        return TraverseHistoryAsync(-1);
    }

    public Task NavigateForwardAsync()
    {
        return TraverseHistoryAsync(1);
    }

    public Task SetViewportAsync(uint width, uint height, double? devicePixelRatio = default)
    {
        var @params = new SetViewportCommandParameters(this)
        {
            Viewport = new(width, height),
            DevicePixelRatio = devicePixelRatio
        };

        return _session.BrowsingContextModule.SetViewportAsync(@params);
    }

    public Task<IReadOnlyList<BrowsingContextInfo>> GetTreeAsync(uint? maxDepth = default)
    {
        return _session.GetTreeAsync(maxDepth, this);
    }

    public async Task<string> PrintAsync(bool? background = default, Margin? margin = default, Orientation? orientation = default, Page? page = default, IEnumerable<uint>? pageRanges = default, double? scale = default, bool? shrinkToFit = default)
    {
        var @params = new PrintCommandParameters(this)
        {
            Background = background,
            Margin = margin,
            Orientation = orientation,
            Page = page,
            PageRanges = pageRanges,
            Scale = scale,
            ShrinkToFit = shrinkToFit
        };

        var result = await _session.BrowsingContextModule.PrintAsync(@params).ConfigureAwait(false);

        return result.Data;
    }

    public Task<Network.Intercept> AddInterceptAsync(Network.InterceptPhase phase, IEnumerable<Network.UrlPattern>? urlPatterns = default)
    {
        return AddInterceptAsync([phase], urlPatterns);
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

        var result = await _session.NetworkModule.AddInterceptAsync(@params).ConfigureAwait(false);

        return result.Intercept;
    }

    public Task HandleUserPromptAsync(bool? accept = default, string? userText = default)
    {
        var @params = new HandleUserPromptCommandParameters(this)
        {
            Accept = accept,
            UserText = userText
        };

        return _session.BrowsingContextModule.HandleUserPrompAsync(@params);
    }

    public Task<Storage.GetCookiesResult> GetCookiesAsync(string? name = default, Network.BytesValue? value = default, string? domain = default, string? path = default, uint? size = default, bool? httpOnly = default, bool? secure = default, Network.SameSite sameSite = default, DateTime? expiry = default)
    {
        var @params = new Storage.GetCookiesCommandParameters()
        {
            Filter = new() { Name = name, Value = value, Domain = domain, Path = path, Size = size, HttpOnly = httpOnly, Secure = secure, SameSite = sameSite, Expiry = expiry },
            Partition = new Storage.BrowsingContextPartitionDescriptor(this)
        };

        return _session.StorageModule.GetCookiesAsync(@params);
    }

    public async Task<Storage.PartitionKey> DeleteCookiesAsync(string? name = default, Network.BytesValue? value = default, string? domain = default, string? path = default, uint? size = default, bool? httpOnly = default, bool? secure = default, Network.SameSite sameSite = default, DateTime? expiry = default)
    {
        var @params = new Storage.DeleteCookiesCommandParameters()
        {
            Filter = new() { Name = name, Value = value, Domain = domain, Path = path, Size = size, HttpOnly = httpOnly, Secure = secure, SameSite = sameSite, Expiry = expiry },
            Partition = new Storage.BrowsingContextPartitionDescriptor(this)
        };

        var res = await _session.StorageModule.DeleteCookiesAsync(@params).ConfigureAwait(false);

        return res.PartitionKey;
    }

    public async Task<Storage.PartitionKey> SetCookieAsync(string name, Network.BytesValue value, string domain, string? path = default, bool? httpOnly = default, bool? secure = default, Network.SameSite sameSite = default, DateTime? expiry = default)
    {
        Storage.PartialCookie cookie = new(name, value, domain)
        {
            Path = path,
            HttpOnly = httpOnly,
            Secure = secure,
            SameSite = sameSite,
            Expiry = expiry
        };

        var @params = new Storage.SetCookieCommandParameters(cookie)
        {
            Partition = new Storage.BrowsingContextPartitionDescriptor(this)
        };

        var res = await _session.StorageModule.SetCookieAsync(@params).ConfigureAwait(false);

        return res.PartitionKey;
    }

    public async Task<Script.PreloadScript> AddPreloadScriptAsync(string functionDeclaration)
    {
        var @params = new Script.AddPreloadScriptCommandParameters(functionDeclaration)
        {
            Contexts = [this]
        };

        var res = await _session.ScriptModule.AddPreloadScriptAsync(@params).ConfigureAwait(false);

        return res.Script;
    }

    public async Task<IReadOnlyList<Script.RealmInfoEventArgs>> GetRealmsAsync()
    {
        var @params = new Script.GetRealmsCommandParameters { Context = this };

        var res = await _session.ScriptModule.GetRealmAsync(@params).ConfigureAwait(false);

        return res.Realms;
    }

    public Task<Subscription> OnNavigationStartedAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        return _session.BrowsingContextModule.OnNavigationStartedAsync(callback, this);
    }

    public Task<Subscription> OnNavigationStartedAsync(Action<NavigationInfoEventArgs> callback)
    {
        return _session.BrowsingContextModule.OnNavigationStartedAsync(callback, this);
    }

    public Task<Subscription> OnFragmentNavigatedAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        return _session.BrowsingContextModule.OnFragmentNavigatedAsync(callback, this);
    }

    public Task<Subscription> OnFragmentNavigatedAsync(Action<NavigationInfoEventArgs> callback)
    {
        return _session.BrowsingContextModule.OnFragmentNavigatedAsync(callback, this);
    }

    public Task<Subscription> OnDomContentLoadedAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        return _session.BrowsingContextModule.OnDomContentLoadedAsync(callback, this);
    }

    public Task<Subscription> OnLoadAsync(Action<NavigationInfoEventArgs> callback)
    {
        return _session.BrowsingContextModule.OnLoadAsync(callback, this);
    }

    public Task<Subscription> OnLoadAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        return _session.BrowsingContextModule.OnLoadAsync(callback, this);
    }

    public Task<Subscription> OnDownloadWillBeginAsync(Action<NavigationInfoEventArgs> callback)
    {
        return _session.BrowsingContextModule.OnDownloadWillBeginAsync(callback, this);
    }

    public Task<Subscription> OnDownloadWillBeginAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        return _session.BrowsingContextModule.OnDownloadWillBeginAsync(callback, this);
    }

    public Task<Subscription> OnNavigationAbortedAsync(Action<NavigationInfoEventArgs> callback)
    {
        return _session.BrowsingContextModule.OnNavigationAbortedAsync(callback, this);
    }

    public Task<Subscription> OnNavigationAbortedAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        return _session.BrowsingContextModule.OnNavigationAbortedAsync(callback, this);
    }

    public Task<Subscription> OnNavigationFailedAsync(Action<NavigationInfoEventArgs> callback)
    {
        return _session.BrowsingContextModule.OnNavigationFailedAsync(callback, this);
    }

    public Task<Subscription> OnNavigationFailedAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        return _session.BrowsingContextModule.OnNavigationFailedAsync(callback, this);
    }

    public Task<Subscription> OnDomContentLoadedAsync(Action<NavigationInfoEventArgs> callback)
    {
        return _session.BrowsingContextModule.OnDomContentLoadedAsync(callback, this);
    }

    public Task<Subscription> OnBeforeRequestSentAsync(Func<Network.BeforeRequestSentEventArgs, Task> callback)
    {
        return _session.NetworkModule.OnBeforeRequestSentAsync(callback, this);
    }

    public Task<Subscription> OnBeforeRequestSentAsync(Action<Network.BeforeRequestSentEventArgs> callback)
    {
        return _session.NetworkModule.OnBeforeRequestSentAsync(callback, this);
    }

    public Task<Subscription> OnResponseStartedAsync(Func<Network.ResponseStartedEventArgs, Task> callback)
    {
        return _session.NetworkModule.OnResponseStartedAsync(callback, this);
    }

    public Task<Subscription> OnResponseStartedAsync(Action<Network.ResponseStartedEventArgs> callback)
    {
        return _session.NetworkModule.OnResponseStartedAsync(callback, this);
    }

    public Task<Subscription> OnResponseCompletedAsync(Func<Network.ResponseCompletedEventArgs, Task> callback)
    {
        return _session.NetworkModule.OnResponseCompletedAsync(callback, this);
    }

    public Task<Subscription> OnResponseCompletedAsync(Action<Network.ResponseCompletedEventArgs> callback)
    {
        return _session.NetworkModule.OnResponseCompletedAsync(callback, this);
    }

    public Task<Subscription> OnFetchErrorAsync(Func<Network.FetchErrorEventArgs, Task> callback)
    {
        return _session.NetworkModule.OnFetchErrorAsync(callback, this);
    }

    public Task<Subscription> OnFetchErrorAsync(Action<Network.FetchErrorEventArgs> callback)
    {
        return _session.NetworkModule.OnFetchErrorAsync(callback, this);
    }

    public Task<Subscription> OnLogEntryAddedAsync(Func<Log.BaseLogEntryEventArgs, Task> callback)
    {
        return _session.LogModule.OnEntryAddedAsync(callback, this);
    }

    public Task<Subscription> OnLogEntryAddedAsync(Action<Log.BaseLogEntryEventArgs> callback)
    {
        return _session.LogModule.OnEntryAddedAsync(callback, this);
    }
}
