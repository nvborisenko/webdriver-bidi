﻿using OpenQA.Selenium.BiDi;
using OpenQA.Selenium.Chrome;

namespace WebDriver.BiDi.Mirror
{
    public partial class MainPage : ContentPage
    {
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

            await using var session = await driver.AsBidirectionalAsync();

            for (int i = 0; i < 5; i++)
            {
                var context = await session.CreateBrowsingContextAsync(OpenQA.Selenium.BiDi.Modules.BrowsingContext.BrowsingContextType.Tab);

                CounterBtn.Text = "Started";

                await session.Network.OnBeforeRequestSentAsync(async arg =>
                {
                    await Task.Delay(10);
                    MainThread.BeginInvokeOnMainThread(() => { CounterBtn.Text = arg.Request.Url; });
                });

                await context.NavigateAsync("https://google.com");

                await context.CloseAsync();

                //await Task.Delay(1_000);
            }
        }
    }

}
