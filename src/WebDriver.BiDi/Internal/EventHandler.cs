using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Internal;

internal abstract class EventHandler
{
    protected EventHandler(string eventName, Type eventArgsType, BrowsingContext? context)
    {
        EventName = eventName;
        EventArgsType = eventArgsType;
        Context = context;
    }

    public string EventName { get; }
    public Type EventArgsType { get; set; }
    public BrowsingContext? Context { get; }

    public abstract Task InvokeAsync(object args);
}

internal class BiDiEventHandler<TEventArgs> : EventHandler where TEventArgs : EventArgs
{
    private readonly Action<TEventArgs> _action;
    private readonly Func<TEventArgs, Task> _func;

    public BiDiEventHandler(string eventName, Action<TEventArgs> action, BrowsingContext? context)
        : base(eventName, typeof(TEventArgs), context)
    {
        _action = action;
    }

    public BiDiEventHandler(string eventName, Func<TEventArgs, Task> func, BrowsingContext? context)
        : base(eventName, typeof(TEventArgs), context)
    {
        _func = func;
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
