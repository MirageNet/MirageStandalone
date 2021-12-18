using System;

namespace UnityEngine
{
    public class Transform
    {
        public Vector3 position => throw new NotSupportedException();
        public Quaternion rotation => throw new NotSupportedException();
        public Vector3 localPosition => throw new NotSupportedException();
        public Quaternion localRotation => throw new NotSupportedException();
        public Vector3 localScale => throw new NotSupportedException();
        public Transform parent => throw new NotSupportedException();
    }
}
