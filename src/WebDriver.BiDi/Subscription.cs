using OpenQA.Selenium.BiDi.Communication;
using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi;

public class Subscription : IAsyncDisposable
{
    private readonly Broker _broker;
    private readonly Communication.EventHandler _eventHandler;

    internal Subscription(Broker broker, Communication.EventHandler eventHandler)
    {
        _broker = broker;
        _eventHandler = eventHandler;
    }

    public async Task UnsubscribeAsync()
    {
        await _broker.UnsubscribeAsync(_eventHandler).ConfigureAwait(false);
    }

    public async ValueTask DisposeAsync()
    {
        await UnsubscribeAsync().ConfigureAwait(false);
    }
}
