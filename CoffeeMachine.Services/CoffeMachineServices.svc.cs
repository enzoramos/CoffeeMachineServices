using CoffeeMachine.Data;
using CoffeeMachine.Interfaces;
using CoffeMachine.Models;
using General.LoggingInterface;
using General.ServiceLocation;
using System;
using System.Linq;

namespace CoffeeMachine.Services
{
    /// <summary>
    /// Provide services to manage coffee machine uses
    /// </summary>
    public class CoffeMachineServices : ICoffeMachineServices
    {

        #region Fields

        private Bootstrapper.Bootstrapper _boostrapper = Bootstrapper.Bootstrapper.Initialize;
        private ILogger _logger = ServiceLocator.Current.GetInstance<ILogger>();

        #endregion Fields

        #region ICoffeeMachineServices

        /// <summary>
        /// Get Last Consume of a customer
        /// </summary>
        /// <param name="uid"></param>
        /// <returns><seealso cref="DataConsume"/></returns>
        public DataConsume GetCustomerLastConsume(string uid)
        {
            _logger.Log(LogLevel.Info,$"Get last consume of {0}",uid);
            try
            {

                ILastConsumeProvider last = ServiceLocator.Current.GetInstance<ILastConsumeProvider>();
                return last.GetLastConsume(uid);
            }
            catch(InvalidOperationException e)
            {
                _logger.Log(LogLevel.Error, e, "Error occured to GetInstance of ILastConsumeProvider");
            }
            catch(ArgumentNullException e)
            {
                _logger.Log(LogLevel.Error, e, "Error occured to retrive data");
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e, "Unexpected error");
            }
            return null;
        }

        /// <summary>
        /// Get an array containing all drinks stored
        /// </summary>
        /// <returns></returns>
        public DataDrink[] GetDrinks()
        {
            _logger.Log(LogLevel.Info, "Get all drinks");

            try
            {
                IDrinkProvider drinks = ServiceLocator.Current.GetInstance<IDrinkProvider>();
                return drinks.GetAll().ToArray();
            }
            catch (InvalidOperationException e)
            {
                _logger.Log(LogLevel.Error, e, "Error occured to GetInstance of ILastConsumeProvider");
            }
            catch (ArgumentNullException e)
            {
                _logger.Log(LogLevel.Error, e, "Error occured to retrive data");
            }catch(Exception e)
            {
                _logger.Log(LogLevel.Error, e, "Unexpected error");
            }
            return null;
        }

        /// <summary>
        /// Set or Update Last consume of a customer
        /// </summary>
        /// <param name="drink"></param>
        /// <param name="uid"></param>
        /// <param name="sugarLevel"></param>
        /// <param name="usedMug"></param>
        public void SetCustomerLastConsume(String drinkName, string uid, int sugarLevel, bool usedMug)
        {
            _logger.Log(LogLevel.Info, $"Set customer : {0} consume history", uid);
            try
            {
                ILastConsumeProvider last = ServiceLocator.Current.GetInstance<ILastConsumeProvider>();
                IDrinkProvider drink = ServiceLocator.Current.GetInstance<IDrinkProvider>();

                last.CreateOrUpdateConsume(drinkName, uid, sugarLevel, usedMug);
            }
            catch (InvalidOperationException e)
            {
                _logger.Log(LogLevel.Error, e, "Error occured to GetInstance of ILastConsumeProvider");
            }
            catch (ArgumentNullException e)
            {
                _logger.Log(LogLevel.Error, e, "Error occured to create data");
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e, "Unexpected error");
            }
        }
        #endregion ICoffeeMachineServices
    }
}
