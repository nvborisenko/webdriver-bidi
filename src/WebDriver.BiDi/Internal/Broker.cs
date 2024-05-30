using OpenQA.Selenium.BiDi.Internal.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Internal;

internal class Broker
#if NET8_0_OR_GREATER
    : IAsyncDisposable
#endif
{
    private readonly Session _session;
    private readonly Transport _transport;

    private readonly ConcurrentDictionary<int?, TaskCompletionSource<object>> _pendingCommands = new();
    private readonly BlockingCollection<MessageEvent> _pendingEvents = [];

    private CancellationTokenSource _receiveMessagesCancellationTokenSource;

    private readonly ConcurrentDictionary<string, List<BiDiEventHandler>> _eventHandlers = new();

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
                new JsonBrowsingContextConverter(_session),
                new BrowserUserContextConverter(session),
                new JsonNavigationConverter(),
                new JsonInterceptConverter(_session),
                new JsonRequestConverter(_session),
                new JsonDateTimeConverter(),
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

    public void RegisterEventHandler<TEventArgs>(string name, BiDiEventHandler<TEventArgs> eventHandler)
        where TEventArgs : EventArgs
    {
        var handlers = _eventHandlers.GetOrAdd(name, (a) => []);

        handlers.Add(eventHandler);
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
