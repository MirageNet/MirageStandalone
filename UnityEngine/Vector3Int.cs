using System;

namespace UnityEngine
{
    public struct Vector3Int : IEquatable<Vector3Int>, IFormattable
    {
        public int x;
        public int y;
        public int z;

        public Vector3Int(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public bool Equals(Vector3Int other)
        {
            return x == other.x
                && y == other.y
                && z == other.z;
        }

        public override string ToString()
        {
            return ToString(null, null);
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return $"({x.ToString(format, formatProvider)}, {y.ToString(format, formatProvider)}, {z.ToString(format, formatProvider)})";
        }
    }
}
