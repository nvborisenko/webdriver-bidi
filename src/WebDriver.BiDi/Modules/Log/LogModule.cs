using System.Threading.Tasks;
using System;
using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.Log;

internal sealed class LogModule(Broker broker) : Module(broker)
{
    public async Task<Subscription> OnEntryAddedAsync(Func<BaseLogEntry, Task> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("log.entryAdded", callback, context).ConfigureAwait(false);
    }

    public async Task<Subscription> OnEntryAddedAsync(Action<BaseLogEntry> callback, BrowsingContext.BrowsingContext? context = default)
    {
        return await Broker.SubscribeAsync("log.entryAdded", callback, context).ConfigureAwait(false);
    }
}
