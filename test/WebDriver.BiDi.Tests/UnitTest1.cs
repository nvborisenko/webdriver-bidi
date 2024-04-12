//using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Tests
{
    //[Parallelizable(ParallelScope.Children)]
    //[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class Tests
    {
        IWebDriver driver;
        BiDiSession session;

        [SetUp]
        public async Task Setup()
        {
            //FirefoxOptions firefoxOptions = new FirefoxOptions
            //{
            //    UseWebSocketUrl = true,
            //    BrowserVersion = "124.0"
            //};

            //driver = new FirefoxDriver(firefoxOptions);

            var options = new ChromeOptions
            {
                UseWebSocketUrl = true,
                //BrowserVersion = "121.0"
            };

            driver = new ChromeDriver(options);

            session = await BiDiSession.ConnectAsync(((IHasCapabilities)driver).Capabilities.GetCapability("webSocketUrl").ToString()!);
        }

        [TearDown]
        public void TearDown()
        {
            session?.Dispose();
            driver?.Dispose();
        }

        [Test]
        public async Task SessionTest()
        {
            var status = await session.StatusAsync();

            Console.WriteLine(status.Message);
        }

        [Test]
        public async Task BrowsingContextTest()
        {
            var context = await session.CreateBrowsingContextAsync();

            await context.CloseAsync();
        }

        [Test]
        public async Task SubscribeTest()
        {
            session.Network.BeforeRequestSent += e => { Console.WriteLine(e.Request.Url); return Task.CompletedTask; };

            using var context = await session.CreateBrowsingContextAsync();

            await context.NavigateAsync("https://google.com");
        }

        [Test]
        public async Task OnNavigationStartedTest()
        {
            using var context = await session.CreateBrowsingContextAsync();

            context.NavigationStarted += async args => { await Task.Delay(200); Console.WriteLine($"{DateTime.Now} {args}"); };
            context.NavigationStarted += args => { Thread.Sleep(200); Console.WriteLine($"{DateTime.Now} {args}"); return Task.CompletedTask; };

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task InterceptTest()
        {
            await session.Network.AddInterceptAsync(new Network.AddInterceptParameters
            {
                Phases = { Network.InterceptPhase.BeforeRequestSent },
                UrlPatterns = { new Network.UrlPatternString { Pattern = "https://selenium.dev/" } }
            });

            using var context = await session.CreateBrowsingContextAsync();

            //session.Network.BeforeRequestSent += (args) => throw new Exception("Blocked");

            session.Network.BeforeRequestSent += async args =>
            {
                if (args.IsBlocked)
                {
                    await args.ContinueRequestAsync(method: "post");
                }
            };

            await context.NavigateAsync("https://selenium.dev");
        }
    }
}
