using OpenQA.Selenium.BiDi.Communication;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Storage;

internal class StorageModule(Broker broker) : Module(broker)
{
    public async Task<GetCookiesResult> GetCookiesAsync(GetCookiesCommandParameters @params)
    {
        return await Broker.ExecuteCommandAsync<GetCookiesResult>(new GetCookiesCommand(@params)).ConfigureAwait(false);
    }

    public async Task<DeleteCookiesResult> DeleteCookiesAsync(DeleteCookiesCommandParameters @params)
    {
        return await Broker.ExecuteCommandAsync<DeleteCookiesResult>(new DeleteCookiesCommand(@params)).ConfigureAwait(false);
    }

    public async Task<SetCookieResult> SetCookieAsync(SetCookieCommandParameters @params)
    {
        return await Broker.ExecuteCommandAsync<SetCookieResult>(new SetCookieCommand(@params)).ConfigureAwait(false);
    }
}
