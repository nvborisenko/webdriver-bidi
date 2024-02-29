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
        //private readonly List<Task> _events = new();

        private int _currentCommandId;

        private Task _commandQueueTask;
        private Task _reveivingMessageTask;
        private Task _eventEmitterTask;

        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public Broker(Transport transpot)
        {
            _transport = transpot;

            _reveivingMessageTask = Task.Run(async () => await _transport.ReceiveMessageAsync(default));
            _commandQueueTask = Task.Run(async () => await ProcessMessageAsync());
            _eventEmitterTask = Task.Run(async () => await ProcessEvents());

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new Int32JsonConverter(), new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };
        }

        private async Task ProcessMessageAsync()
        {
            foreach (var message in _transport.Messages.GetConsumingEnumerable())
            {
                Debug.WriteLine($"Processing message: {message}");

                Result<object>? result = JsonSerializer.Deserialize<Result<object>>(message, _jsonSerializerOptions);

                if (result?.Type == "success")
                {
                    _pendingCommands[result.Id].SetResult(result.ResultData);

                    _pendingCommands.TryRemove(result.Id, out _);
                }
                else if (result?.Type == "event")
                {
                    _pendingEvents.Add(result);
                }
                else if (result?.Type == "error")
                {
                    _pendingCommands[result.Id].SetException(new Exception($"{result.Error}: {result.Message}"));

                    _pendingCommands.TryRemove(result.Id, out _);
                }
                else
                {
                    throw new Exception("Unknown type");
                }
                Debug.WriteLine($"Processed message successfully");
            }
        }

        private async Task ProcessEvents()
        {
            foreach (var result in _pendingEvents.GetConsumingEnumerable())
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
            }, true);

            _pendingCommands[command.Id] = tcs;

            await _transport.SendAsync(json, cancellationToken);

            var result = await tcs.Task;

            return ((JsonElement)result).Deserialize<TResult>(_jsonSerializerOptions)!;
        }

        public void RegisterEventHandler<TEventArgs>(string name, AsyncEventHandler<TEventArgs> eventHandler)
            where TEventArgs : EventArgs
        {
            if (_eventHandlers.TryGetValue(name, out var handlers))
            {
                handlers.Add(new BiDiEventHandler<TEventArgs>(eventHandler));
            }
            else
            {
                _eventHandlers[name] = new List<BiDiEventHandler> { new BiDiEventHandler<TEventArgs>(eventHandler) };
            }
        }

        public void Dispose()
        {
            Task.WaitAll([_eventEmitterTask]);
        }
    }
}
