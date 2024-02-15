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
                BrowserVersion = "121.0"
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
            bidi.Network.BeforeRequestSent += e => { Console.WriteLine(e.Request.Url); };

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

            //bidi.Network.BeforeRequestSent += (args) => throw new Exception("Blocked");

            bidi.Network.BeforeRequestSent += async args =>
            {
                Console.WriteLine($"BeforeRequestSent {args} request...");

                if (args.IsBlocked)
                {
                    Console.WriteLine($"Intercepting {args} request...");

                    await bidi.Network.ContinueRequestAsync(new Network.ContinueRequestParameters
                    {
                        Request = args.Request.Id,
                        //Method = "POST"
                    });
                }
            };

            var context = await bidi.CreateBrowsingContextAsync();

            await context.NavigateAsync("https://selenium.dev");

            await context.CloseAsync();
        }
    }
}