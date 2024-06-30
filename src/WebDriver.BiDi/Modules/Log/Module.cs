using System.Threading.Tasks;
using System;
using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.Log;

internal sealed class Module
{
    private readonly BiDi.Session _session;
    private readonly Broker _broker;

    internal Module(BiDi.Session session, Broker broker)
    {
        _session = session;
        _broker = broker;
    }

    public async Task<Subscription> OnEntryAddedAsync(Func<BaseLogEntryEventArgs, Task> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("log.entryAdded", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnEntryAddedAsync(Action<BaseLogEntryEventArgs> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await _broker.SubscribeAsync("log.entryAdded", callback, context).ConfigureAwait(false);
    }
}
