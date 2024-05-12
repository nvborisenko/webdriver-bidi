using System;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public sealed class BrowsingContextModule
{
    private readonly BiDi.Session _session;
    private readonly Broker _broker;

    internal BrowsingContextModule(BiDi.Session session, Broker broker)
    {
        _session = session;
        _broker = broker;
    }

    public async Task<CreateResult> CreateAsync(CreateCommandParameters parameters)
    {
        return await _broker.ExecuteCommandAsync<CreateCommand, CreateResult>(new CreateCommand { Params = parameters }).ConfigureAwait(false);
    }

    public async Task<NavigateResult> NavigateAsync(string url, ReadinessState wait = ReadinessState.Complete)
    {
        var parameters = new NavigateCommandParameters { Url = url, Wait = wait };

        return await NavigateAsync(parameters).ConfigureAwait(false);
    }

    public async Task<NavigateResult> NavigateAsync(NavigateCommandParameters parameters)
    {
        return await _broker.ExecuteCommandAsync<NavigateCommand, NavigateResult>(new NavigateCommand { Params = parameters }).ConfigureAwait(false);
    }

    public async Task ActivateAsync(ActivateParameters parameters)
    {
        await _broker.ExecuteCommandAsync(new ActivateCommand { Params = parameters }).ConfigureAwait(false);
    }

    public async Task<LocateNodesResult> LocateNodesAsync(LocateNodesParameters parameters)
    {
        return await _broker.ExecuteCommandAsync<LocateNodesCommand, LocateNodesResult>(new LocateNodesCommand { Params = parameters }).ConfigureAwait(false);
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
        return await _broker.ExecuteCommandAsync<CaptureScreenshotCommand, CaptureScreenshotResult>(new CaptureScreenshotCommand { Params = parameters }).ConfigureAwait(false);
    }

    public async Task CloseAsync(CloseCommandParameters parameters)
    {
        await _broker.ExecuteCommandAsync(new CloseCommand { Params = parameters }).ConfigureAwait(false);
    }

    public async Task<TraverseHistoryResult> TraverseHistoryAsync(TraverseHistoryParameters parameters)
    {
        return await _broker.ExecuteCommandAsync<TraverseHistoryCommand, TraverseHistoryResult>(new TraverseHistoryCommand { Params = parameters }).ConfigureAwait(false);
    }

    public async Task SetViewportAsync(SetViewportParameters parameters)
    {
        await _broker.ExecuteCommandAsync(new SetViewportCommand { Params = parameters }).ConfigureAwait(false);
    }

    public async Task OnNavigationStartedAsync(Func<NavigationInfoEventArgs, Task> callback, SynchronizationContext syncContext)
    {
        await _session.SubscribeAsync("browsingContext.navigationStarted").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.navigationStarted", new BiDiEventHandler<NavigationInfoEventArgs>(syncContext, callback));
    }

    public async Task OnNavigationStartedAsync(Action<NavigationInfoEventArgs> callback, SynchronizationContext syncContext)
    {
        await _session.SubscribeAsync("browsingContext.navigationStarted").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.navigationStarted", new BiDiEventHandler<NavigationInfoEventArgs>(syncContext, callback));
    }
}
