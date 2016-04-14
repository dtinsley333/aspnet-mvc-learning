using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterfaceExample;
using InterfaceExample.Canines;


namespace CanineTester
{
    [TestClass]
    public class WolfTests
    {

        Wolf wolf = new Wolf
        {
            GestationDays = 60,
            AverageLifeSpan = 13,
            AverageWeight = 60,
            CoatColor = "Varies",
            CommonName = "Wolf",
            Diet = "Other wildlife",
            IsDomestic = false,
            IsEndangered = false,
            SpeciesName = "Canis lupus"
        };

        [TestMethod]
        public void WolfPopulation()
        {
            var wolfPopulation = wolf.GetPopulationBasedOnCountry("United States");
            Assert.IsTrue(wolfPopulation > 0 || wolfPopulation == null);
        }

        [TestMethod]
        public void WolfHabitat()
        {
            var wolfPopulation = wolf.GetHabitatBasedOnCountry("United States");
            Assert.IsTrue(!String.IsNullOrEmpty(wolfPopulation));
        }
    }
}

