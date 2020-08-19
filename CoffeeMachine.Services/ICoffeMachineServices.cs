using CoffeeMachine.Data;
using CoffeMachine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CoffeeMachine.Services
{
    /// <summary>
    /// Provide services to manage coffee machine uses
    /// </summary>
    [ServiceContract]
    public interface ICoffeMachineServices
    {
        /// <summary>
        /// Get an array containing all drinks stored
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DataDrink[] GetDrinks();

        /// <summary>
        /// Get Last Consume of a customer
        /// </summary>
        /// <param name="uid"></param>
        /// <returns><seealso cref="DataConsume"/></returns>
        [OperationContract]
        DataConsume GetCustomerLastConsume(string uid);

        /// <summary>
        /// Set or Update Last consume of a customer
        /// </summary>
        /// <param name="drink"></param>
        /// <param name="uid"></param>
        /// <param name="sugarLevel"></param>
        /// <param name="usedMug"></param>
        [OperationContract]
        void SetCustomerLastConsume(Drink drink, string uid, int sugarLevel, bool usedMug);

    }
    
}
