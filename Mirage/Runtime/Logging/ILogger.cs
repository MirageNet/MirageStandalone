using System;

    //Based on: https://docs.unity3d.com/ScriptReference/LogType.html
    public enum LogType
    {
        Error,
        Assert,
        Warning,
        Log,
        Exception,
    }

    public interface ILogger
    {
        LogType filterLogType { get; set; }

        bool IsLogTypeAllowed(LogType logType);

        void Log(object message);

        void Log(LogType  type , object message);

        void LogWarning(ILogger logger, object message);

        void LogWarningFormat(object message, object extra);

        void LogError(object message, object extra);

        void LogError(ILogger logger, object message);

        void LogException(Exception ex);
    }

    public class StandaloneLogger : ILogger
    {
        public LogType filterLogType { get; set; }

        public bool IsLogTypeAllowed(LogType logType)
        {
            return true;
        }

        public void Log(object message)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(message);
        }

        public void Log(LogType type, object message)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(type.ToString() + " : " + message);
        }

        public void LogError(ILogger logger, object message)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(message);
        }

        public void LogError(object message, object extra)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(message + " : " + extra);
        }

    public void LogException(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(ex.Message);
        }

        public void LogWarning(ILogger logger, object message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(message);
        }

        public void LogWarningFormat(object message, object extra)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(message);
        }
    }