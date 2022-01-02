using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine
{
    public class GameObject : Object
    {
        [Obsolete("Un-used")]
        public HideFlags hideFlags;
        public bool activeSelf { get; private set; } = true;
        public bool active
        {
            get
            {
                // if not active, then exit false early
                if (!activeSelf)
                    return false;

                // active if no parent, or parent active
                Transform parent = transform.parent;
                return parent == null || parent.gameObject.active;
            }
        }

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

        public T[] GetComponents<T>() where T : class
        {
            return components
                .Select(x => x is T comp ? comp : null)
                .Where(x => x != null)
                .ToArray();
        }
        public void GetComponents<T>(List<T> cache) where T : class
        {
            foreach (Component comp in components)
            {
                if (comp is T tComp)
                {
                    cache.Add(tComp);
                }
            }
        }

        public T[] GetComponentsInParent<T>() where T : class
        {
            var found = new List<T>();
            GetComponentsInParent(false, found);
            return found.ToArray();
        }

        public void GetComponentsInParent<T>(List<T> cache) where T : class => GetComponentsInParent(false, cache);
        public void GetComponentsInParent<T>(bool includeUnActive, List<T> cache) where T : class
        {
            cache.Clear();
            Internal_GetComponentsInParent(includeUnActive, cache);
        }

        private void Internal_GetComponentsInParent<T>(bool includeUnActive, List<T> cache) where T : class
        {
            GetComponents(cache);
            Transform parent = transform.parent;
            while (parent != null)
            {
                if (includeUnActive || parent.gameObject.activeSelf)
                {
                    parent.gameObject.GetComponents(cache);
                    parent = parent.parent;
                }
            }
        }

        public T[] GetComponentsInChildren<T>() where T : class
        {
            var found = new List<T>();
            GetComponentsInChildren(false, found);
            return found.ToArray();
        }

        public void GetComponentsInChildren<T>(List<T> cache) where T : class => GetComponentsInChildren(false, cache);
        public void GetComponentsInChildren<T>(bool includeUnActive, List<T> cache) where T : class
        {
            cache.Clear();
            Internal_GetComponentsInChildren(includeUnActive, cache);
        }

        private void Internal_GetComponentsInChildren<T>(bool includeUnActive, List<T> cache) where T : class
        {
            GetComponents(cache);
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if (includeUnActive || child.activeSelf)
                {
                    child.Internal_GetComponentsInChildren(includeUnActive, cache);
                }
            }
        }

        public void SetActive(bool enabled)
        {
            activeSelf = enabled;
            if (enabled)
                Invoke("OnEnable");
            else
                Invoke("OnDisable");
        }


        internal override void Wake()
        {
            base.Wake();
            if (activeSelf)
                Invoke("OnEnable");
        }
    }
}
