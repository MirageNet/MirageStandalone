using System;

namespace UnityEngine
{
    public struct Color32 : IEquatable<Color32>
    {
        public byte r;
        public byte g;
        public byte b;
        public byte a;

        public Color32(byte r, byte g, byte b, byte a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public bool Equals(Color32 other)
        {
            return r == other.r
                && g == other.g
                && b == other.b
                && a == other.a;
        }
    }
}
