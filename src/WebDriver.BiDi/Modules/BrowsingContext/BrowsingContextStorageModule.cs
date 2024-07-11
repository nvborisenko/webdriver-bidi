using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Modules.Storage;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class BrowsingContextStorageModule
{
    private readonly BrowsingContext _context;
    private readonly StorageModule _storageModule;

    public BrowsingContextStorageModule(BrowsingContext context, StorageModule storageModule)
    {
        _context = context;
        _storageModule = storageModule;
    }

    public Task<GetCookiesResult> GetCookiesAsync(GetCookiesOptions? options = default)
    {
        options ??= new();

        options.Partition = new BrowsingContextPartitionDescriptor(_context);

        return _storageModule.GetCookiesAsync(options);
    }

    public async Task<PartitionKey> DeleteCookiesAsync(GetCookiesOptions? options = default)
    {
        options ??= new();

        options.Partition = new BrowsingContextPartitionDescriptor(_context);

        var res = await _storageModule.DeleteCookiesAsync(options).ConfigureAwait(false);

        return res.PartitionKey;
    }

    public async Task<PartitionKey> SetCookieAsync(PartialCookie cookie, SetCookieOptions? options = default)
    {
        options ??= new();

        options.Partition = new BrowsingContextPartitionDescriptor(_context);

        var res = await _storageModule.SetCookieAsync(cookie, options).ConfigureAwait(false);

        return res.PartitionKey;
    }
}
