using System;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public sealed class BrowsingContextModule
{
    private readonly BiDi.Session _session;
    private readonly Broker _broker;

    internal BrowsingContextModule(string id, BiDi.Session session, Broker broker)
    {
        Id = id;

        _session = session;
        _broker = broker;
    }

    public string Id { get; }

    public async Task<NavigateResult> NavigateAsync(string url, ReadinessState wait = ReadinessState.Complete)
    {
        var parameters = new NavigateCommandParameters { Url = url, Wait = wait };

        return await NavigateAsync(parameters).ConfigureAwait(false);
    }

    public async Task<NavigateResult> NavigateAsync(NavigateCommandParameters parameters)
    {
        parameters.Context = Id;

        return await _broker.ExecuteCommandAsync<NavigateCommand, NavigateResult>(new NavigateCommand { Params = parameters }).ConfigureAwait(false);
    }

    public async Task ActivateAsync()
    {
        await _broker.ExecuteCommandAsync(new ActivateCommand { Params = new ActivateParameters { Context = Id } }).ConfigureAwait(false);
    }

    public async Task<CaptureScreenshotResult> CaptureScreenshotAsync(Origin? origin = default, ImageFormat? imageFormat = default)
    {
        var parameters = new CaptureScreenshotCommandParameters
        {
            Origin = origin,
            Format = imageFormat
        };

        return await CaptureScreenshotAsync(parameters).ConfigureAwait(false);
    }

    public async Task<CaptureScreenshotResult> CaptureScreenshotAsync(CaptureScreenshotCommandParameters parameters)
    {
        parameters.Context = Id;

        return await _broker.ExecuteCommandAsync<CaptureScreenshotCommand, CaptureScreenshotResult>(new CaptureScreenshotCommand { Params = parameters }).ConfigureAwait(false);
    }

    public async Task CloseAsync()
    {
        await _broker.ExecuteCommandAsync(new CloseCommand { Params = new CloseCommandParameters { Context = Id } }).ConfigureAwait(false);
    }

    public async Task OnNavigationStartedAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        var syncContext = SynchronizationContext.Current;

        await _session.SubscribeAsync("browsingContext.navigationStarted").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.navigationStarted", new BiDiEventHandler<NavigationInfoEventArgs>(syncContext, callback));
    }

    public async Task OnNavigationStartedAsync(Action<NavigationInfoEventArgs> callback)
    {
        var syncContext = SynchronizationContext.Current;

        await _session.SubscribeAsync("browsingContext.navigationStarted").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.navigationStarted", new BiDiEventHandler<NavigationInfoEventArgs>(syncContext, callback));
    }
}
