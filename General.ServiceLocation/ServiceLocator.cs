using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.ServiceLocation
{
    /// <summary>
    /// Provide access to <see cref="IServiceLocator"/> instance.
    /// </summary>
    public static class ServiceLocator
    {
        /// <summary>
        /// The service locator instance.
        /// </summary>
        private static IServiceLocator _locator;

        /// <summary>
        /// Allow initialization of ServiceLocator.
        /// </summary>
        /// <param name="locator">The <see cref="IServiceLocator"/> instance</param>
        public static void SetServiceLocator(IServiceLocator locator)
        {
            _locator = locator;
        }

        /// <summary>
        /// Provide access to the current <see cref="IServiceLocator"/> instance.
        /// </summary>
        public static IServiceLocator Current
        {
            get
            {
                if (_locator == null)
                    throw new InvalidOperationException("Service locator not set");

                return _locator;
            }
        }
    }
}
