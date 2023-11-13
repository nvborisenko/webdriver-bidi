using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi
{
    public class Broker
    {
        private readonly Transport _transport;

        private readonly ConcurrentDictionary<long, TaskCompletionSource<object>> _commands = new ConcurrentDictionary<long, TaskCompletionSource<object>>();

        private long _currentCommandId;

        private readonly BlockingCollection<string> _commandQueue = new BlockingCollection<string>();

        private Task _commandQueueTask;
        private Task _eventReveivingTask;

        public Broker(Transport transpot)
        {
            _transport = transpot;

            _transport.MessageReceived += Transport_MessageReceived;

            _eventReveivingTask = Task.Run(async () => await _transport.ReceiveAsync(default));
            _commandQueueTask = Task.Run(ProcessMessage);
        }

        private void ProcessMessage()
        {
            foreach (var message in _commandQueue.GetConsumingEnumerable())
            {
                Result<object>? result = JsonConvert.DeserializeObject<Result<object>>(message);

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
            
            var json = JsonConvert.SerializeObject(command);

            var cts = new TaskCompletionSource<object>();

            _commands[command.Id] = cts;

            await _transport.SendAsync(json, default);

            return ((JObject)await cts.Task).ToObject<TResult>();
        }
    }
}
