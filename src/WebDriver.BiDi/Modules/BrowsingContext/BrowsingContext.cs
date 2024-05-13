using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
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

    public async Task<NavigateResult> NavigateAsync(string url, ReadinessState wait = ReadinessState.Complete)
    {
        var parameters = new NavigateCommandParameters { Url = url, Wait = wait };

        return await NavigateAsync(parameters).ConfigureAwait(false);
    }
    public async Task<NavigateResult> NavigateAsync(NavigateCommandParameters parameters)
    {
        parameters.Context = this;

        return await _session.BrowsingContextModule.NavigateAsync(parameters).ConfigureAwait(false);
    }

    public async Task<NavigateResult> ReloadAsync(bool? ignoreCache = default, ReadinessState? wait = default)
    {
        var parameters = new ReloadParameters
        {
            Context = this,
            IgnoreCache = ignoreCache,
            Wait = wait
        };

        return await _session.BrowsingContextModule.ReloadAsync(parameters).ConfigureAwait(false);
    }

    public async Task ActivateAsync()
    {
        var parameters = new ActivateParameters { Context = this };

        await _session.BrowsingContextModule.ActivateAsync(parameters).ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<Script.NodeRemoteValue>> LocateNodesAsync(Locator locator)
    {
        var parameters = new LocateNodesParameters
        {
            Locator = locator
        };

        return await LocateNodesAsync(parameters).ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<Script.NodeRemoteValue>> LocateNodesAsync(LocateNodesParameters parameters)
    {
        parameters.Context = Id;

        var result = await _session.BrowsingContextModule.LocateNodesAsync(parameters).ConfigureAwait(false);

        return result.Nodes;
    }

    public async Task PerformActionsAsync(List<Input.SourceActions> actions)
    {
        var parameters = new Input.PerformActionsParameters { Context = this, Actions = actions };

        await _session.Input.PerformActionsAsync(parameters).ConfigureAwait(false);
    }

    public async Task<CaptureScreenshotResult> CaptureScreenshotAsync(Origin? origin = default, ImageFormat? imageFormat = default, ClipRectangle? clip = default)
    {
        var parameters = new CaptureScreenshotCommandParameters
        {
            Origin = origin,
            Format = imageFormat,
            Clip = clip
        };

        return await CaptureScreenshotAsync(parameters).ConfigureAwait(false);
    }

    public async Task<CaptureScreenshotResult> CaptureScreenshotAsync(CaptureScreenshotCommandParameters parameters)
    {
        parameters.Context = Id;

        return await _session.BrowsingContextModule.CaptureScreenshotAsync(parameters).ConfigureAwait(false);
    }

    public async Task<Script.EvaluateResult> EvaluateAsync(string expression, bool awaitPromise)
    {
        var parameters = new Script.EvaluateCommandParameters { Expression = expression, Target = new Script.ContextTarget { Context = Id }, AwaitPromise = awaitPromise };
        return await _session.Script.EvaluateAsync(parameters).ConfigureAwait(false);
    }

    public async Task CloseAsync()
    {
        var parameters = new CloseCommandParameters { Context = this };

        await _session.BrowsingContextModule.CloseAsync(parameters).ConfigureAwait(false);
    }

    public async Task TraverseHistoryAsync(int delta)
    {
        var parameters = new TraverseHistoryParameters
        {
            Context = this,
            Delta = delta
        };

        await _session.BrowsingContextModule.TraverseHistoryAsync(parameters).ConfigureAwait(false);
    }

    public async Task NavigateBackAsync()
    {
        await TraverseHistoryAsync(-1).ConfigureAwait(false);
    }

    public async Task NavigateForwardAsync()
    {
        await TraverseHistoryAsync(1).ConfigureAwait(false);
    }

    public async Task SetViewportAsync(Viewport? viewport = default, double? devicePixelRatio = default)
    {
        var parameters = new SetViewportParameters
        {
            Context = this,
            Viewport = viewport,
            DevicePixelRatio = devicePixelRatio
        };

        await _session.BrowsingContextModule.SetViewportAsync(parameters).ConfigureAwait(false);
    }

    public async Task OnNavigationStartedAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        var syncContext = SynchronizationContext.Current;

        await _session.BrowsingContextModule.OnNavigationStartedAsync(callback, syncContext).ConfigureAwait(false);
    }

    public async Task OnNavigationStartedAsync(Action<NavigationInfoEventArgs> callback)
    {
        var syncContext = SynchronizationContext.Current;

        await _session.BrowsingContextModule.OnNavigationStartedAsync(callback, syncContext).ConfigureAwait(false);
    }
}
