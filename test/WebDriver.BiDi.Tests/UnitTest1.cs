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
            //    BrowserVersion = "123.0"
            //};

            //driver = new FirefoxDriver(firefoxOptions);

            var options = new ChromeOptions
            {
                UseWebSocketUrl = true,
                //BrowserVersion = "121.0"
            };

            driver = new ChromeDriver(options);

            bidi = await BiDiSession.ConnectAsync(((IHasCapabilities)driver).Capabilities.GetCapability("webSocketUrl").ToString()!);
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Dispose();
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

            await context.CloseAsync();
        }

        [Test]
        public async Task Subscribe()
        {
            bidi.Network.BeforeRequestSent += e => { Console.WriteLine(e.Request.Url); return Task.CompletedTask; };

            using var context = await bidi.CreateBrowsingContextAsync();

            await context.NavigateAsync("https://google.com", ReadinessState.Complete);
        }

        [Test]
        public async Task OnNavigationStarted()
        {
            using var context = await bidi.CreateBrowsingContextAsync();

            context.NavigationStarted += async args => { await Task.Delay(1); Console.WriteLine(args); };
            //context.NavigationStarted += args => { Thread.Sleep(1); Console.WriteLine(args); return Task.CompletedTask; };

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task Intercept()
        {
            await bidi.Network.AddInterceptAsync(new Network.AddInterceptParameters
            {
                Phases = { Network.InterceptPhase.BeforeRequestSent },
                UrlPatterns = { new Network.UrlPatternString { Pattern = "https://selenium.dev/" } }
            });

            // bidi.Network.BeforeRequestSent += (args) => throw new Exception("Blocked");

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

                    Console.WriteLine($"Intercepted {args} request");
                }
            };

            using var context = await bidi.CreateBrowsingContextAsync();

            await context.NavigateAsync("https://selenium.dev");
        }
    }
}
