using System;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

sealed class BrowsingContextModule
{
    private readonly BiDi.Session _session;
    private readonly Broker _broker;

    internal BrowsingContextModule(BiDi.Session session, Broker broker)
    {
        _session = session;
        _broker = broker;
    }

    public async Task<CreateResult> CreateAsync(CreateCommand.Parameters @params)
    {
        return await _broker.ExecuteCommandAsync<CreateResult>(new CreateCommand(@params)).ConfigureAwait(false);
    }

    public async Task<NavigateResult> NavigateAsync(string url, ReadinessState wait = ReadinessState.Complete)
    {
        var @params = new NavigateCommand.Parameters { Url = url, Wait = wait };

        return await NavigateAsync(@params).ConfigureAwait(false);
    }

    public async Task<NavigateResult> NavigateAsync(NavigateCommand.Parameters @params)
    {
        return await _broker.ExecuteCommandAsync<NavigateResult>(new NavigateCommand(@params)).ConfigureAwait(false);
    }

    public async Task ActivateAsync(ActivateCommand.Parameters @params)
    {
        await _broker.ExecuteCommandAsync(new ActivateCommand(@params)).ConfigureAwait(false);
    }

    public async Task<LocateNodesResult> LocateNodesAsync(LocateNodesCommand.Parameters @params)
    {
        return await _broker.ExecuteCommandAsync<LocateNodesResult>(new LocateNodesCommand(@params)).ConfigureAwait(false);
    }

    public async Task<CaptureScreenshotResult> CaptureScreenshotAsync(Origin? origin = default, ImageFormat? imageFormat = default, ClipRectangle? clip = default)
    {
        var @params = new CaptureScreenshotCommand.Parameters
        {
            Origin = origin,
            Format = imageFormat,
            Clip = clip
        };

        return await CaptureScreenshotAsync(@params).ConfigureAwait(false);
    }

    public async Task<CaptureScreenshotResult> CaptureScreenshotAsync(CaptureScreenshotCommand.Parameters @params)
    {
        return await _broker.ExecuteCommandAsync<CaptureScreenshotResult>(new CaptureScreenshotCommand(@params)).ConfigureAwait(false);
    }

    public async Task CloseAsync(CloseCommand.Parameters @params)
    {
        await _broker.ExecuteCommandAsync(new CloseCommand(@params)).ConfigureAwait(false);
    }

    public async Task<TraverseHistoryResult> TraverseHistoryAsync(TraverseHistoryCommand.Parameters @params)
    {
        return await _broker.ExecuteCommandAsync<TraverseHistoryResult>(new TraverseHistoryCommand(@params)).ConfigureAwait(false);
    }

    public async Task<NavigateResult> ReloadAsync(ReloadCommand.Parameters @params)
    {
        return await _broker.ExecuteCommandAsync<NavigateResult>(new ReloadCommand(@params)).ConfigureAwait(false);
    }

    public async Task SetViewportAsync(SetViewportCommand.Parameters @params)
    {
        await _broker.ExecuteCommandAsync(new SetViewportCommand(@params)).ConfigureAwait(false);
    }

    public async Task<GetTreeResult> GetTreeAsync(GetTreeCommand.Parameters @params)
    {
        return await _broker.ExecuteCommandAsync<GetTreeResult>(new GetTreeCommand(@params)).ConfigureAwait(false);
    }

    public async Task<PrintResult> PrintAsync(PrintCommand.Parameters @params)
    {
        return await _broker.ExecuteCommandAsync<PrintResult>(new PrintCommand(@params)).ConfigureAwait(false);
    }

    public async Task HandleUserPrompAsync(HandleUserPromptCommand.Parameters @params)
    {
        await _broker.ExecuteCommandAsync(new HandleUserPromptCommand(@params)).ConfigureAwait(false);
    }

