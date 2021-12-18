using System;

namespace UnityEngine
{
    public struct Mathf
    {
        public static int CeilToInt(float value)
        {
            return (int)Math.Ceiling(value);
        }

        public static int FloorToInt(float value)
        {
            return (int)Math.Floor(value);
        }

        public static float Log(float value, int newBase)
        {
            return (float)Math.Log(value, newBase);
        }

        public static int RoundToInt(float value)
        {
            return (int)Math.Round(value);
        }
    }
}
