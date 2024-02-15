using OpenQA.Selenium.BiDi;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Chrome;

var options = new ChromeOptions
{
    UseWebSocketUrl = true,
    BrowserVersion = "120.0"
};

var driver = new ChromeDriver(options);

var bidi = await BiDiSession.ConnectAsync(((IHasCapabilities)driver).Capabilities.GetCapability("webSocketUrl").ToString()!);

bidi.Network.BeforeRequestSent += args => { Console.WriteLine(args.Request.Url); };

var context = await bidi.CreateBrowsingContextAsync();

await context.NavigateAsync("https://google.com", ReadinessState.Complete);

await context.CloseAsync();

driver.Quit();
