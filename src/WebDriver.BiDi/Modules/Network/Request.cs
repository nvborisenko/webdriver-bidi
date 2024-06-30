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

    internal async Task ContinueAsync(string? method = default)
    {
        var @params = new ContinueRequestCommandParameters(this)
        {
            Method = method,
        };

        await _session.NetworkModule.ContinueRequestAsync(@params).ConfigureAwait(false);
    }

    internal async Task FailAsync()
    {
        var @params = new FailRequestCommandParameters(this);

        await _session.NetworkModule.FailRequestAsync(@params).ConfigureAwait(false);
    }

    internal async Task ProvideResponseAsync(uint? statusCode = default)
    {
        var @params = new ProvideResponseCommandParameters(this)
        {
            StatusCode = statusCode
        };

        await _session.NetworkModule.ProvideResponseAsync(@params).ConfigureAwait(false);
    }

    internal async Task ContinueResponseAsync(uint? statusCode = default)
    {
        var @params = new ContinueResponseCommandParameters(this)
        {
            StatusCode = statusCode
        };

        await _session.NetworkModule.ContinueResponseAsync(@params).ConfigureAwait(false);
    }
}
