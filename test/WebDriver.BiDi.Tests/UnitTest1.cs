using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace OpenQA.Selenium.BiDi.Tests
{
    public class Tests
    {
        IWebDriver driver;
        BiDiSession bidi;

        [SetUp]
        public async Task Setup()
        {
            //FirefoxOptions firefoxOptions = new FirefoxOptions();
            //firefoxOptions.UseWebSocketUrl = true;

            //driver = new FirefoxDriver(firefoxOptions);

            var options = new ChromeOptions
            {
                UseWebSocketUrl = true
            };

            driver = new ChromeDriver(options);

            bidi = await BiDiSession.ConnectAsync(((IHasCapabilities)driver).Capabilities.GetCapability("webSocketUrl").ToString()!);
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
        }

        [Test]
        public async Task Session()
        {
            var status = await bidi.StatusAsync();

            Console.WriteLine(status.Message);
        }

        [Test]
        public async Task BrowsingContext()
        {
            var context = await bidi.CreateBrowsingContextAsync();

            await context.NavigateAsync("https://google.com", NavigateWait.Complete);

            await context.CloseAsync();
        }

        [Test]
        public async Task Subscribe()
        {
            //await bidi.SubscribeAsync("network.beforeRequestSent");
            bidi.Network.OnBeforeRequestSent += args => { Console.WriteLine(args.Request.Url); };

            var context = await bidi.CreateBrowsingContextAsync();

            await context.NavigateAsync("https://selenium.dev", NavigateWait.Complete);

            await context.CloseAsync();
        }
    }
}