using System;

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

    public class MonoBehaviour : GameObject
    {
        public GameObject gameObject;
    }

    public class Scene : GameObject
    {
        public string path;
        public bool IsValid()
        {
            return true;
        }
    }

    public struct Vector3
    {
        public float x;
        public float y;
        public float z;
        public object Value { get; set; }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            Value = null;
        }
    }

    public struct Vector3Int
    {
        public int x;
        public int y;
        public int z;

        public Vector3Int(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public struct Vector2Int
    {
        public int x;
        public int y;
    }

    public struct Vector2
    {
        public float x;
        public float y;

        public bool HasValue { get; internal set; }
        public object Value { get; set; }
    }

    public struct Quaternion
    {
        public float w;
        public float x;
        public float y;
        public float z;
    }

    public enum HideFlags : byte { NotEditable,HideAndDontSave };

    public class Transform
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 localPosition;
        public Quaternion localRotation;
        public Vector3 localScale;
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class RuntimeInitializeOnLoadMethodAttribute : Attribute {
        public RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType loadType) {

        }
    }

    public enum RuntimeInitializeLoadType {
        BeforeSceneLoad
    };
}
