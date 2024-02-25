using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.BiDi.Session;
using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi
{
    public sealed class BiDiSession
    {
        private readonly Broker _broker;

        private readonly Lazy<Browser.BrowserModule> _browserModule;
        private readonly Lazy<Network.NetworkModule> _networkModule;

        internal BiDiSession(Broker broker)
        {
            _broker = broker;

            _browserModule = new Lazy<Browser.BrowserModule>(() => new Browser.BrowserModule(_broker));
            _networkModule = new Lazy<Network.NetworkModule>(() => new Network.NetworkModule(this, _broker));
        }

        public Browser.BrowserModule Browser => _browserModule.Value;

        public Network.NetworkModule Network => _networkModule.Value;

        public Task<StatusResult> StatusAsync()
        {
            return _broker.ExecuteCommandAsync<StatusCommand, StatusResult>(new StatusCommand());
        }

        public async Task<BrowsingContextModule> CreateBrowsingContextAsync()
        {
            var context = await _broker.ExecuteCommandAsync<CreateCommand, CreateResult>(new CreateCommand());

            return new BrowsingContextModule(context.Context, this, _broker);
        }

        public async Task<EmptyResult> SubscribeAsync(params string[] events)
        {
            return await _broker.ExecuteCommandAsync<SubscribeCommand, EmptyResult>(new SubscribeCommand() { Params = new SubscriptionCommandParameters { Events = events } });
        }

        public static async Task<BiDiSession> ConnectAsync(string url)
        {
            var transport = new Transport(new Uri(url));

            await transport.ConnectAsync(default).ConfigureAwait(false);

            var broker = new Broker(transport);

            return new BiDiSession(broker);
        }
    }
}
