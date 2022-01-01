using System;

namespace UnityEngine
{
    public struct Color : IEquatable<Color>
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public Color(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public bool Equals(Color other)
        {
            return r == other.r
                && g == other.g
                && b == other.b
                && a == other.a;
        }
    }
}
