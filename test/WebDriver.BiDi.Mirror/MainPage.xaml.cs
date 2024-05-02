using OpenQA.Selenium.BiDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebDriver.BiDi.Mirror
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            Task.Run(async () =>
            {
                while (true)
                {
                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        await dotNetBot.RotateTo(360, 2000);
                        await dotNetBot.RotateTo(0, 2000);
                    });
                }
            });
        }

        private async void OnCounterClicked(object sender, System.EventArgs e)
        {
            var options = new ChromeOptions
            {
                UseWebSocketUrl = true,
            };

            using var driver = new ChromeDriver(options);

            for (int i = 0; i < 5; i++)
            {
                var session = await Session.ConnectAsync(((IHasCapabilities)driver).Capabilities.GetCapability("webSocketUrl").ToString()!);

                var context = await session.CreateBrowsingContextAsync();

                CounterBtn.Text = "Started";

                await session.Network.OnBeforeRequestSentAsync(async arg =>
                {
                    //await Task.Delay(100);
                    //MainThread.BeginInvokeOnMainThread(() => { CounterBtn.Text = arg.Request.Url; }); // вот так работает
                    //Thread.Sleep(100);
                    //CounterBtn.Text = arg.Request.Url; // а вот так не работает
                    //await Task.Delay(1000);

                    CounterBtn.Text = (await session.StatusAsync()).Message;
                });

                await context.NavigateAsync("https://google.com");

                await context.CloseAsync();

                //await Task.Delay(1_000);

                await session.DisposeAsync();
            }
        }
    }

}
