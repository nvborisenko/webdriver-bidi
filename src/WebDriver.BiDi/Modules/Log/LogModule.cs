using System.Threading.Tasks;
using System;
using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.Log;

public sealed class LogModule(Broker broker) : Module(broker)
{
    public async Task<Subscription> OnEntryAddedAsync(Func<BaseLogEntry, Task> callback, SubscriptionOptions? options = default)
    {
        return await Broker.SubscribeAsync("log.entryAdded", callback, options).ConfigureAwait(false);
    }

    public async Task<Subscription> OnEntryAddedAsync(Action<BaseLogEntry> callback, SubscriptionOptions? options = default)
    {
        return await Broker.SubscribeAsync("log.entryAdded", callback, options).ConfigureAwait(false);
    }
}
