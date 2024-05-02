using OpenQA.Selenium.BiDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

var options = new ChromeOptions
{
    UseWebSocketUrl = true,
};

using var driver = new ChromeDriver(options);

using var bidi = await Session.ConnectAsync(((IHasCapabilities)driver).Capabilities.GetCapability("webSocketUrl").ToString()!);

var context = await bidi.CreateBrowsingContextAsync();

await context.OnNavigationStartedAsync(async e =>
{
    await Task.Delay(2000); Console.WriteLine($"Navigation started: {e}");
});

await context.NavigateAsync("https://google.com");
