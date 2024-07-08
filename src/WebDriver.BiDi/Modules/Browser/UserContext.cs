using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Browser;

public class UserContext : IAsyncDisposable
{
    private readonly BiDi.Session _session;

    internal UserContext(BiDi.Session session, string id)
    {
        _session = session;
        Id = id;
    }

    public string Id { get; }

    public Task RemoveAsync()
    {
        return _session.BrowserModule.RemoveUserContextAsync(this);
    }

    public async ValueTask DisposeAsync()
    {
        await RemoveAsync().ConfigureAwait(false);
    }
}
