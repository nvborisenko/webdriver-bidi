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

    internal Task ContinueAsync(RequestOptions? options = default)
    {
        return _session.NetworkModule.ContinueRequestAsync(this, options);
    }

    internal Task FailAsync()
    {
        return _session.NetworkModule.FailRequestAsync(this);
    }

    internal Task ProvideResponseAsync(ProvideResponseOptions? options = default)
    {
        return _session.NetworkModule.ProvideResponseAsync(this, options);
    }

    internal Task ContinueResponseAsync(ContinueResponseOptions? options = default)
    {
        return _session.NetworkModule.ContinueResponseAsync(this, options);
    }
}
