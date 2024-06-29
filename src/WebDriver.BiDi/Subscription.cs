using OpenQA.Selenium.BiDi.Internal;
using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi;

public class Subscription
    : IAsyncDisposable
{
    private readonly Broker _broker;
    private readonly Internal.EventHandler _eventHandler;

    internal Subscription(Broker broker, Internal.EventHandler eventHandler)
    {
        _broker = broker;
        _eventHandler = eventHandler;
    }

    public async Task UnsubscribeAsync()
    {
        await _broker.UnsubscribeAsync(_eventHandler);
    }

    public async ValueTask DisposeAsync()
    {
        await UnsubscribeAsync();
    }
}
