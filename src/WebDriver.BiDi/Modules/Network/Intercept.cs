using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class Intercept : IAsyncDisposable
{
    private readonly BiDi.Session _session;

    protected readonly IList<Subscription> _onBeforeRequestSentSubscriptions = [];
    protected readonly IList<Subscription> _onResponseStartedSubscriptions = [];

    internal Intercept(BiDi.Session session, string id)
    {
        _session = session;
        Id = id;
    }

    public string Id { get; }

    public async Task RemoveAsync()
    {
        await _session.NetworkModule.RemoveInterceptAsync(this).ConfigureAwait(false);

        foreach (var subscription in _onBeforeRequestSentSubscriptions)
        {
            await subscription.UnsubscribeAsync().ConfigureAwait(false);
        }
    }

    public virtual async Task OnBeforeRequestSentAsync(Func<BeforeRequestSentEventArgs, Task> callback)
    {
        var subscription = await _session.OnBeforeRequestSentAsync(async args => await Filter(args, callback)).ConfigureAwait(false);

        _onBeforeRequestSentSubscriptions.Add(subscription);
    }

    public virtual async Task OnBeforeRequestSentAsync(BrowsingContext.BrowsingContext context, Func<BeforeRequestSentEventArgs, Task> callback)
    {
        var subscription = await context.OnBeforeRequestSentAsync(async args => await Filter(args, callback)).ConfigureAwait(false);

        _onBeforeRequestSentSubscriptions.Add(subscription);
    }

    public async Task Filter(BeforeRequestSentEventArgs args, Func<BeforeRequestSentEventArgs, Task> callback)
    {
        if (args.Intercepts?.Contains(this) is true && args.IsBlocked)
        {
            await callback(args).ConfigureAwait(false);
        }
    }

    public async Task Filter(ResponseStartedEventArgs args, Func<ResponseStartedEventArgs, Task> callback)
    {
        if (args.Intercepts?.Contains(this) is true && args.IsBlocked)
        {
            await callback(args).ConfigureAwait(false);
        }
    }

    public virtual async Task OnResponseStartedAsync(Func<ResponseStartedEventArgs, Task> callback)
    {
        var subscription = await _session.OnResponseStartedAsync(async args => await Filter(args, callback)).ConfigureAwait(false);

        _onResponseStartedSubscriptions.Add(subscription);
    }

    public virtual async Task OnResponseStartedAsync(BrowsingContext.BrowsingContext context, Func<ResponseStartedEventArgs, Task> callback)
    {
        var subscription = await context.OnResponseStartedAsync(async args => await Filter(args, callback)).ConfigureAwait(false);

        _onResponseStartedSubscriptions.Add(subscription);
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
