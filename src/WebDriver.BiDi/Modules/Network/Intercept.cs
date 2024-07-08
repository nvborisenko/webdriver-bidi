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

    public string Id { get; private set; }

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
        var subscription = await _session.NetworkModule.OnBeforeRequestSentAsync(async args =>
        {
            if (args.Intercepts?.Contains(this) is true && args.IsBlocked)
            {
                await callback(args).ConfigureAwait(false);
            }
        }).ConfigureAwait(false);

        _onBeforeRequestSentSubscriptions.Add(subscription);
    }

    public virtual async Task OnResponseStartedAsync(Func<ResponseStartedEventArgs, Task> callback)
    {
        var subscription = await _session.NetworkModule.OnResponseStartedAsync(async args =>
        {
            if (args.Intercepts?.Contains(this) is true && args.IsBlocked)
            {
                await callback(args).ConfigureAwait(false);
            }
        }).ConfigureAwait(false);

        _onResponseStartedSubscriptions.Add(subscription);
    }

    public async ValueTask DisposeAsync()
    {
        await RemoveAsync();
    }

    public override bool Equals(object obj)
    {
        if (obj is Intercept interceptObj) return interceptObj.Id == Id;

        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
