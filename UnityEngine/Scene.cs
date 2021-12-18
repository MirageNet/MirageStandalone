using System;

namespace UnityEngine
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
    }
}
