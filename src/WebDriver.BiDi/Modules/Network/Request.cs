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
        return _session.Network.ContinueRequestAsync(this, options);
    }

    public Task FailAsync()
    {
        return _session.Network.FailRequestAsync(this);
    }

    public Task ProvideResponseAsync(ProvideResponseOptions? options = default)
    {
        return _session.Network.ProvideResponseAsync(this, options);
    }

    public Task ContinueResponseAsync(ContinueResponseOptions? options = default)
    {
        return _session.Network.ContinueResponseAsync(this, options);
    }

    public Task ContinueWithAuthAsync(AuthCredentials credentials, ContinueWithAuthOptions? options = default)
    {
        return _session.Network.ContinueWithAuthAsync(this, credentials, options);
    }

    public Task ContinueWithAuthAsync(ContinueWithDefaultAuthOptions? options = default)
    {
        return _session.Network.ContinueWithAuthAsync(this, options);
    }

    public Task ContinueWithAuthAsync(ContinueWithCancelledAuthOptions? options = default)
    {
        return _session.Network.ContinueWithAuthAsync(this, options);
    }
}
