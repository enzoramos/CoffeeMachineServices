using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using CoffeMachine.Models;
using System.Collections.Generic;
using System.Linq;
using CoffeeMachine.Bootstrapper;

namespace CoffeeMachine.DataProvider.UnitTest
{
    [TestClass]
    public class SQLDrinkProviderUnitTest
    {
        private Mock<DbSet<Drink>> mockSet = new Mock<DbSet<Drink>>();
        private Bootstrapper.Bootstrapper _boostrapper = Bootstrapper.Bootstrapper.Initialize;
        SQLDrinkProvider provider = new SQLDrinkProvider();


        [TestInitialize]
        public void Initialize()
        {
            var data = new List<Drink>
            {
                new Drink { Name = "Tea" },
                new Drink { Name = "Coffee" },
                new Drink { Name = "Chocolate" },
            }.AsQueryable();
            Mock<DataBaseContext> mockContext = new Mock<DataBaseContext>();
            mockSet.As< IQueryable < Drink >>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As< IQueryable < Drink >>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As< IQueryable < Drink >>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As< IQueryable < Drink >>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockContext.Setup(m => m.Drinks).Returns(mockSet.Object);
            provider.Context = mockContext.Object;

        }
        [TestMethod]
        public void GetAll()
        {
            var drinks = provider.GetAll();

            Assert.AreEqual(drinks.Count(), 3);
            Assert.AreEqual(drinks[0].Name, "Tea");
            Assert.AreEqual(drinks[1].Name, "Coffee");
            Assert.AreEqual(drinks[2].Name, "Chocolate");
        }

        [TestMethod]
        public void GetByName_ValidEntry()
        {
            var drink = provider.GetByName("Chocolate");
            Assert.AreEqual(drink.Name, "Chocolate");
        }

        [TestMethod]
        public void GetByName_InvalidEntry()
        {
            var drink = provider.GetByName("Colate");
            Assert.IsNull(drink);
        }

        [TestMethod]
        public void GetByName_NullEntry()
        {
            var drink = provider.GetByName(null);
            Assert.IsNull(drink);
        }
    }
}
