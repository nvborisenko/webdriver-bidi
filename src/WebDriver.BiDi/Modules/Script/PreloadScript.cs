using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Script;

public class PreloadScript : IAsyncDisposable
{
    readonly BiDi.Session _session;

    public PreloadScript(BiDi.Session session, string id)
    {
        _session = session;
        Id = id;
    }

    public string Id { get; }

    public Task RemoveAsync()
    {
        return _session.ScriptModule.RemovePreloadScriptAsync(this);
    }

    public async ValueTask DisposeAsync()
    {
        await RemoveAsync().ConfigureAwait(false);
    }
}
