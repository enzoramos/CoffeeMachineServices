using General.LoggingInterface;
using System;
using System.Threading;

namespace General.Logging
{
    /// <summary>
    /// Provides a global repository that provides access to <see cref="ILogger"/> instances.
    /// </summary>
    public static class LoggerFactoryProvider
    {
        /// <summary>
        /// The logger factory
        /// </summary>
        private static Lazy<ILoggerFactory> _factory = new Lazy<ILoggerFactory>(() => new NLog.NLogLoggerFactory(), LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Returns the <see cref="ILoggerFactory"/> instance in use.
        /// </summary>
        public static ILoggerFactory Instance
        {
            get { return _factory.Value; }
        }
    }
}
