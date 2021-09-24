using System;

namespace UnityEngine
{
    public class UnityEvent : UnityEventBase
    {

    }

    public class UnityEvent<T> : UnityEventBase
    {
        public UnityEvent()
        {

        }

        public void AddListener(Action<T> handler)
        {
            // TODO: AddListener
        }

        public void RemoveListener(Action<T> handler)
        {
            // TODO: RemoveListener
        }

        public void Invoke(T arg)
        {
            // TODO: Invoke
        }
    }

    public class UnityEvent<T0, T1> : UnityEventBase
    {
        public UnityEvent()
        {

        }

        public void AddListener(Action<T0, T1> handler)
        {
            // TODO: AddListener
        }

        public void RemoveListener(Action<T0, T1> handler)
        {
            // TODO: RemoveListener
        }

        public void Invoke(T0 arg1, T1 arg2)
        {
            // TODO: Invoke
        }
    }
}
