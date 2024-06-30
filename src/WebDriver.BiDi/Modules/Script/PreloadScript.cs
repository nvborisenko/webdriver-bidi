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

    public async Task RemoveAsync()
    {
        var @params = new RemovePreloadScriptCommandParameters(this);

        await _session.ScriptModule.RemovePreloadScriptAsync(@params).ConfigureAwait(false);
    }

    public async ValueTask DisposeAsync()
    {
        await RemoveAsync().ConfigureAwait(false);
    }
}
