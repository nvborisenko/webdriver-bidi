using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi
{
    internal abstract class BiDiEventHandler
    {
        public Type EventArgsType { get; set; }

        public abstract Task Invoke(object args);
    }

    internal class BiDiEventHandler<TEventArgs> : BiDiEventHandler where TEventArgs : EventArgs
    {
        public BiDiEventHandler(AsyncEventHandler<TEventArgs> handler)
        {
            Action = handler;

            EventArgsType = typeof(TEventArgs);
        }

        public AsyncEventHandler<TEventArgs> Action { get; }

        public override Task Invoke(object arg)
        {
            return Action((TEventArgs)arg);
            //Action.Invoke((TEventArgs)arg).GetAwaiter().GetResult();
            //Action.DynamicInvoke(arg);
        }
    }
}
