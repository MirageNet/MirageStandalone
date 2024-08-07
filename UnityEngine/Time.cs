using System;
using System.Diagnostics;
using UnityEngine;

namespace UnityEngine
{
    public static class Time
    {
        static double start;

        static double GetNow() => Stopwatch.GetTimestamp() / (double)Stopwatch.Frequency;

        static Time()
        {
            start = GetNow();
        }

        public static float time => (float)(GetNow() - start);

        public static double timeAsDouble;

        // NOTE: This will have to be incremented manually!
        public static int frameCount = 0;

    }
}
