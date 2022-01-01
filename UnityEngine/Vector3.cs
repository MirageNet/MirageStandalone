using System;

namespace UnityEngine
{
    public struct Vector3 : IEquatable<Vector3>, IFormattable
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public bool Equals(Vector3 other)
        {
            return x == other.x
                && y == other.y
                && z == other.z;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
                format = "F1";

            return $"({x.ToString(format, formatProvider)}, {z.ToString(format, formatProvider)}, {z.ToString(format, formatProvider)})";
        }
    }
}
