using OpenQA.Selenium.BiDi.Modules.Browser;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using OpenQA.Selenium.BiDi.Modules.Session;
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
        }

        public async Task<StatusResult> StatusAsync()
        {
            return await _broker.ExecuteCommand<StatusCommand, StatusResult>(new StatusCommand());
        }

        public async Task<BrowsingContextModule> CreateBrowsingContextAsync()
        {
            var context = await _broker.ExecuteCommand<CreateCommand, CreateResult>(new CreateCommand());

            return new BrowsingContextModule(context.ContextId, _broker);
        }

        public static async Task<BiDiSession> ConnectAsync(string url)
        {
            var transport = new Transport(new Uri(url));

            await transport.ConnectAsync(default).ConfigureAwait(false);

            var broker = new Broker(transport);

            return await Task.FromResult(new BiDiSession(broker));
        }

        public BrowserModule Browser { get; }
    }
}
