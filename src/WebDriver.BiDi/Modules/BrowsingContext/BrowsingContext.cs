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

    public Task<NavigateResult> NavigateAsync(string url, ReadinessState wait = ReadinessState.Complete)
    {
        var parameters = new NavigateCommandParameters { Url = url, Wait = wait };

        return NavigateAsync(parameters);
    }
    public Task<NavigateResult> NavigateAsync(NavigateCommandParameters parameters)
    {
        parameters.Context = this;

        return _session.BrowsingContextModule.NavigateAsync(parameters);
    }

    public Task<NavigateResult> ReloadAsync(bool? ignoreCache = default, ReadinessState? wait = default)
    {
        var parameters = new ReloadParameters
        {
            Context = this,
            IgnoreCache = ignoreCache,
            Wait = wait
        };

        return _session.BrowsingContextModule.ReloadAsync(parameters);
    }

    public Task ActivateAsync()
    {
        var parameters = new ActivateParameters { Context = this };

        return _session.BrowsingContextModule.ActivateAsync(parameters);
    }

    public Task<IReadOnlyList<Script.NodeRemoteValue>> LocateNodesAsync(Locator locator)
    {
        var parameters = new LocateNodesParameters
        {
            Locator = locator
        };

        return LocateNodesAsync(parameters);
    }

    public async Task<IReadOnlyList<Script.NodeRemoteValue>> LocateNodesAsync(LocateNodesParameters parameters)
    {
        parameters.Context = Id;

        var result = await _session.BrowsingContextModule.LocateNodesAsync(parameters).ConfigureAwait(false);

        return result.Nodes;
    }

    public Task PerformActionsAsync(List<Input.SourceActions> actions)
    {
        var parameters = new Input.PerformActionsParameters { Context = this, Actions = actions };

        return _session.Input.PerformActionsAsync(parameters);
    }

    public Task<CaptureScreenshotResult> CaptureScreenshotAsync(Origin? origin = default, ImageFormat? imageFormat = default, ClipRectangle? clip = default)
    {
        var parameters = new CaptureScreenshotCommandParameters
        {
            Origin = origin,
            Format = imageFormat,
            Clip = clip
        };

        return CaptureScreenshotAsync(parameters);
    }

    public Task<CaptureScreenshotResult> CaptureScreenshotAsync(CaptureScreenshotCommandParameters parameters)
    {
        parameters.Context = Id;

        return _session.BrowsingContextModule.CaptureScreenshotAsync(parameters);
    }

    public Task<Script.EvaluateResult> EvaluateAsync(string expression, bool awaitPromise)
    {
        var parameters = new Script.EvaluateCommandParameters { Expression = expression, Target = new Script.ContextTarget { Context = Id }, AwaitPromise = awaitPromise };
        return _session.Script.EvaluateAsync(parameters);
    }

    public Task CloseAsync()
    {
        var parameters = new CloseCommandParameters { Context = this };

        return _session.BrowsingContextModule.CloseAsync(parameters);
    }

    public Task TraverseHistoryAsync(int delta)
    {
        var parameters = new TraverseHistoryParameters
        {
            Context = this,
            Delta = delta
        };

        return _session.BrowsingContextModule.TraverseHistoryAsync(parameters);
    }

    public Task NavigateBackAsync()
    {
        return TraverseHistoryAsync(-1);
    }

    public Task NavigateForwardAsync()
    {
        return TraverseHistoryAsync(1);
    }

    public Task SetViewportAsync(Viewport? viewport = default, double? devicePixelRatio = default)
    {
        var parameters = new SetViewportParameters
        {
            Context = this,
            Viewport = viewport,
            DevicePixelRatio = devicePixelRatio
        };

        return _session.BrowsingContextModule.SetViewportAsync(parameters);
    }

    public Task OnNavigationStartedAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        var syncContext = SynchronizationContext.Current;

        return _session.BrowsingContextModule.OnNavigationStartedAsync(callback, syncContext);
    }

    public Task OnNavigationStartedAsync(Action<NavigationInfoEventArgs> callback)
    {
        var syncContext = SynchronizationContext.Current;

        return _session.BrowsingContextModule.OnNavigationStartedAsync(callback, syncContext);
    }
}
