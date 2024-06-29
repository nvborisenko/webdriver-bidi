using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Browser;

public class UserContext
{
    private readonly BiDi.Session _session;

    internal UserContext(BiDi.Session session, string id)
    {
        _session = session;
        Id = id;
    }

    public string Id { get; }

    public async Task RemoveAsync()
    {
        var @params = new RemoveUserContextCommandParameters(this);

        await _session.BrowserModule.RemoveUserContextAsync(@params).ConfigureAwait(false);
    }
}
