using System;
using System.Collections;
using System.Collections.Generic;

public class EventBus<TEventBase>
{
    private readonly Dictionary<Type, IList> _listeners = new();

    public List<T> GetListeners<T>() where T : TEventBase
    {
        var type = typeof(T);
        if (!_listeners.TryGetValue(type, out var list))
        {
            list = new List<T>();
            _listeners[type] = list;
        }
        return (List<T>)list;
    }

    public void Add<T>(T action) where T : TEventBase
    {
        GetListeners<T>().Add(action);
    }

    public void Remove<T>(T action) where T : TEventBase
    {
        GetListeners<T>().Remove(action);
    }

    public void Invoke<T>(Action<T> invoker) where T : TEventBase
    {
        var listeners = GetListeners<T>();
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            invoker(listeners[i]);
        }
    }

    public void Clear()
    {
        _listeners.Clear();
    }
}
