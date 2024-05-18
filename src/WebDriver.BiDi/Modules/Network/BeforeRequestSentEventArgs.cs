using System.Net.Http;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class BeforeRequestSentEventArgs : BaseParametersEventArgs
{
    public override string ToString()
    {
        return $"{Request.Method} {Request.Url}";
    }

    public Task ContinueAsync(string? method = default)
    {
        IsBlocked = false;

        return Request.Request.ContinueAsync(method);
    }

    public Task FailAsync()
    {
        IsBlocked = false;

        return Request.Request.FailAsync();
    }

    public Task ProvideAsync(uint? statusCode = default)
    {
        IsBlocked = false;

        return Request.Request.ProvideResponseAsync(statusCode);
    }
}
