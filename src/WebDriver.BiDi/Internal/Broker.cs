using OpenQA.Selenium.BiDi.Internal.Json;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Internal;

internal class Broker : IAsyncDisposable
{
    private readonly Session _session;
    private readonly Transport _transport;

    private readonly ConcurrentDictionary<int?, TaskCompletionSource<object>> _pendingCommands = new();
    private readonly BlockingCollection<MessageEvent> _pendingEvents = [];

    private CancellationTokenSource _receiveMessagesCancellationTokenSource;

    private readonly ConcurrentDictionary<string, List<EventHandler>> _eventHandlers = new();

    private int _currentCommandId;

    private static readonly TaskFactory _myTaskFactory = new(CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskContinuationOptions.None, TaskScheduler.Default);

    private readonly Task? _commandQueueTask;
    private Task? _receivingMessageTask;
    private Task? _eventEmitterTask;

    private readonly SourceGenerationContext _jsonSourceGenerationContext;

    public Broker(Session session, Transport transport)
    {
        _session = session;
        _transport = transport;

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                new BrowsingContextConverter(_session),
                new BrowserUserContextConverter(session),
                new NavigationConverter(),
                new InterceptConverter(_session),
                new RequestConverter(_session),
                new ChannelConverter(_session),
                new HandleConverter(_session),
                new InternalIdConverter(_session),
                new PreloadScriptConverter(_session),
                new RealmConverter(_session),
                new RealmTypeConverter(),
                new DateTimeConverter(),
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            },
            AllowOutOfOrderMetadataProperties = true
        };

        _jsonSourceGenerationContext = new SourceGenerationContext(jsonSerializerOptions);
    }

    public async Task ConnectAsync(CancellationToken cancellationToken)
    {
        await _transport.ConnectAsync(cancellationToken).ConfigureAwait(false);

        _receiveMessagesCancellationTokenSource = new CancellationTokenSource();
        _receivingMessageTask = _myTaskFactory.StartNew(async () => await ReceiveMessagesAsync(_receiveMessagesCancellationTokenSource.Token), TaskCreationOptions.LongRunning).Unwrap();
        _eventEmitterTask = _myTaskFactory.StartNew(async () => await ProcessEventsAwaiterAsync(), TaskCreationOptions.LongRunning).Unwrap();
    }

    private async Task ReceiveMessagesAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var message = await _transport.ReceiveAsJsonAsync<Message>(_jsonSourceGenerationContext, cancellationToken);

                if (message is MessageSuccess messageSuccess)
                {
                    _pendingCommands[messageSuccess.Id].SetResult(messageSuccess.Result);

                    _pendingCommands.TryRemove(messageSuccess.Id, out _);
                }
                else if (message is MessageEvent messageEvent)
                {
                    _pendingEvents.Add(messageEvent);
                }
                else if (message is MessageError mesageError)
                {
                    _pendingCommands[mesageError.Id].SetException(new BiDiException($"{mesageError.Error}: {mesageError.Message}"));

                    _pendingCommands.TryRemove(mesageError.Id, out _);
                }
                else
                {
                    throw new Exception("Unknown type");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }

    private async Task ProcessEventsAwaiterAsync()
    {
        foreach (var result in _pendingEvents.GetConsumingEnumerable())
        {
            try
            {
                if (_eventHandlers.TryGetValue(result.Method!, out var eventHandlers))
                {
                    if (eventHandlers is not null)
                    {
                        foreach (var handler in eventHandlers)
                        {
                            var args = (EventArgs)((JsonElement)result.Params!).Deserialize(handler.EventArgsType, _jsonSourceGenerationContext)!;

                            args.Session = _session;

                            await handler.InvokeAsync(args).ConfigureAwait(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unhandled error processing BiDi event: {ex}");
                Console.WriteLine($"Unhandled error processing BiDi event: {ex}");
            }
        }
    }

    public async Task<TResult> ExecuteCommandAsync<TResult>(Command command, CancellationToken cancellationToken = default)
    {
        var result = await ExecuteCommandCoreAsync(command, cancellationToken).ConfigureAwait(false);

        return (TResult)((JsonElement)result).Deserialize(typeof(TResult), _jsonSourceGenerationContext)!;
    }

    public async Task ExecuteCommandAsync(Command command, CancellationToken cancellationToken = default)
    {
        await ExecuteCommandCoreAsync(command, cancellationToken).ConfigureAwait(false);
    }

    private async Task<object> ExecuteCommandCoreAsync(Command command, CancellationToken cancellationToken = default)
    {
        command.Id = Interlocked.Increment(ref _currentCommandId);

        var tcs = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);

        using var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(30));
        cts.Token.Register(() =>
        {
            tcs.TrySetCanceled(cancellationToken);
        });

        _pendingCommands[command.Id] = tcs;

        await _transport.SendAsJsonAsync(command, _jsonSourceGenerationContext, cts.Token).ConfigureAwait(false);

        return await tcs.Task.ConfigureAwait(false);
    }

    public async Task<Subscription> SubscribeAsync<TEventArgs>(string eventName, Action<TEventArgs> action, BrowsingContext? context = default)
        where TEventArgs : EventArgs
    {
        var handlers = _eventHandlers.GetOrAdd(eventName, (a) => []);

        var @params = new Modules.Session.SubscribeCommand.Parameters([eventName]);

        if (context is not null)
        {
            @params.Contexts = [context];
        }

        await _session.SessionModule.SubscribeAsync(@params);

        var eventHandler = new SyncEventHandler<TEventArgs>(eventName, action, context);

        handlers.Add(eventHandler);

        return new Subscription(this, eventHandler);
    }

    public async Task<Subscription> SubscribeAsync<TEventArgs>(string eventName, Func<TEventArgs, Task> func, BrowsingContext? context = default)
        where TEventArgs : EventArgs
    {
        var handlers = _eventHandlers.GetOrAdd(eventName, (a) => []);

        var @params = new Modules.Session.SubscribeCommand.Parameters([eventName]);

        if (context is not null)
        {
            @params.Contexts = [context];
        }

        await _session.SessionModule.SubscribeAsync(@params);

        var eventHandler = new AsyncEventHandler<TEventArgs>(eventName, func, context);

        handlers.Add(eventHandler);

        return new Subscription(this, eventHandler);
    }

    public async Task UnsubscribeAsync(EventHandler eventHandler)
    {
        var eventHandlers = _eventHandlers[eventHandler.EventName];

        eventHandlers.Remove(eventHandler);

        if (eventHandler.Context is not null)
        {
            if (!eventHandlers.Any(h => eventHandler.Context.Equals(h.Context)) && !eventHandlers.Any(h => h.Context is null))
            {
                await _session.SessionModule.UnsubscribeAsync(new([eventHandler.EventName]) { Contexts = [eventHandler.Context] });
            }
        }
        else
        {
            if (!eventHandlers.Any(h => h.Context is not null) && !eventHandlers.Any(h => h.Context is null))
            {
                await _session.SessionModule.UnsubscribeAsync(new([eventHandler.EventName]));
            }
        }
    }

    public async ValueTask DisposeAsync()
    {
        _pendingEvents.CompleteAdding();

        _receiveMessagesCancellationTokenSource.Cancel();

        if (_eventEmitterTask is not null)
        {
            await _eventEmitterTask.ConfigureAwait(false);
        }
    }
}
