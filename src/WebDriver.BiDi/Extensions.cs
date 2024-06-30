﻿using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using OpenQA.Selenium.BiDi.Modules.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi;

public static class Extensions
{
    public static async Task<Intercept> AddInterceptOnBeforeRequestSentAsync(this Session session, Func<BeforeRequestSentEventArgs, Task> callback, IEnumerable<UrlPattern>? urlPatterns = default)
    {
        var intercept = await session.AddInterceptAsync([InterceptPhase.BeforeRequestSent], urlPatterns).ConfigureAwait(false);

        await intercept.OnBeforeRequestSentAsync(callback).ConfigureAwait(false);

        return intercept;
    }

    public static async Task<Intercept> AddInterceptOnResponseStartedAsync(this Session session, Func<ResponseStartedEventArgs, Task> callback, IEnumerable<UrlPattern>? urlPatterns = default)
    {
        var intercept = await session.AddInterceptAsync([InterceptPhase.ResponseStarted], urlPatterns).ConfigureAwait(false);

        await intercept.OnResponseStartedAsync(callback).ConfigureAwait(false);

        return intercept;
    }

    public static async Task<InterceptBrowsingContext> AddInterceptOnBeforeRequestSentAsync(this BrowsingContext context, Func<BeforeRequestSentEventArgs, Task> callback, IEnumerable<UrlPattern>? urlPatterns = default)
    {
        var intercept = await context.AddInterceptAsync([InterceptPhase.BeforeRequestSent], urlPatterns).ConfigureAwait(false);

        await intercept.OnBeforeRequestSentAsync(callback).ConfigureAwait(false);

        return new(context.Session, intercept.Id, context);
    }

    public static async Task<InterceptBrowsingContext> AddInterceptOnResponseStartedAsync(this BrowsingContext context, Func<ResponseStartedEventArgs, Task> callback, IEnumerable<UrlPattern>? urlPatterns = default)
    {
        var intercept = await context.AddInterceptAsync([InterceptPhase.ResponseStarted], urlPatterns).ConfigureAwait(false);

        await intercept.OnResponseStartedAsync(callback).ConfigureAwait(false);

        return new(context.Session, intercept.Id, context);
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