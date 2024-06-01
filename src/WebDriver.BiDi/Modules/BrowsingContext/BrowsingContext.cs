﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class BrowsingContext
{
    private readonly BiDi.Session _session;

    public BrowsingContext(BiDi.Session session, string id)
    {
        _session = session;
        Id = id;
    }

    internal string Id { get; private set; }

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
        var @params = new NavigateCommand.Parameters { Url = url, Wait = wait };

        return NavigateAsync(@params);
    }

    private Task<NavigateResult> NavigateAsync(NavigateCommand.Parameters @params)
    {
        @params.Context = this;

        return _session.BrowsingContextModule.NavigateAsync(@params);
    }

    public Task<NavigateResult> ReloadAsync(bool? ignoreCache = default, ReadinessState? wait = default)
    {
        var @params = new ReloadCommand.Parameters
        {
            Context = this,
            IgnoreCache = ignoreCache,
            Wait = wait
        };

        return _session.BrowsingContextModule.ReloadAsync(@params);
    }

    public Task ActivateAsync()
    {
        var @params = new ActivateCommand.Parameters { Context = this };

        return _session.BrowsingContextModule.ActivateAsync(@params);
    }

    public Task<IReadOnlyList<Script.NodeRemoteValue>> LocateNodesAsync(Locator locator)
    {
        var @params = new LocateNodesCommand.Parameters
        {
            Locator = locator
        };

        return LocateNodesAsync(@params);
    }

    private async Task<IReadOnlyList<Script.NodeRemoteValue>> LocateNodesAsync(LocateNodesCommand.Parameters @params)
    {
        @params.Context = Id;

        var result = await _session.BrowsingContextModule.LocateNodesAsync(@params).ConfigureAwait(false);

        return result.Nodes;
    }

    public Task PerformActionsAsync(Input.SourceActions action)
    {
        return PerformActionsAsync([action]);
    }

    public Task PerformActionsAsync(List<Input.SourceActions> actions)
    {
        var @params = new Input.PerformActionsCommand.Parameters { Context = this, Actions = actions };

        return _session.Input.PerformActionsAsync(@params);
    }

    public Task<CaptureScreenshotResult> CaptureScreenshotAsync(Origin? origin = default, ImageFormat? imageFormat = default, ClipRectangle? clip = default)
    {
        var @params = new CaptureScreenshotCommand.Parameters
        {
            Origin = origin,
            Format = imageFormat,
            Clip = clip
        };

        return CaptureScreenshotAsync(@params);
    }

    private Task<CaptureScreenshotResult> CaptureScreenshotAsync(CaptureScreenshotCommand.Parameters @params)
    {
        @params.Context = Id;

        return _session.BrowsingContextModule.CaptureScreenshotAsync(@params);
    }

    public Task<Script.EvaluateResultSuccess> EvaluateAsync(string expression, bool awaitPromise = true)
    {
        var @params = new Script.EvaluateCommand.Parameters(expression, new Script.ContextTarget { Context = Id }, awaitPromise);

        return _session.Script.EvaluateAsync(@params);
    }

    public Task<Script.EvaluateResultSuccess> CallFunctionAsync(string functionDeclaration, params Script.LocalValue[] arguments)
    {
        return CallFunctionAsync(functionDeclaration, awaitPromise: true, arguments: arguments);
    }

    public Task<Script.EvaluateResultSuccess> CallFunctionAsync(string functionDeclaration, bool awaitPromise = true, IEnumerable<Script.LocalValue>? arguments = default)
    {
        var @params = new Script.CallFunctionCommand.Parameters(functionDeclaration, awaitPromise, new Script.ContextTarget { Context = Id })
        {
            Arguments = arguments
        };

        return _session.Script.CallFunctionAsync(@params);
    }

    public Task CloseAsync()
    {
        var @params = new CloseCommand.Parameters { Context = this };

        return _session.BrowsingContextModule.CloseAsync(@params);
    }

    public Task TraverseHistoryAsync(int delta)
    {
        var @params = new TraverseHistoryCommand.Parameters
        {
            Context = this,
            Delta = delta
        };

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
        var @params = new SetViewportCommand.Parameters
        {
            Context = this,
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
        var @params = new PrintCommand.Parameters(this)
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

    public async Task<Network.Intercept> OnBeforeRequestSentAsync(Network.UrlPattern urlPattern, Func<Network.BeforeRequestSentEventArgs, Task> callback)
    {
        var intercept = await AddInterceptAsync([Network.InterceptPhase.BeforeRequestSent], [urlPattern]).ConfigureAwait(false);

        await OnBeforeRequestSentAsync(async e =>
        {
            if (e.Intercepts?.Contains(intercept) is true)
            {
                await callback(e).ConfigureAwait(false);

                if (e.Intercepts?.Count == 1 && e.IsBlocked)
                {
                    await e.ContinueAsync().ConfigureAwait(false);
                }
            }
        }).ConfigureAwait(false);

        return intercept;
    }

    public async Task<Network.Intercept> OnResponseStartedAsync(Network.UrlPattern urlPattern, Func<Network.ResponseStartedEventArgs, Task> callback)
    {
        var intercept = await AddInterceptAsync([Network.InterceptPhase.ResponseStarted], [urlPattern]).ConfigureAwait(false);

        await OnResponseStartedAsync(async e =>
        {
            if (e.Intercepts?.Contains(intercept) is true)
            {
                await callback(e).ConfigureAwait(false);

                // TODO: register it as separate handler with low priority
                // to be executed at after all event handlers
                if (e.Intercepts?.Count == 1 && e.IsBlocked)
                {
                    await e.ContinueAsync().ConfigureAwait(false);
                }
            }
        }).ConfigureAwait(false);

        return intercept;
    }

    public Task<Network.Intercept> AddInterceptAsync(List<Network.InterceptPhase> phases, List<Network.UrlPattern>? urlPatterns = default)
    {
        var @params = new Network.AddInterceptCommand.Parameters
        {
            Phases = phases,
            UrlPatterns = urlPatterns
        };

        return AddInterceptAsync(@params);
    }

    private async Task<Network.Intercept> AddInterceptAsync(Network.AddInterceptCommand.Parameters @params)
    {
        @params.Contexts = [this];

        var result = await _session.Network.AddInterceptAsync(@params).ConfigureAwait(false);

        return result.Intercept;
    }

    public Task HandleUserPromptAsync(bool? accept = default, string? userText = default)
    {
        var @params = new HandleUserPromptCommand.Parameters(this)
        {
            Accept = accept,
            UserText = userText
        };

        return _session.BrowsingContextModule.HandleUserPrompAsync(@params);
    }

    public Task<Storage.GetCookiesResult> GetCookiesAsync(string? name = default, Network.BytesValue? value = default, string? domain = default, string? path = default, uint? size = default, bool? httpOnly = default, bool? secure = default, Network.SameSite sameSite = default, DateTime? expiry = default)
    {
        var @params = new Storage.GetCookiesCommand.Parameters()
        {
            Filter = new() { Name = name, Value = value, Domain = domain, Path = path, Size = size, HttpOnly = httpOnly, Secure = secure, SameSite = sameSite, Expiry = expiry },
            Partition = new Storage.BrowsingContextPartitionDescriptor(this)
        };

        return _session.Storage.GetCookiesAsync(@params);
    }

    public async Task<Storage.PartitionKey> DeleteCookiesAsync(string? name = default, Network.BytesValue? value = default, string? domain = default, string? path = default, uint? size = default, bool? httpOnly = default, bool? secure = default, Network.SameSite sameSite = default, DateTime? expiry = default)
    {
        var @params = new Storage.DeleteCookiesCommand.Parameters()
        {
            Filter = new() { Name = name, Value = value, Domain = domain, Path = path, Size = size, HttpOnly = httpOnly, Secure = secure, SameSite = sameSite, Expiry = expiry },
            Partition = new Storage.BrowsingContextPartitionDescriptor(this)
        };

        var res = await _session.Storage.DeleteCookiesAsync(@params).ConfigureAwait(false);

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

        var @params = new Storage.SetCookieCommand.Parameters(cookie)
        {
            Partition = new Storage.BrowsingContextPartitionDescriptor(this)
        };

        var res = await _session.Storage.SetCookieAsync(@params).ConfigureAwait(false);

        return res.PartitionKey;
    }

    public Task OnNavigationStartedAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        return _session.BrowsingContextModule.OnNavigationStartedAsync(callback);
    }

    public Task OnNavigationStartedAsync(Action<NavigationInfoEventArgs> callback)
    {
        return _session.BrowsingContextModule.OnNavigationStartedAsync(callback);
    }

    public Task OnFragmentNavigatedAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        return _session.BrowsingContextModule.OnFragmentNavigatedAsync(callback);
    }

    public Task OnFragmentNavigatedAsync(Action<NavigationInfoEventArgs> callback)
    {
        return _session.BrowsingContextModule.OnFragmentNavigatedAsync(callback);
    }

    public Task OnDomContentLoadedAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        return _session.BrowsingContextModule.OnDomContentLoadedAsync(callback);
    }

    public Task OnLoadAsync(Action<NavigationInfoEventArgs> callback)
    {
        return _session.BrowsingContextModule.OnLoadAsync(callback);
    }

    public Task OnLoadAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        return _session.BrowsingContextModule.OnLoadAsync(callback);
    }

    public Task OnDownloadWillBeginAsync(Action<NavigationInfoEventArgs> callback)
    {
        return _session.BrowsingContextModule.OnDownloadWillBeginAsync(callback);
    }

    public Task OnDownloadWillBeginAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        return _session.BrowsingContextModule.OnDownloadWillBeginAsync(callback);
    }

    public Task OnDomContentLoadedAsync(Action<NavigationInfoEventArgs> callback)
    {
        return _session.BrowsingContextModule.OnDomContentLoadedAsync(callback);
    }

    public Task OnBeforeRequestSentAsync(Func<Network.BeforeRequestSentEventArgs, Task> callback)
    {
        return _session.Network.OnBeforeRequestSentAsync(callback);
    }

    public Task OnBeforeRequestSentAsync(Action<Network.BeforeRequestSentEventArgs> callback)
    {
        return _session.Network.OnBeforeRequestSentAsync(callback);
    }

    public Task OnResponseStartedAsync(Func<Network.ResponseStartedEventArgs, Task> callback)
    {
        return _session.Network.OnResponseStartedAsync(callback);
    }

    public Task OnResponseStartedAsync(Action<Network.ResponseStartedEventArgs> callback)
    {
        return _session.Network.OnResponseStartedAsync(callback);
    }

    public Task OnLogEntryAddedAsync(Func<Log.LogEntryEventArgs, Task> callback)
    {
        return _session.Log.OnEntryAddedAsync(callback);
    }

    public Task OnLogEntryAddedAsync(Action<Log.LogEntryEventArgs> callback)
    {
        return _session.Log.OnEntryAddedAsync(callback);
    }
}
