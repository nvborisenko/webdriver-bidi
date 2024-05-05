using FluentAssertions;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using OpenQA.Selenium.BiDi.Modules.Input;
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
        Session session;

        [SetUp]
        public async Task Setup()
        {
            //FirefoxOptions firefoxOptions = new FirefoxOptions
            //{
            //    UseWebSocketUrl = true,
            //    BrowserVersion = "beta"
            //};

            //driver = new FirefoxDriver(firefoxOptions);

            var options = new ChromeOptions
            {
                UseWebSocketUrl = true,
                BrowserVersion = "beta"
            };

            driver = new ChromeDriver(options);

            session = await Session.ConnectAsync(((IHasCapabilities)driver).Capabilities.GetCapability("webSocketUrl").ToString()!);
        }

        [TearDown]
        public async Task TearDown()
        {
            await session.EndAsync();

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

            await context.ActivateAsync();

            await context.CloseAsync();
        }

        [Test]
        public async Task Screenshot()
        {
            var context = await session.CreateBrowsingContextAsync();

            await context.NavigateAsync("https://selenium.dev");

            var screenshot = await context.CaptureScreenshotAsync();

            screenshot.Data.Should().NotBeNullOrEmpty();
            screenshot.AsBytes().Should().NotBeNullOrEmpty();

            var screenshot2 = await context.CaptureScreenshotAsync(Origin.Document);
            screenshot2.Data.Should().NotBeNullOrEmpty();
            screenshot2.Data.Should().NotBe(screenshot.Data);

            var screenshotPng = await context.CaptureScreenshotAsync(Origin.Document, new ImageFormat("image/png"));
            var screenshotJpeg = await context.CaptureScreenshotAsync(Origin.Document, new ImageFormat("image/jpeg"));

            screenshotPng.Data.Should().NotBe(screenshotJpeg.Data);
        }

        [Test]
        public async Task SubscribeTest()
        {
            await session.Network.OnBeforeRequestSentAsync(e =>
            {
                Console.WriteLine(e.Request.Url);
            });

            var context = await session.CreateBrowsingContextAsync();

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task SubscribeInHandlerTest()
        {
            var context = await session.CreateBrowsingContextAsync();

            await context.OnNavigationStartedAsync(async args =>
            {
                await session.Network.OnBeforeRequestSentAsync(args =>
                {
                    Console.WriteLine(args.Request.Url);
                });
            });

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task OnNavigationStartedTest()
        {
            var context = await session.CreateBrowsingContextAsync();

            await context.OnNavigationStartedAsync(async args =>
            {
                await Task.Delay(1000); Console.WriteLine($"{DateTime.Now} {args} - Async");
            });

            await context.OnNavigationStartedAsync(args =>
            {
                Thread.Sleep(1000); Console.WriteLine($"{DateTime.Now} {args} - Sync");
            });

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task InterceptTest()
        {
            await session.Network.AddInterceptAsync(
                phases: [Modules.Network.InterceptPhase.BeforeRequestSent],
                urlPatterns: [Modules.Network.UrlPattern.String("https://selenium.dev/")]);

            var context = await session.CreateBrowsingContextAsync();

            //await session.Network.OnBeforeRequestSentAsync(args => throw new Exception("Blocked"));

            await session.Network.OnBeforeRequestSentAsync(async args =>
            {
                if (args.IsBlocked)
                {
                    await args.ContinueRequestAsync(method: "post");
                }
            });

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task UseClosedBrowserContext()
        {
            var context = await session.CreateBrowsingContextAsync();

            await context.CloseAsync();

            Func<Task> func = async () => await context.NavigateAsync("https://selenium.dev");
            await func.Should().ThrowExactlyAsync<BiDiException>();
        }

        [Test]
        public async Task SubscribeOnBrowsingContextCreated()
        {
            BrowsingContextInfoEventArgs args = null;

            await session.OnBrowsingContextCreatedAsync(e => args = e);

            var context = await session.CreateBrowsingContextAsync();

            args.Should().NotBeNull();
            args.Url.Should().NotBeEmpty();
            args.Context.Should().NotBeNull();
        }

        [Test]
        public async Task LocateNodes()
        {
            var context = await session.CreateBrowsingContextAsync();

            await context.NavigateAsync("https://selenium.dev");

            var nodes = await context.LocateNodesAsync(Locator.Css("div"));

            Console.WriteLine(nodes[0].SharedId);
        }

        [Test]
        public async Task InputActions()
        {
            var context = await session.CreateBrowsingContextAsync();

            await context.NavigateAsync("https://nuget.org");

            //var searchInput = (await context.LocateNodesAsync(Locator.Css("#search"))).First();

            await session.Input.PerformActionsAsync(new()
            {
                Context = context.Id,
                Actions =
                {
                    SourceActions.Press("abc"),
                    new KeySourceActions()
                    {
                        Actions =
                        {
                            new KeyDownAction
                            {
                                Value = "H"
                            },
                            new KeyDownAction
                            {
                                Value = "i"
                            },
                            new KeyDownAction
                            {
                                Value = ","
                            }
                        }
                    }.Press("World").Pause(1000).Press("!").Pause(1000)
                }
            });

            await context.PerformActionsAsync([SourceActions.Press("qwe").Pause(1000)]);
        }
    }
}
