using OpenQA.Selenium.BiDi.Internal;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Storage;

internal class Module(Broker broker)
{
    private readonly Broker _broker = broker;

    public async Task<GetCookiesResult> GetCookiesAsync(GetCookiesCommand.Parameters @params)
    {
        return await _broker.ExecuteCommandAsync<GetCookiesResult>(new GetCookiesCommand(@params)).ConfigureAwait(false);
    }

    public async Task<DeleteCookiesResult> DeleteCookiesAsync(DeleteCookiesCommand.Parameters @params)
    {
        return await _broker.ExecuteCommandAsync<DeleteCookiesResult>(new DeleteCookiesCommand(@params)).ConfigureAwait(false);
    }

    public async Task<SetCookieResult> SetCookieAsync(SetCookieCommand.Parameters @params)
    {
        return await _broker.ExecuteCommandAsync<SetCookieResult>(new SetCookieCommand(@params)).ConfigureAwait(false);
    }
}
