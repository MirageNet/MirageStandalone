using System;
using System.Diagnostics;
using UnityEngine;

namespace UnityEngine
{
    public static class Time
    {
        static float start;

        static float GetNow() => Stopwatch.GetTimestamp() / (float)Stopwatch.Frequency;

        static Time()
        {
            start = GetNow();
        }

        public static float time => GetNow() - start;
    }
}
