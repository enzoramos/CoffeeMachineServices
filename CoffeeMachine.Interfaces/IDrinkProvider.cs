using CoffeeMachine.Data;

namespace CoffeeMachine.Interfaces
{
    /// <summary>
    /// Provide methods to interact with DataDrink
    /// </summary>
    public interface IDrinkProvider
    {
        /// <summary>
        /// Get a Drink by his name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        DataDrink GetByName(string name);
        /// <summary>
        /// Get an array of all drinks
        /// </summary>
        /// <returns></returns>
        DataDrink[] GetAll();
    }
}
