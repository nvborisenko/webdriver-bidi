using OpenQA.Selenium.BiDi.Modules.Log;
using System.Threading.Tasks;
using System;

namespace OpenQA.Selenium.BiDi.Modules.BrowsingContext;

public class BrowsingContextLogModule(BrowsingContext context, LogModule logModule)
{
    public Task<Subscription> OnEntryAddedAsync(Func<BaseLogEntry, Task> callback)
    {
        return logModule.OnEntryAddedAsync(async args =>
        {
            if (args.Source.Context?.Equals(context) is true)
            {
                await callback(args).ConfigureAwait(false);
            }
        });
    }

    public Task<Subscription> OnEntryAddedAsync(Action<BaseLogEntry> callback)
    {
        return logModule.OnEntryAddedAsync(args =>
        {
            if (args.Source.Context?.Equals(context) is true)
            {
                callback(args);
            }
        });
    }
}
