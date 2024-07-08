using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using OpenQA.Selenium.BiDi.Modules.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi;

public static class Extensions
{
    public static async Task<BrowsingContext> CreateBrowsingContextAsync(this Session session, BrowsingContextOptions? options = default)
    {
        return await session.CreateBrowsingContextAsync(BrowsingContextType.Tab, options).ConfigureAwait(false);
    }

    public static async Task<Intercept> AddInterceptOnBeforeRequestSentAsync(this Session session, Func<BeforeRequestSentEventArgs, Task> callback, IEnumerable<UrlPattern>? urlPatterns = default)
    {
        var result = await session.NetworkModule.AddInterceptAsync([InterceptPhase.BeforeRequestSent], new() { UrlPatterns = urlPatterns }).ConfigureAwait(false);

        var intercept = result.Intercept;

        await intercept.OnBeforeRequestSentAsync(callback).ConfigureAwait(false);

        return intercept;
    }

    public static async Task<Intercept> AddInterceptOnResponseStartedAsync(this Session session, Func<ResponseStartedEventArgs, Task> callback, IEnumerable<UrlPattern>? urlPatterns = default)
    {
        var result = await session.NetworkModule.AddInterceptAsync([InterceptPhase.ResponseStarted], new() { UrlPatterns = urlPatterns }).ConfigureAwait(false);

        var intercept = result.Intercept;

        await intercept.OnResponseStartedAsync(callback).ConfigureAwait(false);

        return intercept;
    }

    public static async Task<InterceptBrowsingContext> AddInterceptOnBeforeRequestSentAsync(this BrowsingContext context, Func<BeforeRequestSentEventArgs, Task> callback, IEnumerable<UrlPattern>? urlPatterns = default)
    {
        var result = await context.Session.NetworkModule.AddInterceptAsync([InterceptPhase.BeforeRequestSent], new() { Contexts = [context], UrlPatterns = urlPatterns }).ConfigureAwait(false);

        var intercept = result.Intercept;

        await intercept.OnBeforeRequestSentAsync(callback).ConfigureAwait(false);

        return new(context.Session, intercept.Id, context);
    }

    public static async Task<InterceptBrowsingContext> AddInterceptOnResponseStartedAsync(this BrowsingContext context, Func<ResponseStartedEventArgs, Task> callback, IEnumerable<UrlPattern>? urlPatterns = default)
    {
        var result = await context.Session.NetworkModule.AddInterceptAsync([InterceptPhase.ResponseStarted], new() { Contexts = [context], UrlPatterns = urlPatterns }).ConfigureAwait(false);

        var intercept = result.Intercept;

        await intercept.OnResponseStartedAsync(callback).ConfigureAwait(false);

        return new(context.Session, intercept.Id, context);
    }

    public static Task ContinueAsync(this RequestData requestData, RequestOptions? options = default)
    {
        return requestData.Request.ContinueAsync(options);
    }

    public static Task FailAsync(this RequestData requestData)
    {
        return requestData.Request.FailAsync();
    }

    public static Task ProvideResponseAsync(this RequestData requestData, ProvideResponseOptions? options = default)
    {
        return requestData.Request.ProvideResponseAsync(options);
    }

    public class InterceptBrowsingContext : Intercept
    {
        private readonly BrowsingContext _context;

        internal InterceptBrowsingContext(Session session, string id, BrowsingContext context) : base(session, id)
        {
            _context = context;
        }

        public override async Task OnBeforeRequestSentAsync(Func<BeforeRequestSentEventArgs, Task> callback)
        {
            var subscription = await _context.OnBeforeRequestSentAsync(async args =>
            {
                if (args.Intercepts?.Contains(this) is true && args.IsBlocked)
                {
                    await callback(args).ConfigureAwait(false);
                }
            }).ConfigureAwait(false);

            _onBeforeRequestSentSubscriptions.Add(subscription);
        }

        public override async Task OnResponseStartedAsync(Func<ResponseStartedEventArgs, Task> callback)
        {
            var subscription = await _context.OnResponseStartedAsync(async args =>
            {
                if (args.Intercepts?.Contains(this) is true && args.IsBlocked)
                {
                    await callback(args).ConfigureAwait(false);
                }
            }).ConfigureAwait(false);

            _onResponseStartedSubscriptions.Add(subscription);
        }
    }
}
