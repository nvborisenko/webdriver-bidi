using OpenQA.Selenium.BiDi.Internal.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Internal
{
    internal class Broker : IDisposable
    {
        private readonly Session _session;
        private readonly Transport _transport;

        private readonly ConcurrentDictionary<int?, TaskCompletionSource<object>> _pendingCommands = new();
        private readonly BlockingCollection<Result<object>> _pendingEvents = new();

        private readonly ConcurrentDictionary<string, List<BiDiEventHandler>> _eventHandlers = new();

        private int _currentCommandId;

        private readonly TaskFactory _myTaskFactory = new TaskFactory(CancellationToken.None,
            TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        private Task? _commandQueueTask;
        private Task? _receivingMessageTask;
        private Task? _eventEmitterTask;

        private readonly JsonSerializerOptions _jsonSerializerOptions;

        private readonly BlockingCollection<string> _channel;

        public Broker(Session session, Transport transport)
        {
            _session = session;
            _transport = transport;
            _channel = new BlockingCollection<string>();

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new Int32JsonConverter(), new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };
        }

        public async Task ConnectAsync(CancellationToken cancellationToken)
        {
            await _transport.ConnectAsync(cancellationToken).ConfigureAwait(false);

            _transport.MessageReceived += (s, args) =>
            {
                _channel.Add(args.Message);
            };

            _receivingMessageTask = _myTaskFactory.StartNew(async () => await _transport.ReceiveMessageAsync(default)).Unwrap();
            _commandQueueTask = _myTaskFactory.StartNew(ProcessMessages);
            _eventEmitterTask = _myTaskFactory.StartNew(async () => await ProcessEventsAwaiterAsync()).Unwrap();
        }

        private void ProcessMessages()
        {
            foreach (var message in _channel.GetConsumingEnumerable())
            {
                Debug.WriteLine($"Processing message: {message}");

                Result<object>? result = JsonSerializer.Deserialize<Result<object>>(message, _jsonSerializerOptions);

                if (result?.Type == "success")
                {
                    _pendingCommands[result.Id].TrySetResult(result.ResultData);

                    _pendingCommands.TryRemove(result.Id, out _);
                }
                else if (result?.Type == "event")
                {
                    ProcessEvent(result);
                }
                else if (result?.Type == "error")
                {
                    _pendingCommands[result.Id].TrySetException(new BiDiException($"{result.Error}: {result.Message}"));

                    _pendingCommands.TryRemove(result.Id, out _);
                }
                else
                {
                    throw new Exception("Unknown type");
                }

                Debug.WriteLine($"Processed message successfully");
            }
        }

        private void ProcessEvent(Result<object> result)
        {
            _pendingEvents.Add(result);
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
                                var args = (EventArgs)((JsonElement)result.Params!).Deserialize(handler.EventArgsType, _jsonSerializerOptions)!;

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

        public async Task<TResult> ExecuteCommandAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command
        {
            command.Id = Interlocked.Increment(ref _currentCommandId);

            var json = JsonSerializer.Serialize(command, _jsonSerializerOptions);

            var tcs = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);

            using var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(30));
            cts.Token.Register(() =>
            {
                tcs.TrySetCanceled(cancellationToken);
            });

            _pendingCommands[command.Id] = tcs;

            await _transport.SendAsync(json, cancellationToken).ConfigureAwait(false);

            var result = await tcs.Task.ConfigureAwait(false);

            return ((JsonElement)result).Deserialize<TResult>(_jsonSerializerOptions)!;
        }

        public async Task ExecuteCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command
        {
            command.Id = Interlocked.Increment(ref _currentCommandId);

            var json = JsonSerializer.Serialize(command, _jsonSerializerOptions);

            var tcs = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);

            using var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(30));
            cts.Token.Register(() =>
            {
                tcs.TrySetCanceled(cancellationToken);
            });

            _pendingCommands[command.Id] = tcs;

            await _transport.SendAsync(json, cancellationToken).ConfigureAwait(false);

            await tcs.Task.ConfigureAwait(false);
        }

        public void RegisterEventHandler<TEventArgs>(string name, BiDiEventHandler<TEventArgs> eventHandler)
            where TEventArgs : EventArgs
        {
            var handlers = _eventHandlers.GetOrAdd(name, (a) => []);

            handlers.Add(eventHandler);
        }

        public void Dispose()
        {
            try
            {
                _pendingEvents.CompleteAdding();
                _channel.CompleteAdding();

                if (_eventEmitterTask is not null)
                {
                    AsyncHelper.RunSync(async () => await _eventEmitterTask);
                    //Task.Factory.StartNew(async () => await _eventEmitterTask).Unwrap().GetAwaiter().GetResult();
                    //Task.Run(async () => await _eventEmitterTask).GetAwaiter().GetResult();
                }
            }
            catch (Exception) { }
        }

        public async Task DisposeAsync()
        {
            _pendingEvents.CompleteAdding();
            _channel.CompleteAdding();

            if (_eventEmitterTask is not null)
            {
                await _eventEmitterTask.ConfigureAwait(false);
            }
        }
    }
}