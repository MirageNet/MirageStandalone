using System;

namespace UnityEngine
{
    public struct Quaternion : IFormattable
    {
        // todo Quaternion math

        public float x;
        public float y;
        public float z;
        public float w;

        public Quaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
                }
                throwOutOfRange();
                return default;
            }
        }

        private static void throwOutOfRange()
        {
            throw new IndexOutOfRangeException();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
                format = "F1";

            return $"({x.ToString(format, formatProvider)}, {z.ToString(format, formatProvider)}, {z.ToString(format, formatProvider)}, {w.ToString(format, formatProvider)})";
        }
    }
}
