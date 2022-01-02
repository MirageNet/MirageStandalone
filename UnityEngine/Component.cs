using System;
using System.Collections.Generic;

namespace UnityEngine
{
    public class Component : Object
    {
        public GameObject gameObject => throw new NotSupportedException();

        public Transform transform => gameObject.transform;

        public T GetComponent<T>() where T : class => gameObject.GetComponent<T>();
        public T[] GetComponents<T>() where T : class => gameObject.GetComponents<T>();

        public T[] GetComponentsInParent<T>() where T : class => gameObject.GetComponentsInParent<T>();
        public void GetComponentsInParent<T>(List<T> cache) where T : class => gameObject.GetComponentsInParent<T>(cache);
        public void GetComponentsInParent<T>(bool includeNotActive, List<T> cache) where T : class => gameObject.GetComponentsInParent<T>(includeNotActive, cache);

        public T[] GetComponentsInChildren<T>() where T : class => gameObject.GetComponentsInChildren<T>();
        public void GetComponentsInChildren<T>(List<T> cache) where T : class => gameObject.GetComponentsInChildren<T>(cache);
        public void GetComponentsInChildren<T>(bool includeNotActive, List<T> cache) where T : class => gameObject.GetComponentsInChildren<T>(includeNotActive, cache);


        public bool TryGetComponent<T>(out T component) where T : class => gameObject.TryGetComponent(out component);
    }
}
