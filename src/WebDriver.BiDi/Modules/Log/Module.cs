using OpenQA.Selenium.BiDi.Internal;
using System.Threading.Tasks;
using System;

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

    public async Task OnEntryAddedAsync(Func<BaseLogEntryEventArgs, Task> callback)
    {
        await _session.SubscribeAsync("log.entryAdded").ConfigureAwait(false);

        _broker.RegisterEventHandler("log.entryAdded", new BiDiEventHandler<BaseLogEntryEventArgs>(callback));
    }

    public async Task OnEntryAddedAsync(Action<BaseLogEntryEventArgs> callback)
    {
        await _session.SubscribeAsync("log.entryAdded").ConfigureAwait(false);

        _broker.RegisterEventHandler("log.entryAdded", new BiDiEventHandler<BaseLogEntryEventArgs>(callback));
    }
}
