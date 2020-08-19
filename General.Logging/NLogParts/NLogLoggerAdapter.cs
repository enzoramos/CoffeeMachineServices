using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using NLogLevel = NLog.LogLevel;
using General.LoggingInterface;
using ILogger = General.LoggingInterface.ILogger;
using LogLevel = General.LoggingInterface.LogLevel;

namespace General.Logging.NLog
{
    /// <summary>
    /// An implementation of the <see cref="ILogger"/> interface which logs messages using the NLog framework.
    /// </summary>
    internal class NLogLoggerAdapter : ILogger
    {
        #region Fields
        /// <summary>
        /// The NLog logger which this class wraps.
        /// </summary>
        private Logger _logger;

        /// <summary>
        /// The wrapper (this class) type.
        /// </summary>
        private Type _wrapperType;
        #endregion Fields

        #region Ctor
        /// <summary>
        /// Constructs an instance of <see cref="NLogLoggerAdapter"/> by wrapping a NLog logger.
        /// </summary>
        /// <param name="logger">The NLog logger to wrap</param>internal NLogLoggerAdapter(Logger logger)
        internal NLogLoggerAdapter(Logger logger)
        {
            _logger = logger;
            _wrapperType = this.GetType();
            Name = logger.Name;
            Properties = new Dictionary<object, object>();
        }
        #endregion Ctor

        #region ILogger
        /// <summary>
        /// The logger name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The logger properties.
        /// </summary>
        public IDictionary<object, object> Properties { get; set; }

        /// <summary>
        /// Logs a message at the ERROR level.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="message">The message to log.</param>
        public void Log(LogLevel level, string message)
        {
            LogInternal(level, message, null, null);
        }

        /// <summary>
        /// Logs a message at the ERROR level according to the specified <paramref name="format"/> and <paramref name="args"/>.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="format">A composite format string that contains placeholders for the arguments.</param>
        /// <param name="args">An <see cref="object"/>Array containing zero or more objects to format.</param>
        public void Log(LogLevel level, string format, params object[] args)
        {
            LogInternal(level, format, args, null);
        }

        /// <summary>
        /// Logs an exception and an additional message at the ERROR level.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Additional information regarding the logged exception.</param>
        public void Log(LogLevel level, Exception exception, string message)
        {
            LogInternal(level, message, null, exception);
        }

        /// <summary>
        /// Logs an exception and an additional message at the ERROR level.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="format">A composite format string that contains placeholders for the arguments.</param>
        /// <param name="args">An <see cref="object"/>Array containing zero or more objects to format.</param>
        public void Log(LogLevel level, Exception exception, string format, params object[] args)
        {
            LogInternal(level, format, args, exception);
        }
        #endregion ILogger

        #region Methods
        /// <summary>
        /// Perform the logging message writing.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="messageOrFormat">Message or format.</param>
        /// <param name="args">Args to use in case messageOrFormat is a format.</param>
        /// <param name="exception">The exception to log if provided.</param>
        private void LogInternal(LogLevel level, string messageOrFormat, object[] args, Exception exception)
        {
            var nlogLevel = Convert(level);
            if (_logger.IsEnabled(nlogLevel) == false)
                return;

            LogInternal(nlogLevel, Properties.ToList(), messageOrFormat, args, exception);
        }

        /// <summary>
        /// Perform the logging message writing.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="properties">Properties at log request time.</param>
        /// <param name="messageOrFormat">Message or format.</param>
        /// <param name="args">Args to use in case messageOrFormat is a format.</param>
        /// <param name="exception">The exception to log if provided.</param>
        private void LogInternal(NLogLevel level, List<KeyValuePair<object, object>> properties, string messageOrFormat, object[] args, Exception exception)
        {
            var le = LogEventInfo.Create(level, Name, exception, null, messageOrFormat, args);
            properties.ForEach(kvp => le.Properties.Add(kvp.Key, kvp.Value));
            _logger.Log(_wrapperType, le);

            if (exception != null)
            {
                if (exception is AggregateException aggregate)
                {
                    foreach (var innerException in aggregate.InnerExceptions)
                        LogInternal(level, properties, null, null, innerException);
                }

                if (exception.InnerException != null)
                    LogInternal(level, properties, null, null, exception.InnerException);
            }
        }

        /// <summary>
        /// Convert LogLevel enum to NLog.LogLevel.
        /// </summary>
        /// <param name="level">The LogLevel to convert.</param>
        /// <returns>The converted LogLevel.</returns>
        private NLogLevel Convert(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Trace:
                    return NLogLevel.Trace;
                case LogLevel.Debug:
                    return NLogLevel.Debug;
                case LogLevel.Info:
                    return NLogLevel.Info;
                case LogLevel.Warn:
                    return NLogLevel.Warn;
                case LogLevel.Error:
                    return NLogLevel.Error;
                case LogLevel.Fatal:
                    return NLogLevel.Fatal;
                default:
                    return NLogLevel.Off;
            }
        }
        #endregion Methods
    }
}
