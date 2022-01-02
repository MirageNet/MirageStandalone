using System;
using System.Diagnostics;

namespace UnityEngine
{
    public static class Debug
    {
        public static ILogger unityLogger => throw new NotSupportedException();

        public static void Assert(bool condition, string msg)
        {
            if (condition)
                throw new Exception($"Assert Failed: {msg}");
        }

        public static void Log(string message) => unityLogger.Log(message);
        public static void LogWarning(string message) => unityLogger.LogWarning(message);
        public static void LogError(string message) => unityLogger.LogError(message);
    }
    public static class Time
    {
        static float start;

        static float GetNow() => Stopwatch.GetTimestamp() / (float)Stopwatch.Frequency;

        static Time()
        {
            start = GetNow();
        }

        public static float time => GetNow() - start;

        public static int frameCount => throw new NotSupportedException();
    }

    // eum to match unity
    public enum LogType
    {
        Error = 0,
        Assert = 1,
        Warning = 2,
        Log = 3,
        Exception = 4,
    }

    // Interface to match unity
    public interface ILogHandler
    {
        void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args);

        void LogException(Exception exception, UnityEngine.Object context);
    }

    // Interface to match unity
    public interface ILogger : ILogHandler
    {
        LogType filterLogType { get; set; }
        ILogHandler logHandler { get; set; }
        bool logEnabled { get; set; }

        bool IsLogTypeAllowed(LogType logType);
        void Log(LogType type, object message);

        void Log(object message);
        void LogWarning(object message);
        void LogError(object message);

        void LogException(Exception ex);
    }

    public static class ILoggerExtensions
    {
        public static void Log(this ILogger logger, object message, Object _context) => logger.Log(message);
        public static void LogWarning(this ILogger logger, object message, Object _context) => logger.LogWarning(message);
        public static void LogError(this ILogger logger, object message, Object _context) => logger.LogError(message);
    }
}
