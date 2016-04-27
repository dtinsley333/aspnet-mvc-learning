using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LunchQuiz;
//there's a specific "using" you need in order to create lists
using System.Collections.Generic;

namespace LunchQuizTests
{
    [TestClass]
    public class RestaurantTests
    {
        [TestMethod]
        public void RestaurantEnsureICanCreateInstance()
        {
            Restaurant joes = new Restaurant();
            Assert.IsNotNull(joes);
        }

        [TestMethod]
        public void RestaurantHasMenuItems()
            //try to pass this test and the one below it at the same time
        {
            Restaurant joes = new Restaurant();
            Assert.IsNotNull(joes.MenuItems);
            
        }
        [TestMethod]
        public void RestaurantMenuItemsAreCorrectType()
        {
            Restaurant joes = new Restaurant();
            Assert.IsInstanceOfType(joes.MenuItems, typeof(List<MenuItem>));
        }
        [TestMethod]
        public void RestaurantHasCurrentCustomers()
        //try to pass this test and the one below it at the same time
        {
            Restaurant joes = new Restaurant();
            Assert.IsNotNull(joes.CurrentCustomers);

        }
        [TestMethod]
        public void RestaurantCurrentCustomersAreCorrectType()
        {
            Restaurant joes = new Restaurant();
            Assert.IsInstanceOfType(joes.CurrentCustomers, typeof(List<Customer>));
        }
        [TestMethod]
        public void RestaurantCanAddToCurrentCustomer()
        {
            Restaurant joes = new Restaurant();
            Customer Steve = new Customer();
            joes.CurrentCustomers.Add(Steve);
            CollectionAssert.Contains(joes.CurrentCustomers, Steve);
        }

        [TestMethod]
        public void RestaurantCanAddToCurrentCustomerAnotherWay()
        {
            Restaurant joes = new Restaurant();
            Customer Steve = new Customer();
            joes.AddCustomerToList(Steve);
            CollectionAssert.Contains(joes.CurrentCustomers, Steve);
        }
        [TestMethod]
        public void RestaurantCanSetNumberOf4Tops()
        {
            Restaurant joes = new Restaurant();
            joes.Numberof4PersonTables = 8;
            Assert.AreEqual(8, joes.Numberof4PersonTables);
        }
        [TestMethod]
        public void RestaurantCanSetNumberOf2Tops()
        {
            Restaurant joes = new Restaurant();
            joes.Numberof2PersonTables = 6;
            Assert.AreEqual(6, joes.Numberof2PersonTables);
        }
        [TestMethod]
        public void RestaurantCanCalculateCapacity()
        {
            Restaurant joes = new Restaurant();
            joes.Numberof4PersonTables = 8;
            joes.Numberof2PersonTables = 6;
            Assert.AreEqual(44, joes.capacity());
        }

        [TestMethod]
        public void RestaurantCanCheckIfAnItemIsOnMenuWhenGivenString()
        //HINT: https://msdn.microsoft.com/en-us/library/bfed8bca(v=vs.110).aspx
        {
            Restaurant joes = new Restaurant();
            MenuItem flavorsOfFried = new MenuItem();
            flavorsOfFried.Name = "Sampler Platter";
            joes.MenuItems.Add(flavorsOfFried);
            Assert.IsTrue(joes.ItemIsOnMenu("Sampler Platter"));        
        }

        [TestMethod]
        public void RestaurantCanCheckIfAnItemIsOnMenuWhenGivenAMenuItem()
        {
            Restaurant joes = new Restaurant();
            MenuItem flavorsOfFried = new MenuItem();
            flavorsOfFried.Name = "Sampler Platter";
            joes.MenuItems.Add(flavorsOfFried);
            Assert.IsTrue(joes.ItemIsOnMenu(flavorsOfFried));
        }

    }
}
