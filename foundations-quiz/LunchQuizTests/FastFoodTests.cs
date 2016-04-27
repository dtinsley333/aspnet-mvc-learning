using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LunchQuiz;

namespace LunchQuizTests
{
    [TestClass]
    public class FastFoodTests
    {
        [TestMethod]
        public void FastFoodEnsureICanCreateInstance()
        {
            FastFood krustys = new FastFood();
            Assert.IsNotNull(krustys);
        }
        [TestMethod]
        public void FastFoodCanSetDriveThrough()
        {
            FastFood krustys = new FastFood();
            krustys.HasDriveThrough = true;
            Assert.IsTrue(krustys.HasDriveThrough);
        }

        [TestMethod]
        public void FastFoodCanAddMenuItemWithJustName()
        {
            FastFood krustys = new FastFood();
            int BeginningNumberOfMenuItems = krustys.MenuItems.Count;
            krustys.addMenuItem("Chicken Nuggets");
            Assert.AreEqual(BeginningNumberOfMenuItems + 1, krustys.MenuItems.Count);
        }

        [TestMethod]
        public void FastFoodCanAddMenuItemWithNameAndPrice()
        {
            FastFood krustys = new FastFood();
            int BeginningNumberOfMenuItems = krustys.MenuItems.Count;
            krustys.addMenuItem("Chicken Nuggets", 4.00);
            Assert.AreEqual(BeginningNumberOfMenuItems + 1, krustys.MenuItems.Count);
        }
    }
}
