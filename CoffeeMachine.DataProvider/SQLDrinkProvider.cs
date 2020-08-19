using CoffeeMachine.Data;
using CoffeeMachine.Interfaces;
using CoffeMachine.Models;
using General.LoggingInterface;
using General.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeMachine.DataProvider
{
    /// <summary>
    /// Class to provide drink data from SQLI
    /// </summary>
    public class SQLDrinkProvider : IDrinkProvider 
    {
        #region Fields
        private ILogger _logger = ServiceLocator.Current.GetInstance<ILogger>();
        public DataBaseContext Context { get; set; } = new DataBaseContext();
        #endregion Fields

        public DataDrink[] GetAll()
        {
            try
            {

                    var d = Context.Drinks.ToList();
                    List<DataDrink> result = new List<DataDrink>();
                    foreach (Drink dr in d)
                    {
                        result.Add(new DataDrink() { Name = dr.Name });
                    }

                    return result.ToArray();
                
 
            }
            catch( Exception e)
            {
                _logger.Log(LogLevel.Error, e, "Error occured while retrieving drinks");
                return null;
            }
        }

        public DataDrink GetByName(string name)
        {
            try
            {
                    return new DataDrink() { Name = Context.Drinks.Where(d => d.Name == name).First().Name };
                
            }
            catch(Exception e)
            {
                _logger.Log(LogLevel.Error, e, "Error occured while retrieving drink");
                return null;
            }
        }
    }
}
