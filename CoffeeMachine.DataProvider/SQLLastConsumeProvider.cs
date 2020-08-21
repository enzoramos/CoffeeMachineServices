using CoffeeMachine.Data;
using CoffeeMachine.Interfaces;
using CoffeMachine.Models;
using General.LoggingInterface;
using General.ServiceLocation;
using General.ToolBox;
using System;
using System.Data.Entity.Validation;
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
        public void CreateOrUpdateConsume(string drinkName, string uid, int sugarLevel, bool usedMug)
        {
            try
            {

                if (StringHelper.IsAlphaNum(uid) & StringHelper.IsAlphaNum(drinkName) & sugarLevel > -1)
                {
                    var drink = Context.Drinks.Where(x => x.Name == drinkName).First();
                    if (Context.LastConsume.Any())
                    {
                        var consume = Context.LastConsume.Where(x => x.Uid == uid).FirstOrDefault(null);

                        if (!(consume is null))
                        {
                            UpdateConsume(consume, drink, sugarLevel, usedMug);
                            return;
                        }
                    }
                    CreateConsume(drink, uid, sugarLevel, usedMug);
                    
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
                if (Context.LastConsume.Any())
                    return null;
                var lc = Context.LastConsume.Where(x => x.Uid == uid).FirstOrDefault(null);
                if (lc is null)
                    return null;
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
                try
                {
                    // Your code...
                    // Could also be before try if you know the exception occurs in SaveChanges

                    Context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        _logger.Log(LogLevel.Error,"Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            _logger.Log(LogLevel.Error, "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

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
