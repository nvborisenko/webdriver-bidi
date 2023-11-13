using System;

namespace OpenQA.Selenium.BiDi
{
    internal abstract class BiDiEventHandler
    {
        public Type EventArgsType { get; set; }

        public abstract void Invoke(object args);
    }

    internal class BiDiEventHandler<TEventArgs> : BiDiEventHandler where TEventArgs : EventArgs
    {
        public BiDiEventHandler(Action<TEventArgs> action)
        {
            Action = action;

            EventArgsType = typeof(TEventArgs);
        }

        public Action<TEventArgs> Action { get; }

        public override void Invoke(object args)
        {
            Action((TEventArgs)args);
        }
    }
}
