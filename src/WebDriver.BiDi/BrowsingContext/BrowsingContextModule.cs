using System;
using System.Threading.Tasks;

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
        return await _broker.ExecuteCommandAsync<NavigateCommand, NavigateResult>(new NavigateCommand { Params = new NavigateCommandParameters { Context = Id, Url = url, Wait = wait } }).ConfigureAwait(false);
    }

    public async Task<EmptyResult> CloseAsync()
    {
        return await _broker.ExecuteCommandAsync<CloseCommand, EmptyResult>(new CloseCommand { Params = new CloseCommandParameters { Context = Id } }).ConfigureAwait(false);
    }

    public event AsyncEventHandler<NavigationStartedEventArgs> NavigationStarted
    {
        add
        {
            _session.SubscribeAsync("browsingContext.navigationStarted").GetAwaiter().GetResult();

            _broker.RegisterEventHandler("browsingContext.navigationStarted", value);
        }
        remove
        {

        }
    }

    public void Dispose()
    {
        CloseAsync().ConfigureAwait(false).GetAwaiter().GetResult();
    }
}

