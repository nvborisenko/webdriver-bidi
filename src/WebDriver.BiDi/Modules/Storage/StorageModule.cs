using OpenQA.Selenium.BiDi.Communication;
using System.IO;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Storage;

internal class StorageModule(Broker broker) : Module(broker)
{
    public async Task<GetCookiesResult> GetCookiesAsync(GetCookiesCommandParameters @params, CookiesOptions? options = default)
    {
        if (options is not null)
        {
            @params.Filter = options.Filter;
            @params.Partition = options.Partition;
        }

        return await Broker.ExecuteCommandAsync<GetCookiesResult>(new GetCookiesCommand(@params)).ConfigureAwait(false);
    }

    public async Task<DeleteCookiesResult> DeleteCookiesAsync(DeleteCookiesCommandParameters @params, CookiesOptions? options = default)
    {
        if (options is not null)
        {
            @params.Filter = options.Filter;
            @params.Partition = options.Partition;
        }

        return await Broker.ExecuteCommandAsync<DeleteCookiesResult>(new DeleteCookiesCommand(@params)).ConfigureAwait(false);
    }

    public async Task<SetCookieResult> SetCookieAsync(SetCookieCommandParameters @params, PartialCookieOptions? options = default)
    {
        if (options is not null)
        {
            @params.Cookie.Path = options.Path;
            @params.Cookie.HttpOnly = options.HttpOnly;
            @params.Cookie.Secure = options.Secure;
            @params.Cookie.SameSite = options.SameSite;
            @params.Cookie.Expiry = options.Expiry;
        }

        return await Broker.ExecuteCommandAsync<SetCookieResult>(new SetCookieCommand(@params)).ConfigureAwait(false);
    }
}
