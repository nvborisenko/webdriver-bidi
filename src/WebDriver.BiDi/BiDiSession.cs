using OpenQA.Selenium.BiDi.Browser;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.BiDi.Session;
using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi
{
    public sealed class BiDiSession
    {
        private readonly Broker _broker;

        public BiDiSession(Broker broker)
        {
            _broker = broker;

            Browser = new BrowserModule(_broker);
            Network = new Network.NetworkModule(this, _broker);
        }

        public Task<StatusResult> StatusAsync()
        {
            return _broker.ExecuteCommandAsync<StatusCommand, StatusResult>(new StatusCommand());
        }

        public async Task<BrowsingContextModule> CreateBrowsingContextAsync()
        {
            var context = await _broker.ExecuteCommandAsync<CreateCommand, CreateResult>(new CreateCommand());

            return new BrowsingContextModule(context.ContextId, _broker);
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

            return await Task.FromResult(new BiDiSession(broker));
        }

        public BrowserModule Browser { get; }

        public Network.NetworkModule Network { get; }
    }
}
