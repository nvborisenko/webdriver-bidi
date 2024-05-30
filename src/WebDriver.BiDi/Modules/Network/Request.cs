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
        var @params = new ContinueRequestCommand.Parameters
        {
            Request = this,
            Method = method,
        };

        await _session.Network.ContinueRequestAsync(@params).ConfigureAwait(false);
    }

    internal async Task FailAsync()
    {
        var @params = new FailRequestCommand.Parameters { Request = this };

        await _session.Network.FailRequestAsync(@params).ConfigureAwait(false);
    }

    internal async Task ProvideResponseAsync(uint? statusCode = default)
    {
        var @params = new ProvideResponseCommand.Parameters
        {
            Request = this,
            StatusCode = statusCode
        };

        await _session.Network.ProvideResponseAsync(@params).ConfigureAwait(false);
    }

    internal async Task ContinueResponseAsync(uint? statusCode = default)
    {
        var @params = new ContinueResponseCommand.Parameters
        {
            Request = this,
            StatusCode = statusCode
        };

        await _session.Network.ContinueResponseAsync(@params).ConfigureAwait(false);
    }
}
