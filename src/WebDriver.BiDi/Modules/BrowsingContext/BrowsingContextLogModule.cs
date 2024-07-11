using OpenQA.Selenium.BiDi.Modules.Log;
using System.Threading.Tasks;
using System;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class BrowsingContextLogModule
{
    private readonly BrowsingContext _context;
    private readonly LogModule _logModule;

    public BrowsingContextLogModule(BrowsingContext context, LogModule logModule)
    {
        _context = context;
        _logModule = logModule;
    }

    public Task<Subscription> OnEntryAddedAsync(Func<BaseLogEntry, Task> callback)
    {
        return _logModule.OnEntryAddedAsync(async args =>
        {
            if (args.Source.Context is not null && Equals(args.Source.Context))
            {
                await callback(args).ConfigureAwait(false);
            }
        });
    }

    public Task<Subscription> OnEntryAddedAsync(Action<BaseLogEntry> callback)
    {
        return _logModule.OnEntryAddedAsync(args =>
        {
            if (args.Source.Context?.Equals(_context) is true)
            {
                callback(args);
            }
        });
    }
}
