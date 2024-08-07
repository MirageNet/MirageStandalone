using System;

namespace UnityEngine
{
    public static class Debug
    {
        public static ILogger unityLogger { get; set; }

        public static void Assert(bool condition)
        {
            if (condition) unityLogger.LogError("Assertion failed");
        }
        public static void Assert(bool condition, string message)
        {
            if (condition) unityLogger.LogError(message);
        }
        public static void Log(string message) => unityLogger.Log(message);
        public static void LogWarning(string message) => unityLogger.LogWarning(message);
        public static void LogError(string message) => unityLogger.LogError(message);
        public static void LogException(Exception e) => unityLogger.LogException(e);
    }
    // enum to match unity
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
