using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi;

public static class WebDriverExtensions
{
    public static async Task<Session> AsBiDiSessionAsync(this IWebDriver webDriver)
    {
        var session = await Session.ConnectAsync(((IHasCapabilities)webDriver).Capabilities.GetCapability("webSocketUrl").ToString()!).ConfigureAwait(false);

        return session;
    }

    public static async Task<BrowsingContext> AsBiDiBrowsingContext(this IWebDriver webDriver)
    {
        var session = await webDriver.AsBiDiSessionAsync();

        var currentBrowsingContext = new BrowsingContext(session, webDriver.CurrentWindowHandle);

        return currentBrowsingContext;
    }
}
