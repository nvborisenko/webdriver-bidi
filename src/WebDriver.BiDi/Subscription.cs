﻿using OpenQA.Selenium.BiDi.Communication;
using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi;

public class Subscription : IAsyncDisposable
{
    private readonly Broker Broker;
    private readonly Communication.EventHandler _eventHandler;

    internal Subscription(Broker broker, Communication.EventHandler eventHandler)
    {
        Broker = broker;
        _eventHandler = eventHandler;
    }

    public async Task UnsubscribeAsync()
    {
        await Broker.UnsubscribeAsync(_eventHandler).ConfigureAwait(false);
    }

    public async ValueTask DisposeAsync()
    {
        await UnsubscribeAsync().ConfigureAwait(false);
    }
}
