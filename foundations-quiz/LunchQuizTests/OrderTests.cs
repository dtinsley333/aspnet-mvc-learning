using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LunchQuiz;

namespace LunchQuizTests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void OrderEnsureICanCreateInstance()
        {
            Order whatIwantToEat = new Order();
            Assert.IsNotNull(whatIwantToEat);
        }

        [TestMethod]
        public void OrderEnsureOrderHasAllProperties()
        {
            Order myOrder = new Order();
            myOrder.Drink = "diet coke";
            myOrder.Entree = "burger";
            myOrder.Dessert = "milkshake";
            Assert.AreEqual("diet coke", myOrder.Drink);
            Assert.AreEqual("burger", myOrder.Entree);
            Assert.AreEqual("milkshake", myOrder.Dessert);
        }
        [TestMethod]
        public void OrderIfYouDontSetThePropertiesTheyAreNull()
            //This is pretty much default, once  you declare the properties, this test should pass without any other code being written
        {
            Order myOrder = new Order();
            Assert.IsNull(myOrder.Drink);
            Assert.IsNull(myOrder.Entree);
            Assert.IsNull(myOrder.Dessert);
        }
        [TestMethod]
        public void OrderCantBeVerifiedIfSomethingIsMissing()
        {
            Order myOrder = new Order();
            myOrder.Drink = "diet coke";
            myOrder.Entree = "burger";
            //myOrder.Dessert = ???;
            Assert.IsFalse(myOrder.VerifyOrderComplete());
        }
        [TestMethod]
        public void OrderCantBeVerifiedIfAnythingIsMissing()
        {
            Order myOrder = new Order();
            //myOrder.Drink = ???;
            myOrder.Entree = "burger";
            myOrder.Dessert = "milkshake";
            Assert.IsFalse(myOrder.VerifyOrderComplete());
        }

        [TestMethod]
        public void OrderIsVerifiedIfAllPartsArePresent()
        {
            Order myOrder = new Order();
            myOrder.Drink = "diet coke";
            myOrder.Entree = "burger";
            myOrder.Dessert = "milkshake";
            Assert.IsTrue(myOrder.VerifyOrderComplete());
        }
    }
}
