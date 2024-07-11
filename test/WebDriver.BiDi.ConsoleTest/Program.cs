using OpenQA.Selenium.BiDi;
using OpenQA.Selenium.Chrome;

var options = new ChromeOptions
{
    UseWebSocketUrl = true,
};

using var driver = new ChromeDriver(options);

await using var session = await driver.AsBidirectionalAsync();

var context = await session.BrowsingContext.CreateAsync(OpenQA.Selenium.BiDi.Modules.BrowsingContext.BrowsingContextType.Tab);

await context.OnNavigationStartedAsync(async e =>
{
    await Task.Delay(2000); Console.WriteLine($"Navigation started: {e}");
});

await context.NavigateAsync("https://google.com");