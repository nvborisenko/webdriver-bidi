using System.Net.Http;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class Request
{
    private readonly BiDi.Session _session;

    public Request(BiDi.Session session, string id)
    {
        _session = session;
        Id = id;
    }

    public string Id { get; private set; }

    internal async Task ContinueAsync(string? method = default)
    {
        var parameters = new ContinueRequestParameters
        {
            Request = this,
            Method = method,
        };

        await _session.Network.ContinueRequestAsync(parameters).ConfigureAwait(false);
    }

    internal async Task FailAsync()
    {
        var parameters = new FailRequestParameters { Request = this };

        await _session.Network.FailRequestAsync(parameters).ConfigureAwait(false);
    }

    internal async Task ProvideResponseAsync(uint? statusCode = default)
    {
        var parameters = new ProvideResponseParameters
        {
            Request = this,
            StatusCode = statusCode
        };

        await _session.Network.ProvideResponseAsync(parameters).ConfigureAwait(false);
    }

    internal async Task ContinueResponseAsync(uint? statusCode = default)
    {
        var parameters = new ContinueResponseParameters
        {
            Request = this,
            StatusCode = statusCode
        };

        await _session.Network.ContinueResponseAsync(parameters).ConfigureAwait(false);
    }
}
