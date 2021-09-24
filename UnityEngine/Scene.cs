namespace UnityEngine
{
    public struct Scene
    {
        private int m_Handle;

        public int handle { get { return m_Handle; } }

        public string path;
        public bool IsValid()
        {
            return true;
        }

        private string _name;

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

    }
}
