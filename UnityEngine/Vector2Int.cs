using System;

namespace UnityEngine
{
    public struct Vector2Int : IEquatable<Vector2Int>, IFormattable
    {
        public int x;
        public int y;

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = x;
        }

        public bool Equals(Vector2Int other)
        {
            return x == other.x
                && y == other.y;
        }

        public override string ToString()
        {
            return ToString(null, null);
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return $"({x.ToString(format, formatProvider)}, {y.ToString(format, formatProvider)})";
        }
    }
}
