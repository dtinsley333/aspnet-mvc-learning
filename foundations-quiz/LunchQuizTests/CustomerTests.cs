using System;
using LunchQuiz;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchQuizTests
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void CusotmerCanCreateInstance()
        {
            Customer Bob = new Customer();
            Assert.IsNotNull(Bob);
        }
        
        [TestMethod]
        public void CustomerIsReadyToOrder()
        {
            Customer Bob = new Customer();
            Restaurant joes = new Restaurant();
            MenuItem coke = new MenuItem();
            coke.Name = "coke";
            MenuItem lasagna = new MenuItem();
            lasagna.Name = "lasagna";
            MenuItem milkshake = new MenuItem();
            milkshake.Name = "milkshake";

            joes.MenuItems.Add(coke);
            joes.MenuItems.Add(lasagna);
            joes.MenuItems.Add(milkshake);

            //The object initializer syntax used for bobsOrder is a shortcut used to set properties on an object when creating it
            Order bobsOrder = new Order
            {
                Drink = "coke",
                Entree = "lasagna",
                Dessert = "milkshake"
            };
            Assert.IsTrue(bobsOrder.VerifyOrderComplete());
            Assert.IsTrue(Bob.ReadyToPlaceOrder(bobsOrder, joes));
        }

        [TestMethod]
        public void CustomerIsNotReadyToOrder()
        {
            Customer Bob = new Customer();
            Restaurant joes = new Restaurant();
            MenuItem coke = new MenuItem();
            coke.Name = "coke";
            MenuItem lasagna = new MenuItem();
            lasagna.Name = "lasagna";
            MenuItem milkshake = new MenuItem();
            milkshake.Name = "milkshake";

            joes.MenuItems.Add(coke);
            joes.MenuItems.Add(lasagna);
            joes.MenuItems.Add(milkshake);

            Order bobsOrder = new Order
            {
                Drink = "coke",
                Entree = "lasagna"
                //Dessert = "milkshake"
            };
            Assert.IsFalse(bobsOrder.VerifyOrderComplete()); //False!
            Assert.IsFalse(Bob.ReadyToPlaceOrder(bobsOrder, joes)); //Also false!
        }

        [TestMethod]
        public void CustomerIsStillNotReadyToOrder()
        {
            Customer Bob = new Customer();
            Restaurant joes = new Restaurant();
            MenuItem coke = new MenuItem();
            coke.Name = "coke";
            MenuItem lasagna = new MenuItem();
            lasagna.Name = "lasagna";
            MenuItem milkshake = new MenuItem();
            milkshake.Name = "milkshake";

            joes.MenuItems.Add(coke);
            joes.MenuItems.Add(lasagna);
            //joes.MenuItems.Add(milkshake); Milkshakes are not on the menu =-(

            Order bobsOrder = new Order
            {
                Drink = "coke",
                Entree = "lasagna",
                Dessert = "milkshake"
            };
            Assert.IsTrue(bobsOrder.VerifyOrderComplete());
            Assert.IsFalse(Bob.ReadyToPlaceOrder(bobsOrder, joes));
        }

        [TestMethod]
        public void CustomerIsReadyToOrderFromAllTypesOfRestaurants()
        {
            Customer Bob = new Customer();
            FastFood krustys = new FastFood(); //Uncomment this line after noticing what is different
            MenuItem coke = new MenuItem();
            coke.Name = "coke";
            MenuItem lasagna = new MenuItem();
            lasagna.Name = "lasagna";
            MenuItem milkshake = new MenuItem();
            milkshake.Name = "milkshake";

            krustys.MenuItems.Add(coke);
            krustys.MenuItems.Add(lasagna);
            krustys.MenuItems.Add(milkshake);

            //The object initializer syntax used for bobsOrder is a shortcut used to set properties on an object when creating it
            Order bobsOrder = new Order
            {
                Drink = "coke",
                Entree = "lasagna",
                Dessert = "milkshake"
            };
            Assert.IsTrue(bobsOrder.VerifyOrderComplete());
            Assert.IsTrue(Bob.ReadyToPlaceOrder(bobsOrder, krustys));
        }

    }
}
