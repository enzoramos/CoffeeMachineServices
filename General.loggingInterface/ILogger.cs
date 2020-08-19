using System;
using System.Collections.Generic;

namespace General.LoggingInterface
{
    /// <summary>
    /// Logger interface which exposes methods for logging.
    /// </summary>
    public interface ILogger
    {
        #region properties
        /// <summary>
        /// Gets this logger's name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// manage logger properties.
        /// </summary>
        IDictionary<object, object> Properties { get; set; }
        #endregion properties

        #region methods
        /// <summary>
        /// Logs a message at the requested level.
        /// </summary>
        /// <param name="level">The level of log entry</param>
        /// <param name="message">The message to log</param>
        void Log(LogLevel level, string message);

        /// <summary>
        /// Logs a message at the requested level according to the specified <paramref name="format"/> and <paramref name="args"/>.
        /// </summary>
        /// <param name="level">The level of log entry</param>
        /// <param name="format">A composite format string that contains placeholders for the arguments</param>
        /// <param name="args">An <see cref="object"/> array containing zero or more objects to format</param>
        void Log(LogLevel level, string format, params object[] args);

        /// <summary>
        /// Logs an exception and an additional message at the requested level.
        /// </summary>
        /// <param name="level">The level of log entry</param>
        /// <param name="exception"> The exception to log</param>
        /// <param name="message">Additional information regarding the logged exception</param>
        void Log(LogLevel level, Exception exception, string message);

        /// <summary>
        /// Logs an exception and an additional message at the requested level.
        /// </summary>
        /// <param name="level">The level of log entry</param>
        /// <param name="exception">The exception to log</param>
        /// <param name="format">A composite format string that contains placeholders for the arguments</param>
        /// <param name="args">An <see cref="object"/> array containing zero or more objects to format</param>
        void Log(LogLevel level, Exception exception, string format, params object[] args);
        #endregion methods
    }

    /// <summary>
    /// level of logged event.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Trace = 0
        /// </summary>
        Trace,
        /// <summary>
        /// Debug = 1
        /// </summary>
        Debug,
        /// <summary>
        /// Info = 2
        /// </summary>
        Info,
        /// <summary>
        /// Warn = 3
        /// </summary>
        Warn,
        /// <summary>
        /// Error = 4
        /// </summary>
        Error,
        /// <summary>
        /// Fatal = 5
        /// </summary>
        Fatal,
    }
}
