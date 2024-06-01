using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi.Modules.Browser;

internal sealed class Module
{
    private readonly Broker _broker;

    internal Module(Broker broker)
    {
        _broker = broker;
    }

    public async Task CloseAsync()
    {
        await _broker.ExecuteCommandAsync(new CloseCommand()).ConfigureAwait(false);
    }

    public async Task<UserContextInfo> CreateUserContextAsync()
    {
        return await _broker.ExecuteCommandAsync<UserContextInfo>(new CreateUserContextCommand()).ConfigureAwait(false);
    }

    public async Task<GetUserContextsResult> GetUserContextsAsync()
    {
        return await _broker.ExecuteCommandAsync<GetUserContextsResult>(new GetUserContextsCommand()).ConfigureAwait(false);
    }

    public async Task RemoveUserContextAsync(RemoveUserContextCommand.Parameters @params)
    {
        await _broker.ExecuteCommandAsync(new RemoveUserContextCommand(@params)).ConfigureAwait(false);
    }
}
