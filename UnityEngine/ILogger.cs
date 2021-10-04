using System;

namespace UnityEngine
{
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
}
