using FluentAssertions;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using OpenQA.Selenium.BiDi.Modules.Input;
using OpenQA.Selenium.BiDi.Modules.Script;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V122.Overlay;
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

            session = await driver.AsBiDiSessionAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await session.EndAsync();

            driver?.Dispose();
        }

        [Test]
        public async Task SessionStatusTest()
        {
            var status = await session.StatusAsync();

            Console.WriteLine(status.Message);
        }

        [Test]
        public async Task CurrentBrowsingContext()
        {
            var context = await driver.AsBiDiBrowsingContext();

            var navigateResult = await context.NavigateAsync("https://google.com");

            navigateResult.Navigation.Should().NotBeNull();
            navigateResult.Url.Should().Contain("google.com");
        }

        [Test]
        public async Task SetViewport()
        {
            var context = await driver.AsBiDiBrowsingContext();

            await context.NavigateAsync("https://google.com");

            await context.SetViewportAsync(new Viewport { Width = 500, Height = 300 });
        }

        [Test]
        public async Task BrowsingContextTraverseHistory()
        {
            var context = await session.CreateBrowsingContextAsync();

            await context.NavigateAsync("https://google.com");

            await Task.Delay(500);

            await context.NavigateBackAsync();

            await Task.Delay(500);

            await context.NavigateForwardAsync();
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
        public async Task Navigate()
        {
            var context = await session.CreateBrowsingContextAsync();

            NavigationInfoEventArgs info = null;

            await context.OnNavigationStartedAsync(args =>
            {
                info = args;
            });

            await context.NavigateAsync("https://selenium.dev");

            info.Context.Should().NotBeNull();
            // info.Url.Should().Contain("selenium.dev");
        }

        [Test]
        public async Task Reload()
        {
            var context = await session.CreateBrowsingContextAsync();

            await context.NavigateAsync("https://selenium.dev");

            var info = await context.ReloadAsync();

            info.Navigation.Should().BeNull();
            info.Url.Should().Contain("selenium.dev");
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
                urlPatterns: ["https://selenium.dev/"]);

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
        public async Task InterceptTestAll()
        {
            await session.Network.AddInterceptAsync(Modules.Network.InterceptPhase.BeforeRequestSent);

            var context = await session.CreateBrowsingContextAsync();

            await session.Network.OnBeforeRequestSentAsync(async args =>
            {
                await args.ContinueRequestAsync(method: "post");
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

            //TODO: await event handler to be invoked
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
                Context = context,
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

        [Test]
        public async Task EvaluateException()
        {
            var context = await session.CreateBrowsingContextAsync();

            var res = await context.EvaluateAsync("abc;", true);

            res.GetType().Should().Be(typeof(EvaluateResultException));
        }

        [Test]
        public async Task EvaluateSuccess()
        {
            var context = await session.CreateBrowsingContextAsync();

            var evaluateResult = await context.EvaluateAsync("2 + 2", true);

            evaluateResult.GetType().Should().Be(typeof(EvaluateResultSuccess));

            var resSuccess = evaluateResult as EvaluateResultSuccess;
            var numberValue = resSuccess.Result as NumberValue;
            numberValue.Value.Should().Be(4);

            int nmb = await context.EvaluateAsync("2 + 3", awaitPromise: true);
            nmb.Should().Be(5);

            var str = (string)await context.EvaluateAsync("'qwe' + 'asd'", true);
            str.Should().Be("qweasd");

            var nullStr = (string)await context.EvaluateAsync("null", true);
            nullStr.Should().BeNull();

            var invalidStr = async () => (string)await context.EvaluateAsync("function A() { return 'a' }", true);
            await invalidStr.Should().ThrowExactlyAsync<Exception>().WithMessage("Cannot convert*");
        }
    }
}
