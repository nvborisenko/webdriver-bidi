using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Communication;

namespace OpenQA.Selenium.BiDi.Modules.Browser;

internal sealed class BrowserModule(Broker broker) : Module(broker)
{
    public async Task CloseAsync(CloseOptions? options = default)
    {
        await Broker.ExecuteCommandAsync(new CloseCommand(), options).ConfigureAwait(false);
    }

    public async Task<UserContextInfo> CreateUserContextAsync(CreateUserContextOptions? options = default)
    {
        return await Broker.ExecuteCommandAsync<UserContextInfo>(new CreateUserContextCommand(), options).ConfigureAwait(false);
    }

    public async Task<GetUserContextsResult> GetUserContextsAsync(GetUserContextsOptions? options = default)
    {
        return await Broker.ExecuteCommandAsync<GetUserContextsResult>(new GetUserContextsCommand(), options).ConfigureAwait(false);
    }

    public async Task RemoveUserContextAsync(UserContext userContext, RemoveUserContextOptions? options = default)
    {
        var @params = new RemoveUserContextCommandParameters(userContext);

        await Broker.ExecuteCommandAsync(new RemoveUserContextCommand(@params), options).ConfigureAwait(false);
    }
}
