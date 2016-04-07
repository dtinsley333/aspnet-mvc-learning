using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Dog dog = new Dog
            {
                GestationDays = 60,
                AverageLifeSpan = 13,
                AverageWeight = 35,
                CoatColor = "Varies",
                CommonName = "Dog",
                Diet = "Dog food and purloined snacks",
                IsDomestic = true,
                IsEndangered=false,
                SpeciesName= "Canis lupus familiaris"
    };

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

            //display wolf info
            Console.Write("WOLF INFORMATION" + "\n");
            var wolfCountry = "United States, Canada";
            var wolfPopulation = wolf.GetPopulationBasedOnCountry(wolfCountry);
            var wolfHabitat = wolf.GetHabitatBasedOnCountry(wolfCountry);
            Console.Write("Population for wolves is " + wolfPopulation + " in " + wolfCountry + "\n");
            Console.Write("Habitat for wolves is " + wolfHabitat + " in " + wolfCountry + "\n");
            Console.Write("The scientific name for a wolf is " + wolf.SpeciesName);
            Console.Write("Wolves like to eat " + wolf.Diet + "\n");
            

           //TODO: Refactor the concatenation spaghetti

            //display dog info
            Console.Write("DOG INFORMATION" + "\n");
            var dogCountry = "United States";
            var dogPopulation = dog.GetPopulationBasedOnCountry(dogCountry);
            var dogHabitat = dog.GetHabitatBasedOnCountry(dogCountry);
            Console.Write("Population for dogs is " + dogPopulation + " in " + dogCountry + "\n");
            Console.Write("Habitat for dogs is " + dogHabitat + " in " + dogCountry + "\n");
            Console.Write("The scientific name for a dog is " + dog.SpeciesName + "\n");
            Console.Write("Dogs like to eat " + dog.Diet);
           
          
        }
    }
}
