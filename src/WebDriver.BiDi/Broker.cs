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
    public class Broker
    {
        private readonly Transport _transport;

        private readonly ConcurrentDictionary<long, TaskCompletionSource<object>> _pendingCommands = new();

        private readonly ConcurrentDictionary<string, List<BiDiEventHandler>> _eventHandlers = new();

        private long _currentCommandId;

        private readonly BlockingCollection<string> _commandQueue = new();

        private Task _commandQueueTask;
        private Task _eventReveivingTask;

        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public Broker(Transport transpot)
        {
            _transport = transpot;

            _transport.MessageReceived += Transport_MessageReceived;

            _eventReveivingTask = Task.Run(async () => await _transport.ReceiveAsync(default));
            _commandQueueTask = Task.Run(ProcessMessage);

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        }

        private void ProcessMessage()
        {
            try
            {
                foreach (var message in _commandQueue.GetConsumingEnumerable())
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
                        if (_eventHandlers.TryGetValue(result.Method, out var eventHandlers))
                        {
                            if (eventHandlers is not null)
                            {
                                foreach (var handler in eventHandlers)
                                {
                                    var args = JsonSerializer.Deserialize((JsonElement)result.Params, handler.EventArgsType, _jsonSerializerOptions);

                                    handler.Invoke(args);
                                }
                            }
                        }
                    }
                    else if (result?.Type == "error")
                    {
                        _pendingCommands[result.Id].SetException(new Exception($"{result.Error}: {result.ErrorMessage}"));

                        _pendingCommands.TryRemove(result.Id, out _);
                    }
                    else
                    {
                        throw new Exception("Unknown type");
                    }
                    Debug.WriteLine($"Processed message successfully");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void Transport_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            _commandQueue.Add(e.Message);

            Debug.WriteLine($"Added to queue: {e.Message}");
        }

        public async Task<TResult> ExecuteCommandAsync<TCommand, TResult>(TCommand command)
            where TCommand : Command
        {
            command.Id = Interlocked.Increment(ref _currentCommandId);

            var json = JsonSerializer.Serialize(command, _jsonSerializerOptions);

            var cts = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);

            _pendingCommands[command.Id] = cts;

            await _transport.SendAsync(json, default);

            var result = await cts.Task;

            return ((JsonElement)result).Deserialize<TResult>(_jsonSerializerOptions);
        }

        public void RegisterEventHandler<TEventArgs>(string name, Action<TEventArgs> eventHandler)
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
    }
}
