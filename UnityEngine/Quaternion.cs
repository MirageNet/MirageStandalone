using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Quaternion : IEquatable<Quaternion>
    {
        // X component of the Quaternion. Don't modify this directly unless you know quaternions inside out.
        public float x;
        // Y component of the Quaternion. Don't modify this directly unless you know quaternions inside out.
        public float y;
        // Z component of the Quaternion. Don't modify this directly unless you know quaternions inside out.
        public float z;
        // W component of the Quaternion. Don't modify this directly unless you know quaternions inside out.
        public float w;

        // also required for being able to use Quaternions as keys in hash tables
        public override bool Equals(object other)
        {
            if (!(other is Quaternion)) return false;

            return Equals((Quaternion)other);
        }

        public bool Equals(Quaternion other)
        {
            return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z) && w.Equals(other.w);
        }
    }

}
