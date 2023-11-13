using System;

namespace OpenQA.Selenium.BiDi.Network;

public class NetworkModule
{
    private BiDiSession _session;
    private Broker _broker;

    public NetworkModule(BiDiSession session, Broker broker)
    {
        _session = session;
        _broker = broker;
    }

    public event Action<BeforeRequestSentEventArgs> OnBeforeRequestSent
    {
        add
        {
            _session.SubscribeAsync("network.beforeRequestSent").GetAwaiter().GetResult();

            _broker.RegisterEventHandler<BeforeRequestSentEventArgs>("network.beforeRequestSent", value);
        }
        remove
        {
            
        }
    }
}
