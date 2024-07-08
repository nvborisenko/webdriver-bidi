﻿using System;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

sealed class BrowsingContextModule(Broker broker) : Module(broker)
{
    public async Task<CreateResult> CreateAsync(BrowsingContextType type, BrowsingContextOptions? options = default)
    {
        var @params = new CreateCommandParameters(type);

        if (options is not null)
        {
            @params.ReferenceContext = options.ReferenceContext;
            @params.Background = options.Background;
            @params.UserContext = options.UserContext;
        }

        return await Broker.ExecuteCommandAsync<CreateResult>(new CreateCommand(@params)).ConfigureAwait(false);
    }

    public async Task<NavigateResult> NavigateAsync(BrowsingContext context, string url, NavigateOptions? options = default)
    {
        var @params = new NavigateCommandParameters(context, url);

        if (options is not null)
        {
            @params.Wait = options.Wait;
        }

        return await Broker.ExecuteCommandAsync<NavigateResult>(new NavigateCommand(@params)).ConfigureAwait(false);
    }

    public async Task ActivateAsync(BrowsingContext context)
    {
        var @params = new ActivateCommandParameters(context);

        await Broker.ExecuteCommandAsync(new ActivateCommand(@params)).ConfigureAwait(false);
    }

    public async Task<LocateNodesResult> LocateNodesAsync(BrowsingContext context, Locator locator, NodesOptions? options = default)
    {
        var @params = new LocateNodesCommandParameters(context, locator);

        if (options is not null)
        {
            @params.MaxNodeCount = options.MaxNodeCount;
            @params.SerializationOptions = options.SerializationOptions;
            @params.StartNodes = options.StartNodes;
        }

        return await Broker.ExecuteCommandAsync<LocateNodesResult>(new LocateNodesCommand(@params)).ConfigureAwait(false);
    }

    public async Task<CaptureScreenshotResult> CaptureScreenshotAsync(BrowsingContext context, CaptureScreenshotOptions? options = default)
    {
        var @params = new CaptureScreenshotCommandParameters(context);

        if (options is not null)
        {
            @params.Origin = options.Origin;
            @params.Format = options.Format;
            @params.Clip = options.Clip;
        }

        return await Broker.ExecuteCommandAsync<CaptureScreenshotResult>(new CaptureScreenshotCommand(@params)).ConfigureAwait(false);
    }

    public async Task CloseAsync(BrowsingContext context)
    {
        var @params = new CloseCommandParameters(context);

        await Broker.ExecuteCommandAsync(new CloseCommand(@params)).ConfigureAwait(false);
    }

    public async Task<TraverseHistoryResult> TraverseHistoryAsync(BrowsingContext context, int delta)
    {
        var @params = new TraverseHistoryCommandParameters(context, delta);

        return await Broker.ExecuteCommandAsync<TraverseHistoryResult>(new TraverseHistoryCommand(@params)).ConfigureAwait(false);
    }

    public async Task<NavigateResult> ReloadAsync(BrowsingContext context, ReloadOptions? options = default)
    {
        var @params = new ReloadCommandParameters(context);

        if (options is not null)
        {
            @params.IgnoreCache = options.IgnoreCache;
            @params.Wait = options.Wait;
        }

        return await Broker.ExecuteCommandAsync<NavigateResult>(new ReloadCommand(@params)).ConfigureAwait(false);
    }

    public async Task SetViewportAsync(BrowsingContext context, ViewportOptions? options = default)
    {
        var @params = new SetViewportCommandParameters(context);

        if (options is not null)
        {
            @params.Viewport = options.Viewport;
            @params.DevicePixelRatio = options?.DevicePixelRatio;
        }

        await Broker.ExecuteCommandAsync(new SetViewportCommand(@params)).ConfigureAwait(false);
    }

    public async Task<GetTreeResult> GetTreeAsync(TreeOptions? options = default)
    {
        var @params = new GetTreeCommandParameters();

        if (options is not null)
        {
            @params.MaxDepth = options.MaxDepth;
            @params.Root = options.Root;
        }

        return await Broker.ExecuteCommandAsync<GetTreeResult>(new GetTreeCommand(@params)).ConfigureAwait(false);
    }

    public async Task<PrintResult> PrintAsync(BrowsingContext context, PrintOptions? options = default)
    {
        var @params = new PrintCommandParameters(context);

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

    public async Task HandleUserPrompAsync(BrowsingContext context, UserPromptOptions? options = default)
    {
        var @params = new HandleUserPromptCommandParameters(context);

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
