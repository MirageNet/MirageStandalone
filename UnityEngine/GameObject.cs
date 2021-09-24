using System;
using System.Numerics;

namespace UnityEngine
{
    public class GameObject
    {
        public bool activeSelf;
        public HideFlags hideFlags;
        public string name;
        public Transform transform;

        public void SetActive(bool state)
        {
            activeSelf = state;
        }
        public void Destroy(GameObject gameObject)
        {

        }

        public void DontDestroyOnLoad(GameObject gameObject)
        {

        }

        public T GetComponent<T>()
        {
            throw new NotImplementedException();
        }


        public GameObject Instantiate(GameObject prefab)
        {
            return prefab;
        }

        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return prefab;
        }
    }
}
