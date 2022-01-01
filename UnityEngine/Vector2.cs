using System;

namespace UnityEngine
{
    public struct Vector2 : IEquatable<Vector2>, IFormattable
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(Vector2 other)
        {
            return x == other.x
                && y == other.y;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
                format = "F1";

            return $"({x.ToString(format, formatProvider)}, {y.ToString(format, formatProvider)})";
        }
    }
}
