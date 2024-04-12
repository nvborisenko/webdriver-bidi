using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.BiDi.Session;
using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi
{
    public class BiDiSession : IDisposable
    {
        private readonly Transport _transport;
        private readonly Broker _broker;

        private readonly Lazy<Browser.BrowserModule> _browserModule;
        private readonly Lazy<Network.NetworkModule> _networkModule;

        internal BiDiSession(string uri)
        {
            _transport = new Transport(new Uri(uri));
            _broker = new Broker(this, _transport);

            _browserModule = new Lazy<Browser.BrowserModule>(() => new Browser.BrowserModule(_broker));
            _networkModule = new Lazy<Network.NetworkModule>(() => new Network.NetworkModule(this, _broker));
        }

        public Browser.BrowserModule Browser => _browserModule.Value;

        public Network.NetworkModule Network => _networkModule.Value;

        public async Task<StatusResult> StatusAsync()
        {
            return await _broker.ExecuteCommandAsync<StatusCommand, StatusResult>(new StatusCommand()).ConfigureAwait(false);
        }

        public async Task<BrowsingContextModule> CreateBrowsingContextAsync()
        {
            var context = await _broker.ExecuteCommandAsync<CreateCommand, CreateResult>(new CreateCommand()).ConfigureAwait(false);

            return new BrowsingContextModule(context.Context, this, _broker);
        }

        public async Task SubscribeAsync(params string[] events)
        {
            await _broker.ExecuteCommandAsync(new SubscribeCommand() { Params = new SubscriptionCommandParameters { Events = events } }).ConfigureAwait(false);
        }

        private async Task ConnectAsync()
        {
            await _broker.ConnectAsync(default).ConfigureAwait(false);
        }

        public static async Task<BiDiSession> ConnectAsync(string url)
        {
            var bidiSession = new BiDiSession(url);

            await bidiSession.ConnectAsync().ConfigureAwait(false);

            return bidiSession;
        }

        public void Dispose()
        {
            _transport?.Dispose();
            _broker?.Dispose();
        }
    }
}
