using System;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi
{
    public class Broker
    {
        private readonly Transport _transport;

        private readonly ConcurrentDictionary<long, TaskCompletionSource<object>> _commands = new();

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
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        }

        private void ProcessMessage()
        {
            foreach (var message in _commandQueue.GetConsumingEnumerable())
            {
                Result<object>? result = JsonSerializer.Deserialize<Result<object>>(message, _jsonSerializerOptions);

                if (result?.Type == "success")
                {
                    _commands[result.Id].SetResult(result.ResultData);
                }
                else if (result?.Type == "event")
                {

                }
                else if (result?.Type == "error")
                {
                    _commands[result.Id].SetException(new Exception($"{result.Error}: {result.ErrorMessage}"));
                }
                else
                {
                    throw new Exception("Unknown type");
                }
            }
        }

        private void Transport_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            _commandQueue.Add(e.Message);
        }

        public async Task<TResult> ExecuteCommand<TCommand, TResult>(TCommand command)
            where TCommand : Command
        {
            command.Id = Interlocked.Increment(ref _currentCommandId);

            var json = JsonSerializer.Serialize(command, _jsonSerializerOptions);

            var cts = new TaskCompletionSource<object>();

            _commands[command.Id] = cts;

            await _transport.SendAsync(json, default);

            var result = await cts.Task;

            return ((JsonElement)result).Deserialize<TResult>(_jsonSerializerOptions);
        }
    }
}
