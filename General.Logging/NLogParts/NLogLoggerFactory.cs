using General.LoggingInterface;
using NLog;
using ILogger = General.LoggingInterface.ILogger;

namespace General.Logging.NLog
{
    /// <summary>
    /// An implementation of the <see cref="ILoggerFactory"/> interface which creates <see cref="ILogger"/> instances 
    /// that use the NLog framework as the underlying logging mechanism.
    /// </summary>
    internal class NLogLoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// Returns an appropriate <see cref="ILogger"/> instance as specified by the name parameter.
        /// </summary>
        /// <param name="name">The name of the logger to return</param>
        public ILogger GetLogger(string name)
        {
            var nlogLogger = LogManager.GetLogger(name);
            return new NLogLoggerAdapter(nlogLogger);
        }

        /// <summary>
        /// Returns a logger named according to the type parameter using the <see cref="LogManager"/>.
        /// </summary>
        /// <param name="type">The type of the logger</param>
        /// <returns>logger</returns>
        public ILogger GetLogger(System.Type type)
        {
            return GetLogger(type.Name);
        }

        /// <summary>
        /// Returns a logger named according to the type parameter using the <see cref="LogManager"/>.
        /// </summary>
        /// <typeparam name="T">The type of the logger</typeparam>
        /// <returns>logger</returns>
        public ILogger GetLogger<T>()
        {
            return GetLogger(typeof(T).Name);
        }
    }
}
