using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class Request
{
    private readonly BiDi.Session _session;

    internal Request(BiDi.Session session, string id)
    {
        _session = session;
        Id = id;
    }

    public string Id { get; private set; }

    public Task ContinueAsync(RequestOptions? options = default)
    {
        return _session.NetworkModule.ContinueRequestAsync(this, options);
    }

    public Task FailAsync()
    {
        return _session.NetworkModule.FailRequestAsync(this);
    }

    public Task ProvideResponseAsync(ProvideResponseOptions? options = default)
    {
        return _session.NetworkModule.ProvideResponseAsync(this, options);
    }

    public Task ContinueResponseAsync(ContinueResponseOptions? options = default)
    {
        return _session.NetworkModule.ContinueResponseAsync(this, options);
    }
}
