using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class ResponseStartedEventArgs(ResponseData response) : BaseParametersEventArgs
{
    public ResponseData Response { get; } = response;

    public Task ContinueAsync(uint? statusCode = default)
    {
        return Request.Request.ContinueResponseAsync(statusCode);
    }

    public Task ProvideAsync(uint? statusCoode = default)
    {
        return Request.Request.ProvideResponseAsync(statusCoode);
    }

    public override string ToString()
    {
        return $"Url: {Response.Url}, Protocol: {Response.Protocol}";
    }
}
