using FluentAssertions;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using OpenQA.Selenium.BiDi.Modules.Input;
using OpenQA.Selenium.BiDi.Modules.Log;
using OpenQA.Selenium.BiDi.Modules.Network;
using OpenQA.Selenium.BiDi.Modules.Script;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Linq;
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
                BrowserVersion = "beta",
            };

            driver = new ChromeDriver(options);

            session = await driver.AsBidirectionalAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            if (driver is not FirefoxDriver) // unsupported operation: Ending session which was started with Webdriver classic is not supported, use Webdriver classic delete command instead.
            {
                await session.EndAsync();
            }

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
            var context = await driver.AsBidirectionalBrowsingContextAsync();

            var navigateResult = await context.NavigateAsync("https://google.com");

            navigateResult.Navigation.Should().NotBeNull();
            navigateResult.Url.Should().Contain("google.com");
        }

        [Test]
        public async Task GetTree()
        {
            var session = await driver.AsBidirectionalAsync();

            var contexts = await session.GetTreeAsync();

            await session.GetTreeAsync(new() { Root = contexts[0].Context });
        }

        [Test]
        public async Task Print()
        {
            var context = await driver.AsBidirectionalBrowsingContextAsync();

            var res = await context.PrintAsync(new() { Background = true });

            res.Should().NotBeNull();
        }

        [Test]
        public async Task BrowserUserContext()
        {
            var session = await driver.AsBidirectionalAsync();

            var userContextInfo = await session.CreateBrowserUserContextAsync();

            var allUserInfoContexts = await session.GetBrowserUserContextsAsync();

            allUserInfoContexts.Select(uc => uc.UserContext.Id).Should().Contain(userContextInfo.UserContext.Id);

            await userContextInfo.UserContext.RemoveAsync();
        }

        [Test]
        public async Task OnBrowsingContextCreated()
        {
            var session = await driver.AsBidirectionalAsync();

            await session.OnBrowsingContextCreatedAsync(async e => await e.Context.NavigateAsync("https://selenium.dev"));

            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await Task.Delay(500);

            string url = await context.EvaluateAsync("window.location.href", true);

            url.Should().Be("https://www.selenium.dev/");
        }

        [Test]
        public async Task OnBrowsingContextDestroyed()
        {
            var session = await driver.AsBidirectionalAsync();

            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            BrowsingContext destroyedContext = null;

            await session.OnBrowsingContextDestroyedAsync(e => destroyedContext = e.Context);

            await context.CloseAsync();

            await Task.Delay(500);

            destroyedContext.Should().Be(context);
        }

        [Test]
        public async Task OnUserPromptOpened()
        {
            var session = await driver.AsBidirectionalAsync();

            UserPromptClosedEventArgs args = null;

            await session.OnUserPromptOpenedAsync(async e => await e.Context.HandleUserPromptAsync(new() { Accept = true, UserText = "hi" }));
            await session.OnUserPromptClosedAsync(e => args = e);

            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.EvaluateAsync("prompt()", true);

            await Task.Delay(100);

            args.Should().NotBeNull();
            args.UserText.Should().Be("hi");
        }

        [Test]
        public async Task SetViewport()
        {
            var context = await driver.AsBidirectionalBrowsingContextAsync();

            await context.NavigateAsync("https://google.com");

            await context.SetViewportAsync(new() { Viewport = new(500, 300) });
        }

        [Test]
        public async Task BrowsingContextTraverseHistory()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.NavigateAsync("https://google.com");

            await Task.Delay(500);

            await context.NavigateBackAsync();

            await Task.Delay(500);

            await context.NavigateForwardAsync();
        }

        [Test]
        public async Task BrowsingContextTest()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.ActivateAsync();

            await context.CloseAsync();
        }

        [Test]
        public async Task Screenshot()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.NavigateAsync("https://selenium.dev");

            var screenshot = await context.CaptureScreenshotAsync();

            screenshot.Data.Should().NotBeNullOrEmpty();
            screenshot.AsBytes().Should().NotBeNullOrEmpty();

            var screenshot2 = await context.CaptureScreenshotAsync(new() { Origin = Origin.Document });
            screenshot2.Data.Should().NotBeNullOrEmpty();
            screenshot2.Data.Should().NotBe(screenshot.Data);

            var screenshotPng = await context.CaptureScreenshotAsync(new() { Origin = Origin.Document, Format = new ImageFormat("image/png") });
            var screenshotJpeg = await context.CaptureScreenshotAsync(new() { Origin = Origin.Document, Format = new ImageFormat("image/jpeg") });

            screenshotPng.Data.Should().NotBe(screenshotJpeg.Data);
        }

        [Test]
        public async Task SubscribeTest()
        {
            await session.OnBeforeRequestSentAsync(Console.WriteLine);

            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.NavigateAsync("https://selenium.dev", new() { Wait = ReadinessState.Complete });
        }

        [Test]
        public async Task UnsubscribeTest()
        {
            string url1 = null;

            var subscription1 = await session.OnBeforeRequestSentAsync(e =>
            {
                url1 = e.Request.Url;
            });

            string url2 = null;

            var subscription2 = await session.OnBeforeRequestSentAsync(e =>
            {
                url2 = e.Request.Url;
            });

            await subscription2.UnsubscribeAsync();

            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.NavigateAsync("https://selenium.dev");

            url1.Should().NotBeNull();
            url2.Should().BeNull();
        }

        [Test]
        public async Task Navigate()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            NavigationInfo info = null;

            await context.OnNavigationStartedAsync(args =>
            {
                info = args;
            });

            await context.NavigateAsync("https://selenium.dev");

            info.Context.Should().NotBeNull();
            info.Timestamp.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(30));
            // info.Navigation.Should().NotBeNull();
            // info.Url.Should().Contain("selenium.dev");
        }

        [Test]
        public async Task Reload()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.NavigateAsync("https://selenium.dev");

            var info = await context.ReloadAsync();

            info.Navigation.Should().BeNull();
            info.Url.Should().Contain("selenium.dev");
        }

        [Test]
        public async Task GetRealms()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.NavigateAsync("https://selenium.dev");

            var realms = await context.GetRealmsAsync();

            realms.Should().HaveCount(1);
        }

        [Test]
        public async Task AddPreloadScript()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await using var script = await context.AddPreloadScriptAsync("prompt()");

            await context.NavigateAsync("https://selenium.dev", new() { Wait = ReadinessState.None });

            await context.HandleUserPromptAsync();
        }

        [Test]
        public async Task GetCookies()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            var res = await context.GetCookiesAsync();

            res.PartitionKey.UserContext.Should().Be("default");
        }

        [Test]
        public async Task DeleteCookies()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            var partitionKey = await context.DeleteCookiesAsync();

            partitionKey.UserContext.Should().Be("default");
        }

        [Test]
        public async Task SetCookie()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            var tomorrow = DateTime.Now.AddDays(1);

            var partitionKey = await context.SetCookieAsync(new("test", "value", "domain") { Expiry = tomorrow });

            partitionKey.UserContext.Should().Be("default");

            var cookies = await context.GetCookiesAsync();

            cookies.Cookies.Should().HaveCount(1);
            cookies.Cookies[0].Name.Should().Be("test");
            (cookies.Cookies[0].Value as StringValue).Value.Should().Be("value");
            cookies.Cookies[0].Domain.Should().Be("domain");
            cookies.Cookies[0].Expiry.Should().BeCloseTo(tomorrow, TimeSpan.FromMilliseconds(1)); // microseconds are not considered
        }

        [Test]
        public async Task SubscribeInHandlerTest()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.OnNavigationStartedAsync(async args =>
            {
                await session.OnBeforeRequestSentAsync(args =>
                {
                    Console.WriteLine(args.Request.Url);
                });
            });

            await context.NavigateAsync("https://selenium.dev", new() { Wait = ReadinessState.Complete });
        }

        [Test]
        public async Task OnNavigationStarted()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

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
        public async Task OnFragmentNavigated()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.OnFragmentNavigatedAsync(args =>
            {
                Console.WriteLine($"{DateTime.Now} {args}");
            });

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task OnDomContentLoaded()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.OnDomContentLoadedAsync(args =>
            {
                Console.WriteLine($"{DateTime.Now} {args}");
            });

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task OnLoad()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.OnLoadAsync(args =>
            {
                Console.WriteLine($"{DateTime.Now} {args}");
            });

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task OnDownloadWillBegin()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.OnDownloadWillBeginAsync(args =>
            {
                Console.WriteLine($"{DateTime.Now} {args}");
            });

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task OnNavigationAborted()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.OnNavigationAbortedAsync(args =>
            {
                Console.WriteLine($"{DateTime.Now} {args}");
            });

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task OnNavigationFailed()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.OnNavigationFailedAsync(args =>
            {
                Console.WriteLine($"{DateTime.Now} {args}");
            });

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task OnLogEntryAdded()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            ConsoleLogEntry consoleLog = null;

            await using var subscription = await context.OnLogEntryAddedAsync(e => consoleLog = e as ConsoleLogEntry);

            await context.EvaluateAsync("console.log('abc')", true);

            // think about it
            await Task.Delay(100);

            consoleLog.Should().NotBeNull();
            consoleLog.Text.Should().Be("abc");
        }

        [Test]
        public async Task OnBeforeRequestSent()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.OnBeforeRequestSentAsync(Console.WriteLine);

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task OnResponseCompleted()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.OnResponseCompletedAsync(Console.WriteLine);

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task OnResponseStartedSent()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.OnResponseStartedAsync(Console.WriteLine);

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task InterceptTestContinueRequest()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await using var intercept = await context.OnBeforeRequestSentAsync(interceptOptions: default, async args =>
            {
                await args.Request.Request.ContinueAsync(new() { Method = "POST" });
            });

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task InterceptTestContinueResponse()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await using var intercept = await context.OnResponseStartedAsync(new(), async args =>
            {
                await args.Request.Request.ContinueAsync();
            });

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task InterceptTestFailRequest()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await using var intercept = await context.OnBeforeRequestSentAsync(new() { UrlPatterns = ["https://**"] }, async args =>
            {
                await args.Request.Request.FailAsync();
            });

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task InterceptTestProvideResponse()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await using var intercept = await context.OnBeforeRequestSentAsync(null, async args =>
            {
                await args.Request.Request.ProvideResponseAsync();
            });

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task InterceptTestProvideResponseBody()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await using var intercept = await context.OnBeforeRequestSentAsync(null, async args =>
            {
                await args.Request.Request.ProvideResponseAsync(new() { Body = $"""
                    <html>
                        <body>
                            <h1 id=\"id1\">Request to {args.Request.Url} has been hijacked!</h1>
                        </body>
                    </html>
                    """ });
            });

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task InterceptTestProvideResponse2()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.OnResponseStartedAsync(new(), async args =>
            {
                await args.Request.Request.ProvideResponseAsync(new() { StatusCode = 200 });
            });

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task InterceptTestAll()
        {
            await session.OnBeforeRequestSentAsync(new InterceptOptions(), async args =>
            {
                await args.Request.Request.ContinueAsync(new() { Method = "POST" });
            });

            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.NavigateAsync("https://selenium.dev");
        }

        [Test]
        public async Task UseClosedBrowserContext()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.CloseAsync();

            Func<Task> func = async () => await context.NavigateAsync("https://selenium.dev");
            await func.Should().ThrowExactlyAsync<BiDiException>();
        }

        [Test]
        public async Task SubscribeOnBrowsingContextCreated()
        {
            BrowsingContextInfo args = null;

            //TODO: await event handler to be invoked
            await session.OnBrowsingContextCreatedAsync(e => args = e);

            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            args.Should().NotBeNull();
            args.Url.Should().NotBeEmpty();
            args.Context.Should().NotBeNull();
        }

        [Test]
        public async Task LocateNodes()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.NavigateAsync("https://selenium.dev", new() { Wait = ReadinessState.Complete });

            var nodes = await context.LocateNodesAsync(Locator.Css("div"));

            foreach (var node in nodes)
            {
                Console.WriteLine(node);
            }
        }

        [Test]
        public async Task InputActions()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.NavigateAsync("https://nuget.org", new() { Wait = ReadinessState.Complete });

            //var searchInput = (await context.LocateNodesAsync(Locator.Css("#search"))).First();

            await context.PerformActionsAsync(
                [
                    SourceActions.Press("abc"),
                    new KeySourceActions()
                    {
                        Actions =
                        {
                            new KeyDownAction("H"),
                            new KeyDownAction("i"),
                            new KeyDownAction(",")
                        }
                    }.Press("World").Pause(1000).Press("!").Pause(1000)
                 ]);

            await context.PerformActionsAsync([SourceActions.Press("qwe").Pause(1000)]);
        }

        [Test]
        public async Task EvaluateException()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            var func = async () => await context.EvaluateAsync("abc;", true);

            var subject = await func.Should().ThrowAsync<ScriptEvaluateException>();

            Console.WriteLine(subject.Which);
        }

        [Test]
        public async Task EvaluateSuccess()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            var evaluateResult = await context.EvaluateAsync("2 + 2", true);

            var numberValue = evaluateResult.Result as NumberRemoteValue;
            numberValue.Value.Should().Be(4);

            int nmb = await context.EvaluateAsync("2 + 3", true);
            nmb.Should().Be(5);

            var str = (string)await context.EvaluateAsync("'qwe' + 'asd'", true);
            str.Should().Be("qweasd");

            var nullStr = (string)await context.EvaluateAsync("null", true);
            nullStr.Should().BeNull();

            var invalidStr = async () => (string)await context.EvaluateAsync("function A() { return 'a' }", true);
            await invalidStr.Should().ThrowExactlyAsync<Exception>().WithMessage("Cannot convert*");
        }

        [Test]
        public async Task CallFunction()
        {
            var context = await session.CreateBrowsingContextAsync(BrowsingContextType.Tab);

            await context.NavigateAsync("https://selenium.dev", new() { Wait = ReadinessState.Interactive });

            int sum = await context.CallFunctionAsync("(a, b) => a + b", true, new() { Arguments = [2, 3] });

            sum.Should().Be(5);

            string concat = await context.CallFunctionAsync("function hello(name) { return 'Hello, ' + name; }", true, new() { Arguments = ["World"] });

            concat.Should().Be("Hello, World");

            NodeRemoteValue el = await context.CallFunctionAsync("() => document.querySelector('div')", true);
            Console.WriteLine(el.Value.LocalName);
        }
    }
}
