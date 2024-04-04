using OpenQA.Selenium;
using OpenQA.Selenium.BiDi;
using OpenQA.Selenium.Chrome;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebDriver.BiDi.WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var options = new ChromeOptions
            {
                UseWebSocketUrl = true,
                //BrowserVersion = "121.0"
            };

            using var driver = new ChromeDriver(options);

            await Task.Run(async () =>
            {
                using var bidi = await BiDiSession.ConnectAsync(((IHasCapabilities)driver).Capabilities.GetCapability("webSocketUrl").ToString()!);

                using var context = await bidi.CreateBrowsingContextAsync();

                context.NavigationStarted += async args => { await Task.Delay(1000); Console.WriteLine($"{DateTime.Now} {args}"); };
                context.NavigationStarted += args => { Thread.Sleep(1000); Console.WriteLine($"{DateTime.Now} {args}"); return Task.CompletedTask; };

                await context.NavigateAsync("https://selenium.dev");
            });
        }
    }
}