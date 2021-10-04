using System;
using UnityEngine;

namespace Mirage.Logging
{
    public class StandaloneLogger : ILogger
    {
        public ILogHandler logHandler { get; set; }
        public bool logEnabled { get; set; }
        public LogType filterLogType { get; set; }

        public StandaloneLogger()
        {
            // set initial log handler to be itself
            logHandler = this;
            filterLogType = LogType.Log;
            logEnabled = true;
        }

        public bool IsLogTypeAllowed(LogType logType)
        {
            if (!logEnabled) { return false; }
            if (logType == LogType.Exception) { return true; }
            if (filterLogType == LogType.Exception) { return false; }

            // if check type is less than logger type
            // eg check error <= warning === true
            return (logType <= filterLogType);
        }

        public void Log(LogType type, object message)
        {
            logHandler.LogFormat(type, null, message.ToString());
        }

        public void Log(object message)
        {
            logHandler.LogFormat(LogType.Log, null, message.ToString());
        }

        public void LogWarning(object message)
        {
            logHandler.LogFormat(LogType.Warning, null, message.ToString());
        }

        public void LogError(object message)
        {
            logHandler.LogFormat(LogType.Error, null, message.ToString());
        }

        public void LogException(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(ex.Message);
        }

        #region Implementation of ILogHandler
        ConsoleColor[] logTypeToColor = new ConsoleColor[] {
            ConsoleColor.Red,
            ConsoleColor.Red,
            ConsoleColor.Yellow,
            ConsoleColor.White,
            ConsoleColor.Red,
        };


        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            if (IsLogTypeAllowed(logType))
            {
                Console.ForegroundColor = logTypeToColor[(int)logType];
                Console.WriteLine(string.Format(format, args));
                Console.ResetColor();
            }
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            if (logEnabled)
            {
                Console.ForegroundColor = logTypeToColor[(int)LogType.Exception];
                Console.WriteLine(exception);
                Console.ResetColor();
            }
        }

        #endregion
    }
}
