using System;

namespace UnityEngine
{
    public class Component : Object
    {
        public GameObject gameObject => throw new NotSupportedException();

        public Transform transform => gameObject.transform;

        public T GetComponent<T>() where T : class => gameObject.GetComponent<T>();

        public bool TryGetComponent<T>(out T component) where T : class => gameObject.TryGetComponent(out component);
    }
}
