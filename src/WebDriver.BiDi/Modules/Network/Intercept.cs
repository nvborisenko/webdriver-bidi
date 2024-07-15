﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class Intercept : IAsyncDisposable
{
    private readonly BiDi.Session _session;

    protected readonly IList<Subscription> _onBeforeRequestSentSubscriptions = [];
    protected readonly IList<Subscription> _onResponseStartedSubscriptions = [];
    protected readonly IList<Subscription> _onAuthRequiredSubscriptions = [];

    internal Intercept(BiDi.Session session, string id)
    {
        _session = session;
        Id = id;
    }

    public string Id { get; }

    public async Task RemoveAsync()
    {
        await _session.Network.RemoveInterceptAsync(this).ConfigureAwait(false);

        foreach (var subscription in _onBeforeRequestSentSubscriptions)
        {
            await subscription.UnsubscribeAsync().ConfigureAwait(false);
        }

        foreach (var subscription in _onResponseStartedSubscriptions)
        {
            await subscription.UnsubscribeAsync().ConfigureAwait(false);
        }

        foreach (var subscription in _onAuthRequiredSubscriptions)
        {
            await subscription.UnsubscribeAsync().ConfigureAwait(false);
        }
    }

    public async Task OnBeforeRequestSentAsync(Func<BeforeRequestSentEventArgs, Task> callback, SubscriptionOptions? options = default)
    {
        var subscription = await _session.Network.OnBeforeRequestSentAsync(async args => await Filter(args, callback), options).ConfigureAwait(false);

        _onBeforeRequestSentSubscriptions.Add(subscription);
    }

    public async Task OnResponseStartedAsync(Func<ResponseStartedEventArgs, Task> callback, SubscriptionOptions? options = default)
    {
        var subscription = await _session.Network.OnResponseStartedAsync(async args => await Filter(args, callback), options).ConfigureAwait(false);

        _onResponseStartedSubscriptions.Add(subscription);
    }

    public async Task OnAuthRequiredAsync(Func<AuthRequiredEventArgs, Task> callback, SubscriptionOptions? options = default)
    {
        var subscription = await _session.Network.OnAuthRequiredAsync(async args => await Filter(args, callback), options).ConfigureAwait(false);

        _onAuthRequiredSubscriptions.Add(subscription);
    }

    private async Task Filter(BeforeRequestSentEventArgs args, Func<BeforeRequestSentEventArgs, Task> callback)
    {
        if (args.Intercepts?.Contains(this) is true && args.IsBlocked)
        {
            await callback(args).ConfigureAwait(false);
        }
    }

    private async Task Filter(ResponseStartedEventArgs args, Func<ResponseStartedEventArgs, Task> callback)
    {
        if (args.Intercepts?.Contains(this) is true && args.IsBlocked)
        {
            await callback(args).ConfigureAwait(false);
        }
    }

    private async Task Filter(AuthRequiredEventArgs args, Func<AuthRequiredEventArgs, Task> callback)
    {
        if (args.Intercepts?.Contains(this) is true && args.IsBlocked)
        {
            await callback(args).ConfigureAwait(false);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await RemoveAsync();
    }

    public override bool Equals(object? obj)
    {
        if (obj is Intercept interceptObj) return interceptObj.Id == Id;

        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
