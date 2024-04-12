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

            using var bidi = await BiDiSession.ConnectAsync(((IHasCapabilities)driver).Capabilities.GetCapability("webSocketUrl").ToString()!);
            var count = 0;
            bidi.Network.BeforeRequestSent += async args => { count++; await Dispatcher.InvokeAsync(() => OutputTextBox.Text = $"{args.Request.Url}\n"); };
            //bidi.Network.BeforeRequestSent += args => { OutputTextBox.Text = $"{args.Request.Url}\n"; return Task.CompletedTask; };

            using var context = await bidi.CreateBrowsingContextAsync();

            for (int i = 0; i < 5; i++)
            {
                await context.NavigateAsync("https://selenium.dev");
            }

            this.Title = count.ToString();
        }

        private void OutputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //OutputTextBox.ScrollToEnd();
        }
    }
}