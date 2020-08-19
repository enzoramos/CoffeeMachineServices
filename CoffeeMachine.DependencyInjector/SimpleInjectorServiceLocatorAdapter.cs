using General.ServiceLocation;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.DependencyInjector
{
    /// <summary>
    /// IServiceLocator implementation around SimpleInjector dependency injection library. <br/>
    /// https://github.com/simpleinjector/SimpleInjector <br/>
    /// http://simpleinjector.readthedocs.io/ <br/>
    /// </summary>
    public class SimpleInjectorServiceLocatorAdapter : IServiceLocator
    {
        // The SimpleInjector
        private readonly Container _container;

        /// <summary>
        /// Instanciate the adapter with a SimpleInjector container instance.
        /// </summary>
        /// <param name="container"></param>
        public SimpleInjectorServiceLocatorAdapter(Container container)
        {
            _container = container;
        }

        /// <summary>
        /// Returns all instances of TService type.
        /// </summary>
        /// <typeparam name="TService">The service type</typeparam>
        /// <returns>The instances</returns>
        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return _container.GetAllInstances(typeof(TService)).Cast<TService>();
        }

        /// <summary>
        /// Returns all instances of serviceType type.
        /// </summary>
        /// <param name="serviceType">The service type</param>
        /// <returns>The instances</returns>
        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            IServiceProvider serviceProvider = _container;
            Type collectionType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            var collection = (IEnumerable<object>)serviceProvider.GetService(collectionType);
            return collection ?? Enumerable.Empty<object>();
        }

        /// <summary>
        /// Returns an instance of type TService matching a key.
        /// </summary>
        /// <typeparam name="TService">The service type</typeparam>
        /// <param name="key">The key to the service instance</param>
        /// <returns>An instance of the service</returns>
        public TService GetInstance<TService>(string key)
        {
            return (TService)GetInstance(typeof(TService));
        }

        /// <summary>
        /// Returns an instance of type TService.
        /// </summary>
        /// <typeparam name="TService">The service type</typeparam>
        /// <returns>An instance of the service</returns>
        public TService GetInstance<TService>()
        {
            return (TService)_container.GetInstance(typeof(TService));
        }

        /// <summary>
        /// Returns an instance of type serviceType matching a key.
        /// </summary>
        /// <param name="serviceType">The service type</param>
        /// <param name="key">The key to the service instance</param>
        /// <returns>An instance of the service</returns>
        public object GetInstance(Type serviceType, string key)
        {
            return GetInstance(serviceType);
        }

        /// <summary>
        /// Returns an instance of type serviceType.
        /// </summary>
        /// <param name="serviceType">The service type</param>
        /// <returns>An instance of the service</returns>
        public object GetInstance(Type serviceType)
        {
            return _container.GetInstance(serviceType);
        }

        /// <summary>
        /// Returns an instance of type serviceType.
        /// </summary>
        /// <param name="serviceType">The service type</param>
        /// <returns>An instance of the service</returns>
        public object GetService(Type serviceType)
        {
            IServiceProvider serviceProvider = _container;
            return serviceProvider.GetService(serviceType);
        }
    }
}
