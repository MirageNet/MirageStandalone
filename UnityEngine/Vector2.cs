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

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
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
