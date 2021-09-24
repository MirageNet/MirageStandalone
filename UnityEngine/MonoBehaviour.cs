namespace UnityEngine
{
    public class MonoBehaviour : GameObject
    {
        public GameObject gameObject;

        public void DontDestroyOnLoad(GameObject obj)
        {

        }

        public T GetComponent<T>() where T : MonoBehaviour
        {
            // TODO: GetCmponent
            return (T) new MonoBehaviour();
        }

        public void Destroy(GameObject obj)
        {
            // TODO: destroy
        }

        public GameObject Instantiate(GameObject obj, Vector3 position, Quaternion rotation)
        {
            // TODO: Instantiate
            return new GameObject();
        }
    }
}
