using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.ServiceLocation
{
    public interface IServiceLocator : IServiceProvider
    {
        /// <summary>
        /// Returns all instances of TService type.
        /// </summary>
        /// <typeparam name="TService">The service type</typeparam>
        /// <returns>The instances</returns>
        IEnumerable<TService> GetAllInstances<TService>();

        /// <summary>
        /// Returns all instances of serviceType type.
        /// </summary>
        /// <param name="serviceType">The service type</param>
        /// <returns>The instances</returns>
        IEnumerable<object> GetAllInstances(Type serviceType);

        /// <summary>
        /// Returns an instance of type TService matching a key.
        /// </summary>
        /// <typeparam name="TService">The service type</typeparam>
        /// <param name="key">The key to the service instance</param>
        /// <returns>An instance of the service</returns>
        TService GetInstance<TService>(string key);

        /// <summary>
        /// Returns an instance of type TService.
        /// </summary>
        /// <typeparam name="TService">The service type</typeparam>
        /// <returns>An instance of the service</returns>
        TService GetInstance<TService>();

        /// <summary>
        /// Returns an instance of type serviceType matching a key.
        /// </summary>
        /// <param name="serviceType">The service type</param>
        /// <param name="key">The key to the service instance</param>
        /// <returns>An instance of the service</returns>
        object GetInstance(Type serviceType, string key);

        /// <summary>
        /// Returns an instance of type serviceType.
        /// </summary>
        /// <param name="serviceType">The service type</param>
        /// <returns>An instance of the service</returns>
        object GetInstance(Type serviceType);

        /// <summary>
        /// Returns an instance of type serviceType.
        /// </summary>
        /// <param name="serviceType">The service type</param>
        /// <returns>An instance of the service</returns>
        new object GetService(Type serviceType);
    }
}
