using System;

namespace UnityEngine.SceneManagement
{
    public struct Scene
    {
        public int handle => throw new NotSupportedException();
        public string path => throw new NotSupportedException();

        public string name
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public bool IsValid()
        {
            return false;
        }

        public static bool operator ==(Scene a, Scene b) => throw new NotSupportedException();
        public static bool operator !=(Scene a, Scene b) => throw new NotSupportedException();
    }
}
