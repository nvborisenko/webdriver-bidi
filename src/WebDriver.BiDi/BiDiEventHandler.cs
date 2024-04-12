using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi
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
        public BiDiEventHandler(AsyncEventHandler<TEventArgs> handler)
            : base(typeof(TEventArgs))
        {
            Action = handler;
        }

        public AsyncEventHandler<TEventArgs> Action { get; }

        public override async Task InvokeAsync(object args)
        {
            //var result = Action((TEventArgs)arg).ConfigureAwait(false);
            //return AsyncHelper.RunSync(() => Action((TEventArgs)arg));
            //await result;




            List<Exception> exceptions = null;
            Delegate[] listenerDelegates = Action.GetInvocationList();
            for (int index = 0; index < listenerDelegates.Length; ++index)
            {
                var listenerDelegate = (AsyncEventHandler<TEventArgs>)listenerDelegates[index];
                try
                {
                    await listenerDelegate((TEventArgs)args).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    if (exceptions == null)
                        exceptions = new List<Exception>(2);
                    exceptions.Add(ex);
                }
            }

            // Throw collected exceptions, if any
            if (exceptions != null)
                throw new AggregateException(exceptions);
        }
    }
}
