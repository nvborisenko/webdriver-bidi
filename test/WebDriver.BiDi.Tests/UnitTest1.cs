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
            //FirefoxOptions firefoxOptions = new FirefoxOptions
            //{
            //    UseWebSocketUrl = true,
            //    BrowserVersion = "120.0"
            //};

            //driver = new FirefoxDriver(firefoxOptions);

            var options = new ChromeOptions
            {
                UseWebSocketUrl = true,
                BrowserVersion = "120.0"
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

            await context.NavigateAsync("https://google.com", ReadinessState.Complete);

            await context.CloseAsync();
        }

        [Test]
        public async Task Subscribe()
        {
            await bidi.SubscribeAsync("network.beforeRequestSent");
            bidi.Network.BeforeRequestSent += (sender, args) => { Console.WriteLine(args.Request.Url); };

            var context = await bidi.CreateBrowsingContextAsync();

            await context.NavigateAsync("https://google.com", ReadinessState.Complete);

            await context.CloseAsync();
        }

        [Test]
        public async Task Intercept()
        {
            await bidi.Network.AddInterceptAsync(new Network.AddInterceptParameters
            {
                Phases = { Network.InterceptPhase.BeforeRequestSent },
                UrlPatterns = { new Network.UrlPatternString { Pattern = "https://selenium.dev/" } }
            });

            //bidi.Network.BeforeRequestSent += (sender, args) => throw new Exception("Blocked");

            bidi.Network.BeforeRequestSent += async (sender, args) =>
            {
                throw new Exception("Blocked222");
                if (args.IsBlocked)
                {
                    await bidi.Network.ContinueRequestAsync(new Network.ContinueRequestParameters
                    {
                        RequestId = args.Request.Id,
                        Method = "POST"
                    });
                }
            };

            var context = await bidi.CreateBrowsingContextAsync();

            await context.NavigateAsync("https://selenium.dev", ReadinessState.Complete);

            await context.CloseAsync();
        }
    }
}