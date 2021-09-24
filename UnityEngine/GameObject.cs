namespace UnityEngine
{
    public class GameObject : Object
    {
        public bool activeSelf;
        public HideFlags hideFlags;
        public Transform transform;

        public T GetComponent<T>() where T : MonoBehaviour
        {
            return (T) new MonoBehaviour();
        }

        public void SetActive(bool state)
        {
            activeSelf = state;
        }
    }
}
