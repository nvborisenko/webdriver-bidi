using System;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.BrowsingContext;

public sealed class BrowsingContextModule : IDisposable
{
    private readonly BiDiSession _session;
    private readonly Broker _broker;

    internal BrowsingContextModule(string id, BiDiSession session, Broker broker)
    {
        Id = id;

        _session = session;
        _broker = broker;
    }

    public string Id { get; }

    public async Task<NavigateResult> NavigateAsync(string url, ReadinessState wait = ReadinessState.Complete)
    {
        var parameters = new NavigateCommandParameters { Url = url, Wait = wait };

        return await NavigateAsync(parameters).ConfigureAwait(false);
    }

    public async Task<NavigateResult> NavigateAsync(NavigateCommandParameters parameters)
    {
        parameters.Context = Id;

        return await _broker.ExecuteCommandAsync<NavigateCommand, NavigateResult>(new NavigateCommand { Params = parameters }).ConfigureAwait(false);
    }

    public async Task CloseAsync()
    {
        await _broker.ExecuteCommandAsync(new CloseCommand { Params = new CloseCommandParameters { Context = Id } }).ConfigureAwait(false);
    }

    public event AsyncEventHandler<NavigationInfoEventArgs> NavigationStarted
    {
        add
        {
            AsyncHelper.RunSync(() => _session.SubscribeAsync("browsingContext.navigationStarted"));

            _broker.RegisterEventHandler("browsingContext.navigationStarted", value);
        }
        remove
        {

        }
    }

    public void Dispose()
    {
        AsyncHelper.RunSync(CloseAsync);
    }
}

