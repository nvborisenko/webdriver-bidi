using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.BiDi.Internal;

namespace OpenQA.Selenium.BiDi
{
    public class Session
#if NET8_0_OR_GREATER
        : IAsyncDisposable
#endif
    {
        private readonly Transport _transport;
        private readonly Broker _broker;

        private readonly Lazy<Modules.Session.SessionModule> _sessionModule;
        private readonly Lazy<Modules.BrowsingContext.BrowsingContextModule> _browsingContextModule;
        private readonly Lazy<Modules.Browser.BrowserModule> _browserModule;
        private readonly Lazy<Modules.Network.NetworkModule> _networkModule;
        private readonly Lazy<Modules.Input.InputModule> _inputModule;
        private readonly Lazy<Modules.Script.ScriptModule> _scriptModule;
        private readonly Lazy<Modules.Log.LogModule> _logModule;

        internal Session(string url)
        {
            var uri = new Uri(url);

            _transport = new Transport(new Uri(url));
            _broker = new Broker(this, _transport);

            _sessionModule = new Lazy<Modules.Session.SessionModule>(() => new Modules.Session.SessionModule(this, _broker));
            _browsingContextModule = new Lazy<Modules.BrowsingContext.BrowsingContextModule>(() => new Modules.BrowsingContext.BrowsingContextModule(this, _broker));
            _browserModule = new Lazy<Modules.Browser.BrowserModule>(() => new Modules.Browser.BrowserModule(_broker));
            _networkModule = new Lazy<Modules.Network.NetworkModule>(() => new Modules.Network.NetworkModule(this, _broker));
            _inputModule = new Lazy<Modules.Input.InputModule>(() => new Modules.Input.InputModule(_broker));
            _scriptModule = new Lazy<Modules.Script.ScriptModule>(() => new Modules.Script.ScriptModule(_broker));
            _logModule = new Lazy<Modules.Log.LogModule>(() => new Modules.Log.LogModule(this, _broker));
        }

        public Modules.Session.SessionModule SessionModule => _sessionModule.Value;

        public Modules.BrowsingContext.BrowsingContextModule BrowsingContextModule => _browsingContextModule.Value;

        public Modules.Browser.BrowserModule Browser => _browserModule.Value;

        internal Modules.Network.NetworkModule Network => _networkModule.Value;

        public Modules.Input.InputModule Input => _inputModule.Value;

        public Modules.Script.ScriptModule Script => _scriptModule.Value;

        public Modules.Log.LogModule Log => _logModule.Value;

        public Task<Modules.Session.StatusResult> StatusAsync()
        {
            return SessionModule.StatusAsync();
        }

        public async Task<Modules.BrowsingContext.BrowsingContext> CreateBrowsingContextAsync()
        {
            var createResult = await BrowsingContextModule.CreateAsync(new Modules.BrowsingContext.CreateCommandParameters()).ConfigureAwait(false);

            return createResult.Context;
        }

        public Task<Modules.Network.AddInterceptResult> AddInterceptAsync(Modules.Network.InterceptPhase phase, List<Modules.Network.UrlPattern>? urlPatterns = default)
        {
            var parameters = new Modules.Network.AddInterceptParameters
            {
                Phases = [phase],
                UrlPatterns = urlPatterns
            };

            return AddInterceptAsync(parameters);
        }

        public Task<Modules.Network.AddInterceptResult> AddInterceptAsync(List<Modules.Network.InterceptPhase> phases, List<Modules.Network.UrlPattern>? urlPatterns = default)
        {
            var parameters = new Modules.Network.AddInterceptParameters
            {
                Phases = phases,
                UrlPatterns = urlPatterns
            };

            return AddInterceptAsync(parameters);
        }

        public Task<Modules.Network.AddInterceptResult> AddInterceptAsync(Modules.Network.AddInterceptParameters parameters)
        {
            return Network.AddInterceptAsync(parameters);
        }

        public async Task OnBrowsingContextCreatedAsync(Action<Modules.BrowsingContext.BrowsingContextInfoEventArgs> callback)
        {
            await SubscribeAsync("browsingContext.contextCreated").ConfigureAwait(false);

            _broker.RegisterEventHandler("browsingContext.contextCreated", new BiDiEventHandler<Modules.BrowsingContext.BrowsingContextInfoEventArgs>(callback));
        }

        public async Task OnBrowsingContextCreatedAsync(Func<Modules.BrowsingContext.BrowsingContextInfoEventArgs, Task> callback)
        {
            await SubscribeAsync("browsingContext.contextCreated").ConfigureAwait(false);

            _broker.RegisterEventHandler("browsingContext.contextCreated", new BiDiEventHandler<Modules.BrowsingContext.BrowsingContextInfoEventArgs>(callback));
        }

        public Task OnBeforeRequestSentAsync(Func<Modules.Network.BeforeRequestSentEventArgs, Task> callback)
        {
            return Network.OnBeforeRequestSentAsync(callback);
        }

        public Task OnBeforeRequestSentAsync(Action<Modules.Network.BeforeRequestSentEventArgs> callback)
        {
            return Network.OnBeforeRequestSentAsync(callback);
        }

        public Task OnResponseStartedAsync(Func<Modules.Network.ResponseStartedEventArgs, Task> callback)
        {
            return Network.OnResponseStartedAsync(callback);
        }

        public Task OnResponseStartedAsync(Action<Modules.Network.ResponseStartedEventArgs> callback)
        {
            return Network.OnResponseStartedAsync(callback);
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

        public async Task EndAsync()
        {
            await _broker.ExecuteCommandAsync(new Modules.Session.EndCommand()).ConfigureAwait(false);

            await _broker.DisposeAsync().ConfigureAwait(false);

            _transport?.Dispose();
        }

        internal Task SubscribeAsync(params string[] events)
        {
            return SessionModule.SubscribeAsync(new Modules.Session.SubscriptionCommandParameters { Events = events });
        }

        public async ValueTask DisposeAsync()
        {
            await EndAsync().ConfigureAwait(false);
        }
    }
}
