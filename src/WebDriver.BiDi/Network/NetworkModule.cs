using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Network;

public sealed class NetworkModule
{
    private readonly BiDiSession _session;
    private readonly Broker _broker;

    internal NetworkModule(BiDiSession session, Broker broker)
    {
        _session = session;
        _broker = broker;
    }

    public async Task AddInterceptAsync(AddInterceptParameters parameters)
    {
        await _broker.ExecuteCommandAsync(new AddInterceptCommand { Params = parameters }).ConfigureAwait(false);
    }

    public async Task ContinueRequestAsync(ContinueRequestParameters parameters)
    {
        await _broker.ExecuteCommandAsync(new ContinueRequestCommand { Params = parameters }).ConfigureAwait(false);
    }

    public event AsyncEventHandler<BeforeRequestSentEventArgs> BeforeRequestSent
    {
        add
        {
            AsyncHelper.RunSync(() => _session.SubscribeAsync("network.beforeRequestSent"));

            _broker.RegisterEventHandler<BeforeRequestSentEventArgs>("network.beforeRequestSent", value);
        }
        remove
        {

        }
    }
}
