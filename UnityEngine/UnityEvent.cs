using System.Collections.Generic;

namespace UnityEngine.Events
{
    public delegate void UnityAction();
    public delegate void UnityAction<T>(T arg0);
    public delegate void UnityAction<T0, T1>(T0 arg0, T1 arg1);

    public class UnityEvent : UnityEventBase
    {
        List<UnityAction> handlers = new List<UnityAction>();

        public void AddListener(UnityAction handler)
        {
            handlers.Add(handler);
        }
        public void RemoveListener(UnityAction handler)
        {
            handlers.Remove(handler);
        }
        public override void RemoveAllListeners()
        {
            handlers.Clear();
        }

        public void Invoke()
        {
            foreach (UnityAction handler in handlers)
            {
                handler.Invoke();
            }
        }
    }

    public class UnityEvent<T> : UnityEventBase
    {
        List<UnityAction<T>> handlers = new List<UnityAction<T>>();

        public void AddListener(UnityAction<T> handler)
        {
            handlers.Add(handler);
        }
        public void RemoveListener(UnityAction<T> handler)
        {
            handlers.Remove(handler);
        }
        public override void RemoveAllListeners()
        {
            handlers.Clear();
        }

        public void Invoke(T arg1)
        {
            foreach (UnityAction<T> handler in handlers)
            {
                handler.Invoke(arg1);
            }
        }
    }

    public class UnityEvent<T0, T1> : UnityEventBase
    {
        List<UnityAction<T0, T1>> handlers = new List<UnityAction<T0, T1>>();

        public void AddListener(UnityAction<T0, T1> handler)
        {
            handlers.Add(handler);
        }
        public void RemoveListener(UnityAction<T0, T1> handler)
        {
            handlers.Remove(handler);
        }
        public override void RemoveAllListeners()
        {
            handlers.Clear();
        }

        public void Invoke(T0 arg1, T1 arg2)
        {
            foreach (UnityAction<T0, T1> handler in handlers)
            {
                handler.Invoke(arg1, arg2);
            }
        }
    }
}
