using CoffeeMachine.DataProvider;
using CoffeeMachine.DependencyInjector;
using CoffeeMachine.Interfaces;
using General.Logging;
using General.LoggingInterface;
using General.ServiceLocation;
using SimpleInjector;

namespace CoffeeMachine.Bootstrapper
{
    public sealed class Bootstrapper
    {
        private static Bootstrapper _instance = null;
        private static readonly object padlock = new object();

        /// <summary>
        /// Initialize the dependency injection mechanism.
        /// </summary>
        public static Bootstrapper Initialize
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        // initialize SimpleInjector container
                        var container = new Container();
                        container.Options.DefaultLifestyle = Lifestyle.Transient;

                        // create and setup adapter for service locator
                        var adapter = new SimpleInjectorServiceLocatorAdapter(container);
                        ServiceLocator.SetServiceLocator(adapter);

                        // register IServiceLocator
                        container.RegisterSingleton(() => ServiceLocator.Current);

                        //register Logger
                        container.Register<ILogger>(() => LoggerFactoryProvider.Instance.GetLogger("CoffeeMachineService"));

                        //register DataProvider
                        container.Register(typeof(ILastConsumeProvider), typeof(SQLLastConsumeProvider));
                        container.Register(typeof(IDrinkProvider), typeof(SQLDrinkProvider));
#if DEBUG
                        container.Verify(VerificationOption.VerifyAndDiagnose);
#endif //DEBUG
                        _instance = new Bootstrapper();
                    }
                    return _instance;
                }
            }
        }
    }
}
