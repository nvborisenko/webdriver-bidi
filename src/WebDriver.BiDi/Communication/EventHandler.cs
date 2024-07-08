using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System;
using System.Threading.Tasks;

namespace OpenQA.Selenium.BiDi.Communication;

internal abstract class EventHandler(string eventName, Type eventArgsType, BrowsingContext? context)
{
    public string EventName { get; } = eventName;
    public Type EventArgsType { get; set; } = eventArgsType;
    public BrowsingContext? Context { get; } = context;

    public abstract ValueTask InvokeAsync(object args);
}

internal class AsyncEventHandler<TEventArgs>(string eventName, Func<TEventArgs, Task> func, BrowsingContext? context)
    : EventHandler(eventName, typeof(TEventArgs), context) where TEventArgs : EventArgs
{
    private readonly Func<TEventArgs, Task> _func = func;

    public override async ValueTask InvokeAsync(object args)
    {
        await _func((TEventArgs)args).ConfigureAwait(false);
    }
}

internal class SyncEventHandler<TEventArgs>(string eventName, Action<TEventArgs> action, BrowsingContext? context)
    : EventHandler(eventName, typeof(TEventArgs), context) where TEventArgs : EventArgs
{
    private readonly Action<TEventArgs> _action = action;

    public override ValueTask InvokeAsync(object args)
    {
        _action((TEventArgs)args);

        return default;
    }
}