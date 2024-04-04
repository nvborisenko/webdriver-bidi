using OpenQA.Selenium.BiDi.Internal.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi
{
    internal class Broker : IDisposable
    {
        private readonly Transport _transport;

        private readonly ConcurrentDictionary<int, TaskCompletionSource<object>> _pendingCommands = new();
        private readonly BlockingCollection<Result<object>> _pendingEvents = new();

        private readonly ConcurrentDictionary<string, List<BiDiEventHandler>> _eventHandlers = new();

        private int _currentCommandId;

        private readonly Task _commandQueueTask;
        private readonly Task _reveivingMessageTask;
        private readonly Task _eventEmitterTask;

        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public Broker(Transport transpot)
        {
            _transport = transpot;

            _reveivingMessageTask = Task.Run(async () => await _transport.ReceiveMessageAsync(default));
            _commandQueueTask = Task.Run(ProcessMessage);
            _eventEmitterTask = Task.Run(ProcessEvents);

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new Int32JsonConverter(), new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };
        }

        private void ProcessMessage()
        {
            foreach (var message in _transport.Messages.GetConsumingEnumerable())
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
                    _pendingEvents.Add(result);
                }
                else if (result?.Type == "error")
                {
                    _pendingCommands[result.Id].TrySetException(new Exception($"{result.Error}: {result.Message}"));

                    _pendingCommands.TryRemove(result.Id, out _);
                }
                else
                {
                    throw new Exception("Unknown type");
                }

                Debug.WriteLine($"Processed message successfully");
            }
        }

        private void ProcessEvents()
        {
            foreach (var result in _pendingEvents.GetConsumingEnumerable())
            {
                try
                {
                    if (_eventHandlers.TryGetValue(result.Method, out var eventHandlers))
                    {
                        if (eventHandlers is not null)
                        {
                            foreach (var handler in eventHandlers)
                            {
                                var args = JsonSerializer.Deserialize((JsonElement)result.Params, handler.EventArgsType, _jsonSerializerOptions);

                                var res = handler.Invoke(args);

                                if (!res.IsCompleted)
                                {
                                    res.Wait();
                                }
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

            var result = await tcs.Task;

            return ((JsonElement)result).Deserialize<TResult>(_jsonSerializerOptions)!;
        }

        public void RegisterEventHandler<TEventArgs>(string name, AsyncEventHandler<TEventArgs> eventHandler)
            where TEventArgs : EventArgs
        {
            var handlers = _eventHandlers.GetOrAdd(name, (a) => []);

            handlers.Add(new BiDiEventHandler<TEventArgs>(eventHandler));
        }

        public void Dispose()
        {
            _pendingEvents.CompleteAdding();
            try
            {
                Task.WaitAll([_eventEmitterTask]);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unhandled error occured while disposing Broker object: {ex}");
            }
        }
    }
}
