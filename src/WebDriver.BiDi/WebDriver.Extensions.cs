using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi;

public static class WebDriverExtensions
{
    public static async Task<Session> AsBiDiAsync(this IWebDriver webDriver)
    {
        var session = await Session.ConnectAsync(((IHasCapabilities)webDriver).Capabilities.GetCapability("webSocketUrl").ToString()!).ConfigureAwait(false);

        return session;
    }
}
