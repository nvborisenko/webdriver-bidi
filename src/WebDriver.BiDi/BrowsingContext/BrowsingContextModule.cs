using System.Threading;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.BrowsingContext;

public class BrowsingContextModule
{
    private readonly Broker _broker;

    public BrowsingContextModule(string id, Broker broker)
    {
        Id = id;
        _broker = broker;
    }

    public string Id { get; private set; }

    public Task<NavigateResult> NavigateAsync(string url, ReadinessState wait = ReadinessState.Complete, CancellationToken cancellationToken = default)
    {
        return _broker.ExecuteCommandAsync<NavigateCommand, NavigateResult>(new NavigateCommand { Params = new NavigateCommandParameters { BrowsingContextId = Id, Url = url, Wait = wait } }, cancellationToken);
    }

    public async Task<EmptyResult> CloseAsync()
    {
        return await _broker.ExecuteCommandAsync<CloseCommand, EmptyResult>(new CloseCommand { Params = new CloseCommandParameters { Context = Id } });
    }
}

