using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Modules.Network;

public class BeforeRequestSentEventArgs : BaseParametersEventArgs
{
    public Task ContinueRequestAsync(string? method = default)
    {
        return Request.Request.ContinueAsync(method);
    }
}
