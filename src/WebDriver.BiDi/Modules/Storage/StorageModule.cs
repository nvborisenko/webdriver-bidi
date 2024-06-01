using OpenQA.Selenium.BiDi.Internal;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Storage;

internal class StorageModule(Broker broker)
{
    private readonly Broker _broker = broker;

    public async Task<GetCookiesResult> GetCookiesAsync(GetCookiesCommand.Parameters @params)
    {
        return await _broker.ExecuteCommandAsync<GetCookiesResult>(new GetCookiesCommand(@params)).ConfigureAwait(false);
    }
}
