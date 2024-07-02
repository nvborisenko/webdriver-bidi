using System;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

sealed class Module
{
    private readonly BiDi.Session _session;
    private readonly Broker _broker;

    internal Module(BiDi.Session session, Broker broker)
    {
        _session = session;
        _broker = broker;
    }

    public async Task<CreateResult> CreateAsync(CreateCommandParameters @params)
    {
        return await _broker.ExecuteCommandAsync<CreateResult>(new CreateCommand(@params)).ConfigureAwait(false);
    }

    public async Task<NavigateResult> NavigateAsync(NavigateCommandParameters @params)
    {
        return await _broker.ExecuteCommandAsync<NavigateResult>(new NavigateCommand(@params)).ConfigureAwait(false);
    }

    public async Task ActivateAsync(ActivateCommandParameters @params)
    {
        await _broker.ExecuteCommandAsync(new ActivateCommand(@params)).ConfigureAwait(false);
    }

    public async Task<LocateNodesResult> LocateNodesAsync(LocateNodesCommandParameters @params)
    {
        return await _broker.ExecuteCommandAsync<LocateNodesResult>(new LocateNodesCommand(@params)).ConfigureAwait(false);
    }

    public async Task<CaptureScreenshotResult> CaptureScreenshotAsync(CaptureScreenshotCommandParameters @params)
    {
        return await _broker.ExecuteCommandAsync<CaptureScreenshotResult>(new CaptureScreenshotCommand(@params)).ConfigureAwait(false);
    }

    public async Task CloseAsync(CloseCommandParameters @params)
    {
        await _broker.ExecuteCommandAsync(new CloseCommand(@params)).ConfigureAwait(false);
    }

    public async Task<TraverseHistoryResult> TraverseHistoryAsync(TraverseHistoryCommandParameters @params)
    {
        return await _broker.ExecuteCommandAsync<TraverseHistoryResult>(new TraverseHistoryCommand(@params)).ConfigureAwait(false);
    }

    public async Task<NavigateResult> ReloadAsync(ReloadCommandParameters @params)
    {
        return await _broker.ExecuteCommandAsync<NavigateResult>(new ReloadCommand(@params)).ConfigureAwait(false);
    }

    public async Task SetViewportAsync(SetViewportCommandParameters @params)
    {
        await _broker.ExecuteCommandAsync(new SetViewportCommand(@params)).ConfigureAwait(false);
    }

    public async Task<GetTreeResult> GetTreeAsync(GetTreeCommandParameters @params)
    {
        return await _broker.ExecuteCommandAsync<GetTreeResult>(new GetTreeCommand(@params)).ConfigureAwait(false);
    }

    public async Task<PrintResult> PrintAsync(PrintCommandParameters @params)
    {
        return await _broker.ExecuteCommandAsync<PrintResult>(new PrintCommand(@params)).ConfigureAwait(false);
    }

    public async Task HandleUserPrompAsync(HandleUserPromptCommandParameters @params)
    {
        await _broker.ExecuteCommandAsync(new HandleUserPromptCommand(@params)).ConfigureAwait(false);
    }

    public async Task<Subscription> OnNavigationStartedAsync(Func<NavigationInfo, Task> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.navigationStarted", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnNavigationStartedAsync(Action<NavigationInfo> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.navigationStarted", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnFragmentNavigatedAsync(Func<NavigationInfo, Task> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.fragmentNavigated", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnFragmentNavigatedAsync(Action<NavigationInfo> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.fragmentNavigated", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnDomContentLoadedAsync(Func<NavigationInfo, Task> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.domContentLoaded", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnDomContentLoadedAsync(Action<NavigationInfo> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.domContentLoaded", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnLoadAsync(Func<NavigationInfo, Task> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.load", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnLoadAsync(Action<NavigationInfo> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.load", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnDownloadWillBeginAsync(Func<NavigationInfo, Task> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.downloadWillBegin", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnDownloadWillBeginAsync(Action<NavigationInfo> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.downloadWillBegin", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnNavigationAbortedAsync(Func<NavigationInfo, Task> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.navigationAborted", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnNavigationAbortedAsync(Action<NavigationInfo> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.navigationAborted", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnNavigationFailedAsync(Func<NavigationInfo, Task> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.navigationFailed", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnNavigationFailedAsync(Action<NavigationInfo> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.navigationFailed", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnContextCreatedAsync(Func<BrowsingContextInfo, Task> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.contextCreated", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnContextCreatedAsync(Action<BrowsingContextInfo> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.contextCreated", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnContextDestroyedAsync(Func<BrowsingContextInfo, Task> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.contextDestroyed", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnContextDestroyedAsync(Action<BrowsingContextInfo> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.contextDestroyed", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnUserPromptOpenedAsync(Func<UserPromptOpenedEventArgs, Task> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.userPromptOpened", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnUserPromptOpenedAsync(Action<UserPromptOpenedEventArgs> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.userPromptOpened", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnUserPromptClosedAsync(Func<UserPromptClosedEventArgs, Task> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.userPromptClosed", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnUserPromptClosedAsync(Action<UserPromptClosedEventArgs> callback, BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("browsingContext.userPromptClosed", callback, context).ConfigureAwait(false);
    }
}
