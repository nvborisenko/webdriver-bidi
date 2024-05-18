using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class ResponseStartedEventArgs : BaseParametersEventArgs
{
    [JsonInclude]
    public ResponseData Response { get; private set; }

    public Task ContinueResponseAsync(uint? statusCode = default)
    {
        return Request.Request.ContinueResponseAsync(statusCode);
    }

    public override string ToString()
    {
        return $"Url: {Response.Url}, Protocol: {Response.Protocol}";
    }
}
