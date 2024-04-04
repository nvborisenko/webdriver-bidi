﻿using System.Threading.Tasks;

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

    public async Task<EmptyResult> AddInterceptAsync(AddInterceptParameters parameters)
    {
        return await _broker.ExecuteCommandAsync<AddInterceptCommand, EmptyResult>(new AddInterceptCommand { Params = parameters }).ConfigureAwait(false);
    }

    public async Task<EmptyResult> ContinueRequestAsync(ContinueRequestParameters parameters)
    {
        return await _broker.ExecuteCommandAsync<ContinueRequestCommand, EmptyResult>(new ContinueRequestCommand { Params = parameters }).ConfigureAwait(false);
    }

    public event AsyncEventHandler<BeforeRequestSentEventArgs> BeforeRequestSent
    {
        add
        {
            _session.SubscribeAsync("network.beforeRequestSent").ConfigureAwait(false).GetAwaiter().GetResult();

            _broker.RegisterEventHandler<BeforeRequestSentEventArgs>("network.beforeRequestSent", value);
        }
        remove
        {

        }
    }
}
