using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.BrowsingContext;

public sealed class BrowsingContextModule
{
    private readonly Broker _broker;

    internal BrowsingContextModule(string id, Broker broker)
    {
        Id = id;
        _broker = broker;
    }

    public string Id { get; }

    public async Task<NavigateResult> NavigateAsync(string url, ReadinessState wait = ReadinessState.Complete)
    {
        return await _broker.ExecuteCommandAsync<NavigateCommand, NavigateResult>(new NavigateCommand { Params = new NavigateCommandParameters { Context = Id, Url = url, Wait = wait } });
    }

    public async Task<EmptyResult> CloseAsync()
    {
        return await _broker.ExecuteCommandAsync<CloseCommand, EmptyResult>(new CloseCommand { Params = new CloseCommandParameters { Context = Id } });
    }
}

