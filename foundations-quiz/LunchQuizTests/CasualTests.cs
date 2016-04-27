using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using LunchQuiz;

namespace LunchQuizTests
{
    [TestClass]
    public class CasualTests
    {
        [TestMethod]
        public void CasualCanCreateInstance()
        {
            Casual tchotchkes = new Casual();
            Assert.IsNotNull(tchotchkes);
        }
        [TestMethod]
        public void CasualCanSetPatio()
        {
            Casual tchotchkes = new Casual();
            tchotchkes.HasPatio = false;
            Assert.IsFalse(tchotchkes.HasPatio);
        }
        [TestMethod]
        public void CasualRestaurantsAreCreatedWithSomethingAlreadyOnTheMenu()
            //you will need to write a constructor function
        {
            Casual tchotchkes = new Casual();
            Assert.IsTrue(tchotchkes.MenuItems.Count > 0);

        }
         
    }
}
