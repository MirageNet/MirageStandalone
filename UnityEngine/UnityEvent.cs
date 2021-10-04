using System;
using System.Collections.Generic;

namespace UnityEngine
{
    public class UnityEvent : UnityEventBase
    {
        List<Action> handlers = new List<Action>();

        public void AddListener(Action handler)
        {
            handlers.Add(handler);
        }
        public void RemoveListener(Action handler)
        {
            handlers.Remove(handler);
        }
        public override void RemoveAllListeners()
        {
            handlers.Clear();
        }

        public void Invoke()
        {
            foreach (Action handler in handlers)
            {
                handler.Invoke();
            }
        }
    }

    public class UnityEvent<T> : UnityEventBase
    {
        List<Action<T>> handlers = new List<Action<T>>();

        public void AddListener(Action<T> handler)
        {
            handlers.Add(handler);
        }
        public void RemoveListener(Action<T> handler)
        {
            handlers.Remove(handler);
        }
        public override void RemoveAllListeners()
        {
            handlers.Clear();
        }

        public void Invoke(T arg1)
        {
            foreach (Action<T> handler in handlers)
            {
                handler.Invoke(arg1);
            }
        }
    }

    public class UnityEvent<T0, T1> : UnityEventBase
    {
        List<Action<T0, T1>> handlers = new List<Action<T0, T1>>();

        public void AddListener(Action<T0, T1> handler)
        {
            handlers.Add(handler);
        }
        public void RemoveListener(Action<T0, T1> handler)
        {
            handlers.Remove(handler);
        }
        public override void RemoveAllListeners()
        {
            handlers.Clear();
        }

        public void Invoke(T0 arg1, T1 arg2)
        {
            foreach (Action<T0, T1> handler in handlers)
            {
                handler.Invoke(arg1, arg2);
            }
        }
    }
}
