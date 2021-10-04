using System;

namespace UnityEngine
{
    public class GameObject : Object
    {
        public bool activeSelf;
        public HideFlags hideFlags;
        public Transform transform;

        public void SetActive(bool state)
        {
            activeSelf = state;
        }
        public void Destroy(GameObject gameObject)
        {
            throw new NotImplementedException();
        }

        public void DontDestroyOnLoad(GameObject gameObject)
        {
            throw new NotImplementedException();
        }

        public T GetComponent<T>()
        {
            _ = TryGetComponent(out T component);
            return component;
        }

        public bool TryGetComponent<T>(out T component)
        {
            throw new NotImplementedException();
        }

        public GameObject Instantiate(GameObject prefab)
        {
            throw new NotImplementedException();
        }

        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            throw new NotImplementedException();
        }
    }
}
