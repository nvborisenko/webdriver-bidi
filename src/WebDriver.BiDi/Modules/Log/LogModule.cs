using OpenQA.Selenium.BiDi.Internal;
using System.Threading.Tasks;
using System;

namespace OpenQA.Selenium.BiDi.Modules.Log;

internal sealed class LogModule
{
    private readonly BiDi.Session _session;
    private readonly Broker _broker;

    internal LogModule(BiDi.Session session, Broker broker)
    {
        _session = session;
        _broker = broker;
    }

    public async Task OnEntryAddedAsync(Func<LogEntryEventArgs, Task> callback)
    {
        await _session.SubscribeAsync("log.entryAdded").ConfigureAwait(false);

        _broker.RegisterEventHandler("log.entryAdded", new BiDiEventHandler<LogEntryEventArgs>(callback));
    }

    public async Task OnEntryAddedAsync(Action<LogEntryEventArgs> callback)
    {
        await _session.SubscribeAsync("log.entryAdded").ConfigureAwait(false);

        _broker.RegisterEventHandler("log.entryAdded", new BiDiEventHandler<LogEntryEventArgs>(callback));
    }
}