    public async Task OnNavigationStartedAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        await _session.SubscribeAsync("browsingContext.navigationStarted").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.navigationStarted", new BiDiEventHandler<NavigationInfoEventArgs>(callback));
    }

    public async Task OnNavigationStartedAsync(Action<NavigationInfoEventArgs> callback)
    {
        await _session.SubscribeAsync("browsingContext.navigationStarted").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.navigationStarted", new BiDiEventHandler<NavigationInfoEventArgs>(callback));
    }

    public async Task OnFragmentNavigatedAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        await _session.SubscribeAsync("browsingContext.fragmentNavigated").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.fragmentNavigated", new BiDiEventHandler<NavigationInfoEventArgs>(callback));
    }

    public async Task OnFragmentNavigatedAsync(Action<NavigationInfoEventArgs> callback)
    {
        await _session.SubscribeAsync("browsingContext.fragmentNavigated").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.fragmentNavigated", new BiDiEventHandler<NavigationInfoEventArgs>(callback));
    }

    public async Task OnDomContentLoadedAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        await _session.SubscribeAsync("browsingContext.domContentLoaded").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.domContentLoaded", new BiDiEventHandler<NavigationInfoEventArgs>(callback));
    }

    public async Task OnDomContentLoadedAsync(Action<NavigationInfoEventArgs> callback)
    {
        await _session.SubscribeAsync("browsingContext.domContentLoaded").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.domContentLoaded", new BiDiEventHandler<NavigationInfoEventArgs>(callback));
    }

    public async Task OnLoadAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        await _session.SubscribeAsync("browsingContext.load").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.load", new BiDiEventHandler<NavigationInfoEventArgs>(callback));
    }

    public async Task OnLoadAsync(Action<NavigationInfoEventArgs> callback)
    {
        await _session.SubscribeAsync("browsingContext.load").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.load", new BiDiEventHandler<NavigationInfoEventArgs>(callback));
    }

    public async Task OnDownloadWillBeginAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        await _session.SubscribeAsync("browsingContext.downloadWillBegin").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.downloadWillBegin", new BiDiEventHandler<NavigationInfoEventArgs>(callback));
    }

    public async Task OnDownloadWillBeginAsync(Action<NavigationInfoEventArgs> callback)
    {
        await _session.SubscribeAsync("browsingContext.downloadWillBegin").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.downloadWillBegin", new BiDiEventHandler<NavigationInfoEventArgs>(callback));
    }

    public async Task OnNavigationAbortedAsync(Func<NavigationInfoEventArgs, Task> callback)
    {
        await _session.SubscribeAsync("browsingContext.navigationAborted").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.navigationAborted", new BiDiEventHandler<NavigationInfoEventArgs>(callback));
    }

    public async Task OnNavigationAbortedAsync(Action<NavigationInfoEventArgs> callback)
    {
        await _session.SubscribeAsync("browsingContext.navigationAborted").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.navigationAborted", new BiDiEventHandler<NavigationInfoEventArgs>(callback));
    }

    public async Task OnContextCreatedAsync(Func<BrowsingContextInfo, Task> callback)
    {
        await _session.SubscribeAsync("browsingContext.contextCreated").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.contextCreated", new BiDiEventHandler<BrowsingContextInfo>(callback));
    }

    public async Task OnContextCreatedAsync(Action<BrowsingContextInfo> callback)
    {
        await _session.SubscribeAsync("browsingContext.contextCreated").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.contextCreated", new BiDiEventHandler<BrowsingContextInfo>(callback));
    }

    public async Task OnContextDestroyedAsync(Func<BrowsingContextInfo, Task> callback)
    {
        await _session.SubscribeAsync("browsingContext.contextDestroyed").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.contextDestroyed", new BiDiEventHandler<BrowsingContextInfo>(callback));
    }

    public async Task OnContextDestroyedAsync(Action<BrowsingContextInfo> callback)
    {
        await _session.SubscribeAsync("browsingContext.contextDestroyed").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.contextDestroyed", new BiDiEventHandler<BrowsingContextInfo>(callback));
    }

    public async Task OnUserPromptOpenedAsync(Func<UserPromptOpenedEventArgs, Task> callback)
    {
        await _session.SubscribeAsync("browsingContext.userPromptOpened").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.userPromptOpened", new BiDiEventHandler<UserPromptOpenedEventArgs>(callback));
    }

    public async Task OnUserPromptOpenedAsync(Action<UserPromptOpenedEventArgs> callback)
    {
        await _session.SubscribeAsync("browsingContext.userPromptOpened").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.userPromptOpened", new BiDiEventHandler<UserPromptOpenedEventArgs>(callback));
    }

    public async Task OnUserPromptClosedAsync(Func<UserPromptClosedEventArgs, Task> callback)
    {
        await _session.SubscribeAsync("browsingContext.userPromptClosed").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.userPromptClosed", new BiDiEventHandler<UserPromptClosedEventArgs>(callback));
    }

    public async Task OnUserPromptClosedAsync(Action<UserPromptClosedEventArgs> callback)
    {
        await _session.SubscribeAsync("browsingContext.userPromptClosed").ConfigureAwait(false);

        _broker.RegisterEventHandler("browsingContext.userPromptClosed", new BiDiEventHandler<UserPromptClosedEventArgs>(callback));
    }
}
