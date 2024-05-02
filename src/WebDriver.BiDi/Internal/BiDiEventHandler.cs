using System;
using System.Threading;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Internal
{
    internal abstract class BiDiEventHandler
    {
        protected BiDiEventHandler(Type eventArgsType)
        {
            EventArgsType = eventArgsType;
        }

        public Type EventArgsType { get; set; }

        public abstract Task InvokeAsync(object args);
    }

    internal class BiDiEventHandler<TEventArgs> : BiDiEventHandler where TEventArgs : EventArgs
    {
        private readonly SynchronizationContext _synchronizationContext;

        private readonly Action<TEventArgs> _action;
        private readonly Func<TEventArgs, Task> _func;

        public BiDiEventHandler(SynchronizationContext synchronizationContext, Action<TEventArgs> handler)
            : base(typeof(TEventArgs))
        {
            _synchronizationContext = synchronizationContext;
            _action = handler;
        }

        public BiDiEventHandler(SynchronizationContext synchronizationContext, Func<TEventArgs, Task> handler)
            : base(typeof(TEventArgs))
        {
            _synchronizationContext = synchronizationContext;
            _func = handler;
        }

        public override async Task InvokeAsync(object args)
        {
            //SynchronizationContext.SetSynchronizationContext(_synchronizationContext);

            if (_action is not null)
            {
                if (_synchronizationContext is not null)
                {
                    _synchronizationContext.Post(_ => _action((TEventArgs)args), null);
                }
                else
                {
                    _action((TEventArgs)args);
                }
                //_action((TEventArgs)args);
            }
            else
            {
                if (_synchronizationContext is not null)
                {
                    var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);

                    _synchronizationContext.Post(async _ =>
                    {
                        try
                        {
                            await _func((TEventArgs)args).ConfigureAwait(false);
                            tcs.SetResult(null);
                        }
                        catch (Exception e)
                        {
                            tcs.SetException(e);
                        }
                    }, null);

                    //await tcs.Task;
                    await tcs.Task.ConfigureAwait(false);

                }
                else
                {
                    await _func((TEventArgs)args).ConfigureAwait(false);
                }
                //await _func((TEventArgs)args).ConfigureAwait(false);
            }
        }
    }
}
