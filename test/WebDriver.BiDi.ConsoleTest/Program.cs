﻿using OpenQA.Selenium.BiDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

var options = new ChromeOptions
{
    UseWebSocketUrl = true,
};

var driver = new ChromeDriver(options);

var bidi = await BiDiSession.ConnectAsync(((IHasCapabilities)driver).Capabilities.GetCapability("webSocketUrl").ToString()!);

bidi.Network.BeforeRequestSent += args => {  Console.WriteLine(args.Request.Url); };

var context = await bidi.CreateBrowsingContextAsync();

context.NavigationStarted += async e => { await Task.Delay(3000); Console.WriteLine(e); };

await context.NavigateAsync("https://google.com");

await context.CloseAsync();

driver.Quit();
