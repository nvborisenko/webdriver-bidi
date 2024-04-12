using System;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi
{
    public class BiDiSession : IDisposable
    {
        private readonly Transport _transport;
        private readonly Broker _broker;

        private readonly Lazy<Modules.Browser.BrowserModule> _browserModule;
        private readonly Lazy<Modules.Network.NetworkModule> _networkModule;

        internal BiDiSession(string uri)
        {
            _transport = new Transport(new Uri(uri));
            _broker = new Broker(this, _transport);

            _browserModule = new Lazy<Modules.Browser.BrowserModule>(() => new Modules.Browser.BrowserModule(_broker));
            _networkModule = new Lazy<Modules.Network.NetworkModule>(() => new Modules.Network.NetworkModule(this, _broker));
        }

        public Modules.Browser.BrowserModule Browser => _browserModule.Value;

        public Modules.Network.NetworkModule Network => _networkModule.Value;

        public async Task<Modules.Session.StatusResult> StatusAsync()
        {
            return await _broker.ExecuteCommandAsync<Modules.Session.StatusCommand, Modules.Session.StatusResult>(new Modules.Session.StatusCommand()).ConfigureAwait(false);
        }

        public async Task<Modules.BrowsingContext.BrowsingContextModule> CreateBrowsingContextAsync()
        {
            var context = await _broker.ExecuteCommandAsync<Modules.BrowsingContext.CreateCommand, Modules.BrowsingContext.CreateResult>(new Modules.BrowsingContext.CreateCommand()).ConfigureAwait(false);

            return new Modules.BrowsingContext.BrowsingContextModule(context.Context, this, _broker);
        }

        public async Task SubscribeAsync(params string[] events)
        {
            await _broker.ExecuteCommandAsync(new Modules.Session.SubscribeCommand() { Params = new Modules.Session.SubscriptionCommandParameters { Events = events } }).ConfigureAwait(false);
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
