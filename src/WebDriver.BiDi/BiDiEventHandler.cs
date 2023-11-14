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
        public BiDiEventHandler(Delegate action)
        {
            Action = action;

            EventArgsType = typeof(TEventArgs);
        }

        public Delegate Action { get; }

        public override void Invoke(object arg)
        {
            Action.DynamicInvoke(null, arg);
        }
    }
}
