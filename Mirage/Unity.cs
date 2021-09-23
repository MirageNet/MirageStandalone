namespace Mirage
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

    public class Vector3
    {
        public float x;
        public float y;
        public float z;
    }

    public class Vector2
    {
        public float x;
        public float y;
    }

    public class Quaternion {}

    public enum HideFlags : byte { NotEditable,HideAndDontSave };

    public class Transform
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 localPosition;
        public Quaternion localRotation;
        public Vector3 localScale;
    }
}
