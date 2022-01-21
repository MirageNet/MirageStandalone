using System;
using System.Collections.Generic;

namespace UnityEngine
{
    public class Transform : MonoBehaviour
    {
        public int childCount;

        public Vector3 position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
        public Quaternion rotation { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
        public Vector3 localPosition { get; set; }
        public Quaternion localRotation { get; set; }
        public Vector3 localScale { get; set; }

        public Transform parent { get; internal set; }

        public Transform GetChild(int i)
        {
            return children[i];
        }

        internal List<Transform> children = new List<Transform>();
    }
}
