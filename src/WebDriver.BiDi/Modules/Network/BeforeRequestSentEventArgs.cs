using System.Net.Http;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class BeforeRequestSentEventArgs : BaseParametersEventArgs
{
    public Task ContinueRequestAsync(HttpMethod? method = default)
    {
        return Request.Request.ContinueAsync(method);
    }

    public Task FailRequestAsync()
    {
        return Request.Request.FailAsync();
    }

    public Task ProvideResponseAsync(uint? statusCode = default)
    {
        return Request.Request.ProvideResponseAsync(statusCode);
    }
}
