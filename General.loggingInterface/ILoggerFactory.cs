using System;

namespace General.LoggingInterface
{
    /// <summary>
    /// <see cref="ILoggerFactory"/> instances manufacture <see cref="ILogger"/> instances by name. <br/>
    /// These factory methods may create new instances or retrieve cached / pooled instances
    /// depending on the the name of the requested logger.
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Returns an appropriate <see cref="ILogger"/> instance as specified by the name parameter.
        /// </summary>
        /// <param name="name">The name of the logger to return</param>
        /// <returns>A ILogger instance.</returns>
        ILogger GetLogger(string name);

        /// <summary>
        /// Returns a logger named according to the type parameter.
        /// </summary>
        /// <param name="type">The type of the logger.</param>
        /// <returns>A ILogger instance.</returns>
        ILogger GetLogger(Type type);

        /// <summary>
        /// Returns a logger named according to the type of T.
        /// </summary>
        /// <typeparam name="T">The type of the logger.</typeparam>
        /// <returns>A ILogger instance.</returns>
        ILogger GetLogger<T>();
    }
}
