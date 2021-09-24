#if NETCOREAPP

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
    /// <summary>
    ///     Unity code license. This is enough to mimic unity vector2
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2 : IEquatable<Vector2>, IFormattable
    {
        // X component of the vector.
        public float x;
        // Y component of the vector.
        public float y;

        // Constructs a new vector with given x, y components.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2(float x, float y) { this.x = x; this.y = y; }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
            }
        }

#region Implementation of IEquatable<Vector2>

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector2 other)
        {
            return x == other.x && y == other.y;
        }

#endregion

#region Implementation of IFormattable

        /// <summary>Formats the value of the current instance using the specified format.</summary>
        /// <param name="format">The format to use.
        /// -or-
        /// A null reference (<see langword="Nothing" /> in Visual Basic) to use the default format defined for the type of the <see cref="T:System.IFormattable" /> implementation.</param>
        /// <param name="formatProvider">The provider to use to format the value.
        /// -or-
        /// A null reference (<see langword="Nothing" /> in Visual Basic) to obtain the numeric format information from the current locale setting of the operating system.</param>
        /// <returns>The value of the current instance in the specified format.</returns>
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if (string.IsNullOrEmpty(format))
                format = "F1";

            return $"({x.ToString(format, formatProvider)}, {y.ToString(format, formatProvider)})";
        }

#endregion
    }
}
#endif
