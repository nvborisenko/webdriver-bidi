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

    public async Task<NavigateResult> NavigateAsync(string url, NavigateWait wait = NavigateWait.Complete)
    {
        return await _broker.ExecuteCommandAsync<NavigateCommand, NavigateResult>(new NavigateCommand { Params = new NavigateCommandParameters { BrowsingContextId = Id, Url = url, Wait = wait } });
    }

    public async Task<EmptyResult> CloseAsync()
    {
        return await _broker.ExecuteCommandAsync<CloseCommand, EmptyResult>(new CloseCommand { Params = new CloseCommandParameters { Context = Id } });
    }
}

