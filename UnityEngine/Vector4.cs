using System;

namespace UnityEngine
{
    public struct Vector4 : IEquatable<Vector4>
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public bool Equals(Vector4 other)
        {
            return x == other.x
                && y == other.y
                && z == other.z
                && w == other.w;
        }
    }
}
