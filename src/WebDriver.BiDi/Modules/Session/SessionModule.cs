using OpenQA.Selenium.BiDi.Internal;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Session;

public sealed class SessionModule
{
    private readonly BiDi.Session _session;
    private readonly Broker _broker;

    internal SessionModule(BiDi.Session session, Broker broker)
    {
        _session = session;
        _broker = broker;
    }

    public async Task<StatusResult> StatusAsync()
    {
        return await _broker.ExecuteCommandAsync<StatusCommand, StatusResult>(new StatusCommand()).ConfigureAwait(false);
    }

    public Task OnBeforeRequestSent(Func<Network.BeforeRequestSentEventArgs, Task> callback)
    {
        var syncContext = SynchronizationContext.Current;

        return _session.Network.OnBeforeRequestSentAsync(callback, syncContext);
    }

    public Task OnBeforeRequestSent(Action<Network.BeforeRequestSentEventArgs> callback)
    {
        var syncContext = SynchronizationContext.Current;

        return _session.Network.OnBeforeRequestSentAsync(callback, syncContext);
    }
}
