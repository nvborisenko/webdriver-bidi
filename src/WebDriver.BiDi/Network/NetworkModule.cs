using System;
using System.Threading.Tasks;

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

    public async Task<EmptyResult> AddInterceptAsync(AddInterceptParameters parameters)
    {
        return await _broker.ExecuteCommandAsync<AddInterceptCommand, EmptyResult>(new AddInterceptCommand { Params = parameters });
    }

    public async Task<EmptyResult> ContinueRequestAsync(ContinueRequestParameters parameters)
    {
        return await _broker.ExecuteCommandAsync<ContinueRequestCommand, EmptyResult>(new ContinueRequestCommand { Params = parameters });
    }

    public event Action<BeforeRequestSentEventArgs> BeforeRequestSent
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
