using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3Int : IEquatable<Vector3Int>
    {
        // X component of the vector.
        public int x;

        // Y component of the vector.
        public int y;

        // Z component of the vector.
        public int z;

        // also required for being able to use Vector3s as keys in hash tables
        public override bool Equals(object other)
        {
            if (!(other is Vector3Int)) return false;

            return Equals((Vector3Int)other);
        }

        public bool Equals(Vector3Int other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Int(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
