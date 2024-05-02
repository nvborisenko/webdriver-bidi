using System;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi
{
    public class Session : IDisposable
    {
        private readonly Transport _transport;
        private readonly Broker _broker;

        private readonly Lazy<Modules.Browser.BrowserModule> _browserModule;
        private readonly Lazy<Modules.Network.NetworkModule> _networkModule;

        internal Session(string uri)
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

        public async Task OnBrowserContextCreatedAsync(Action<Modules.BrowsingContext.InfoEventArgs> callback)
        {
            var syncContext = SynchronizationContext.Current;

            await SubscribeAsync("browsingContext.contextCreated").ConfigureAwait(false);

            _broker.RegisterEventHandler("browsingContext.contextCreated", new BiDiEventHandler<Modules.BrowsingContext.InfoEventArgs>(syncContext, callback));
        }

        public async Task OnBrowserContextCreatedAsync(Func<Modules.BrowsingContext.InfoEventArgs, Task> callback)
        {
            var syncContext = SynchronizationContext.Current;

            await SubscribeAsync("browsingContext.contextCreated").ConfigureAwait(false);

            _broker.RegisterEventHandler("browsingContext.contextCreated", new BiDiEventHandler<Modules.BrowsingContext.InfoEventArgs>(syncContext, callback));
        }

        public static async Task<Session> ConnectAsync(string url)
        {
            var session = new Session(url);

            await session.ConnectAsync().ConfigureAwait(false);

            return session;
        }

        private async Task ConnectAsync()
        {
            await _broker.ConnectAsync(default).ConfigureAwait(false);
        }

        internal async Task SubscribeAsync(params string[] events)
        {
            await _broker.ExecuteCommandAsync(new Modules.Session.SubscribeCommand() { Params = new Modules.Session.SubscriptionCommandParameters { Events = events } }).ConfigureAwait(false);
        }

        public void Dispose()
        {
            _transport?.Dispose();
            _broker?.Dispose();
        }

        public async Task DisposeAsync()
        {
            _transport?.Dispose();

            if (_broker is not null)
            {
                await _broker.DisposeAsync().ConfigureAwait(false);
            }
        }
    }
}
