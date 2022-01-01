using System;
using System.Collections.Generic;

namespace UnityEngine
{
    public class GameObject : Object
    {
        public Transform transform => throw new NotSupportedException();

        readonly List<Component> components = new List<Component>();

        public T GetComponent<T>() where T : class
        {
            _ = TryGetComponent(out T component);
            return component;
        }

        public bool TryGetComponent<T>(out T component) where T : class
        {
            foreach (Component comp in components)
            {
                if (comp is T tComp)
                {
                    component = tComp;
                    return true;
                }
            }
            component = null;
            return false;
        }
    }
}
