using System;
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
        private readonly Action<TEventArgs> _action;
        private readonly Func<TEventArgs, Task> _func;

        public BiDiEventHandler(Action<TEventArgs> handler)
            : base(typeof(TEventArgs))
        {
            _action = handler;
        }

        public BiDiEventHandler(Func<TEventArgs, Task> handler)
            : base(typeof(TEventArgs))
        {
            _func = handler;
        }

        public override async Task InvokeAsync(object args)
        {
            if (_action is not null)
            {
                _action((TEventArgs)args);
            }
            else
            {
                await _func((TEventArgs)args).ConfigureAwait(false);
            }
        }
    }
}
