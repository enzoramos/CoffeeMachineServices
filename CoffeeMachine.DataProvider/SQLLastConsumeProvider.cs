using CoffeeMachine.Data;
using CoffeeMachine.Interfaces;
using CoffeMachine.Models;
using General.LoggingInterface;
using General.ServiceLocation;
using General.ToolBox;
using System;
using System.Linq;

namespace CoffeeMachine.DataProvider
{
    public class SQLLastConsumeProvider : ILastConsumeProvider
    {
        #region Properties
        private ILogger _logger = ServiceLocator.Current.GetInstance<ILogger>();
        public DataBaseContext Context { get; set; } = new DataBaseContext();
        #endregion #region Properties

        #region ILastConsumeProvider
        public void CreateOrUpdateConsume(Drink drink, string uid, int sugarLevel, bool usedMug)
        {
            try
            {

                if (StringHelper.IsAlphaNum(uid) & null != drink & sugarLevel > -1)
                {
                    var consume = Context.LastConsume.Where(x => x.Uid == uid).First();
                    if (consume is null)
                    {
                        CreateConsume(drink, uid, sugarLevel, usedMug);
                        return;
                    }
                    UpdateConsume(consume, drink, sugarLevel, usedMug);
                }
                _logger.Log(LogLevel.Warn, "Invalid Parameters");
            }

            catch (ArgumentNullException e)
            {
                _logger.Log(LogLevel.Error, e, "Error occured to retrive data");
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e, "Unexpected error");
            }


        }

        public DataConsume GetLastConsume(string uid)
        {
            try
            {
                var lc = Context.LastConsume.Where(x => x.Uid == uid).First();
                return new DataConsume() { Consume = new DataDrink() { Name = lc.Drink1.Name }, Uid = lc.Uid, SugarLevel = lc.SugarLevel, UsedMug = lc.UseMug };

            }
            catch (ArgumentNullException e)
            {
                _logger.Log(LogLevel.Error, e, "Error occured to retrive data");
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e, "Unexpected error");
            }
            return null;
        }
        #endregion ILastConsumeProvider

        #region privateMethods
        private void CreateConsume(Drink drink, string uid, int sugarLevel, bool usedMug)
        {
            var consume = new LastConsume()
            {
                Uid = uid,
                Drink = drink.Id,
                Drink1 = drink,
                SugarLevel = sugarLevel,
                UseMug = usedMug,
            };


                using (var dbContextTransaction = Context.Database.BeginTransaction())
                {
                    var query = Context.LastConsume.Add(consume);
                    Context.SaveChanges();
                    dbContextTransaction.Commit();
                }
            
        }
        private void UpdateConsume(LastConsume consume, Drink drink, int sugarLevel, bool usedMug)
        {
            consume.Drink1 = drink;
            consume.Drink = drink.Id;
            consume.SugarLevel = sugarLevel;
            consume.UseMug = usedMug;


                using (var dbContextTransaction = Context.Database.BeginTransaction())
                {
                    var query = Context.LastConsume.Add(consume);
                    Context.SaveChanges();
                    dbContextTransaction.Commit();
                }
        }
        #endregion privateMethods
    }
}
