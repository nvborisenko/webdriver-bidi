using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.Browser;

internal sealed class BrowserModule(Broker broker) : Module(broker)
{
    public async Task CloseAsync()
    {
        await Broker.ExecuteCommandAsync(new CloseCommand()).ConfigureAwait(false);
    }

    public async Task<UserContextInfo> CreateUserContextAsync()
    {
        return await Broker.ExecuteCommandAsync<UserContextInfo>(new CreateUserContextCommand()).ConfigureAwait(false);
    }

    public async Task<GetUserContextsResult> GetUserContextsAsync()
    {
        return await Broker.ExecuteCommandAsync<GetUserContextsResult>(new GetUserContextsCommand()).ConfigureAwait(false);
    }

    public async Task RemoveUserContextAsync(UserContext userContext)
    {
        var @params = new RemoveUserContextCommandParameters(userContext);

        await Broker.ExecuteCommandAsync(new RemoveUserContextCommand(@params)).ConfigureAwait(false);
    }
}
