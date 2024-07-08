using System;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

sealed class BrowsingContextModule(Broker broker) : Module(broker)
{
    public async Task<CreateResult> CreateAsync(CreateCommandParameters @params, BrowsingContextOptions? options = default)
    {
        if (options is not null)
        {
            @params.ReferenceContext = options.ReferenceContext;
            @params.Background = options.Background;
            @params.UserContext = options.UserContext;
        }

        return await Broker.ExecuteCommandAsync<CreateResult>(new CreateCommand(@params)).ConfigureAwait(false);
    }

    public async Task<NavigateResult> NavigateAsync(NavigateCommandParameters @params, NavigateOptions? options = default)
    {
        if (options is not null)
        {
            @params.Wait = options.Wait;
        }

        return await Broker.ExecuteCommandAsync<NavigateResult>(new NavigateCommand(@params)).ConfigureAwait(false);
    }

    public async Task ActivateAsync(ActivateCommandParameters @params)
    {
        await Broker.ExecuteCommandAsync(new ActivateCommand(@params)).ConfigureAwait(false);
    }

    public async Task<LocateNodesResult> LocateNodesAsync(LocateNodesCommandParameters @params, NodesOptions? options = default)
    {
        if (options is not null)
        {
            @params.MaxNodeCount = options.MaxNodeCount;
            @params.SerializationOptions = options.SerializationOptions;
            @params.StartNodes = options.StartNodes;
        }

        return await Broker.ExecuteCommandAsync<LocateNodesResult>(new LocateNodesCommand(@params)).ConfigureAwait(false);
    }

    public async Task<CaptureScreenshotResult> CaptureScreenshotAsync(CaptureScreenshotCommandParameters @params, CaptureScreenshotOptions? options = default)
    {
        if (options is not null)
        {
            @params.Origin = options.Origin;
            @params.Format = options.Format;
            @params.Clip = options.Clip;
        }

        return await Broker.ExecuteCommandAsync<CaptureScreenshotResult>(new CaptureScreenshotCommand(@params)).ConfigureAwait(false);
    }

    public async Task CloseAsync(CloseCommandParameters @params)
    {
        await Broker.ExecuteCommandAsync(new CloseCommand(@params)).ConfigureAwait(false);
    }

    public async Task<TraverseHistoryResult> TraverseHistoryAsync(TraverseHistoryCommandParameters @params)
    {
        return await Broker.ExecuteCommandAsync<TraverseHistoryResult>(new TraverseHistoryCommand(@params)).ConfigureAwait(false);
    }

    public async Task<NavigateResult> ReloadAsync(ReloadCommandParameters @params, ReloadOptions? options = default)
    {
        if (options is not null)
        {
            @params.IgnoreCache = options.IgnoreCache;
            @params.Wait = options.Wait;
        }

        return await Broker.ExecuteCommandAsync<NavigateResult>(new ReloadCommand(@params)).ConfigureAwait(false);
    }

    public async Task SetViewportAsync(SetViewportCommandParameters @params, ViewportOptions? options = default)
    {
        if (options is not null)
        {
            @params.Viewport = options.Viewport;
            @params.DevicePixelRatio = options?.DevicePixelRatio;
        }

        await Broker.ExecuteCommandAsync(new SetViewportCommand(@params)).ConfigureAwait(false);
    }

    public async Task<GetTreeResult> GetTreeAsync(GetTreeCommandParameters @params, TreeOptions? options = default)
    {
        if (options is not null)
        {
            @params.MaxDepth = options.MaxDepth;
            @params.Root = options.Root;
        }

        return await Broker.ExecuteCommandAsync<GetTreeResult>(new GetTreeCommand(@params)).ConfigureAwait(false);
    }

    public async Task<PrintResult> PrintAsync(PrintCommandParameters @params, PrintOptions? options = default)
    {
        if (options is not null)
        {
            @params.Background = options.Background;
            @params.Margin = options.Margin;
            @params.Orientation = options.Orientation;
            @params.Page = options.Page;
            @params.PageRanges = options.PageRanges;
            @params.Scale = options.Scale;
            @params.ShrinkToFit = options.ShrinkToFit;
        }

        return await Broker.ExecuteCommandAsync<PrintResult>(new PrintCommand(@params)).ConfigureAwait(false);
    }

    public async Task HandleUserPrompAsync(HandleUserPromptCommandParameters @params, UserPromptOptions? options = default)
    {
        if (options is not null)
        {
            @params.Accept = options.Accept;
            @params.UserText = options.UserText;
        }

        await Broker.ExecuteCommandAsync(new HandleUserPromptCommand(@params)).ConfigureAwait(false);
    }

    public async Task<Subscription> OnNavigationStartedAsync(Func<NavigationInfo, Task> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.navigationStarted", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnNavigationStartedAsync(Action<NavigationInfo> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.navigationStarted", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnFragmentNavigatedAsync(Func<NavigationInfo, Task> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.fragmentNavigated", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnFragmentNavigatedAsync(Action<NavigationInfo> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.fragmentNavigated", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnDomContentLoadedAsync(Func<NavigationInfo, Task> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.domContentLoaded", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnDomContentLoadedAsync(Action<NavigationInfo> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.domContentLoaded", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnLoadAsync(Func<NavigationInfo, Task> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.load", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnLoadAsync(Action<NavigationInfo> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.load", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnDownloadWillBeginAsync(Func<NavigationInfo, Task> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.downloadWillBegin", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnDownloadWillBeginAsync(Action<NavigationInfo> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.downloadWillBegin", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnNavigationAbortedAsync(Func<NavigationInfo, Task> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.navigationAborted", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnNavigationAbortedAsync(Action<NavigationInfo> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.navigationAborted", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnNavigationFailedAsync(Func<NavigationInfo, Task> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.navigationFailed", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnNavigationFailedAsync(Action<NavigationInfo> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.navigationFailed", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnContextCreatedAsync(Func<BrowsingContextInfo, Task> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.contextCreated", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnContextCreatedAsync(Action<BrowsingContextInfo> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.contextCreated", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnContextDestroyedAsync(Func<BrowsingContextInfo, Task> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.contextDestroyed", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnContextDestroyedAsync(Action<BrowsingContextInfo> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.contextDestroyed", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnUserPromptOpenedAsync(Func<UserPromptOpenedEventArgs, Task> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.userPromptOpened", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnUserPromptOpenedAsync(Action<UserPromptOpenedEventArgs> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.userPromptOpened", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnUserPromptClosedAsync(Func<UserPromptClosedEventArgs, Task> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.userPromptClosed", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnUserPromptClosedAsync(Action<UserPromptClosedEventArgs> callback, BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("browsingContext.userPromptClosed", callback, context).ConfigureAwait(false);
    }
}
