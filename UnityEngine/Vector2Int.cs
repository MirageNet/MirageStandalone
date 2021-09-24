#if NETCOREAPP
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace UnityEngine
{
    /// <summary>
    ///     Unity code license. This is enough to mimic unity vectorInt2
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2Int : IEquatable<Vector2Int>, IFormattable
    {
        public int x
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return m_X; }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { m_X = value; }
        }

        public int y
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return m_Y; }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { m_Y = value; }
        }

        private int m_X;
        private int m_Y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Int(int x, int y)
        {
            m_X = x;
            m_Y = y;
        }

        #region Implementation of IEquatable<Vector2Int>

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(Vector2Int other)
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
            return $"({x.ToString(format, formatProvider)}, {y.ToString(format, formatProvider)})";
        }

        #endregion
    }
}
#endif
