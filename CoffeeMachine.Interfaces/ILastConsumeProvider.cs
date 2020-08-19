using CoffeeMachine.Data;
using CoffeMachine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Interfaces
{
     public interface ILastConsumeProvider
    {
        DataConsume GetLastConsume(string uid);
        void CreateOrUpdateConsume(string drinkName, string uid, int sugarLevel, bool usedMug);
    }
}
