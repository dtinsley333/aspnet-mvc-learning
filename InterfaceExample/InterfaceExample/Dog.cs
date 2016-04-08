using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceExample
{
    class Dog : ICanine
    {
        public int AverageLifeSpan { get; set; }
        public int AverageWeight { get; set; }
        public int GestationDays { get; set; }
        public string CoatColor { get; set; }
        public string CommonName { get; set; }
        public bool IsDomestic { get; set; }
        public string Diet { get; set; }
        public bool IsEndangered { get; set; }
        public string SpeciesName { get; set; }

        public string GetHabitatBasedOnCountry(string country)
        {
            if (country == "United States" || country == "Canada")
            {
                Debug.WriteLine(country);
                return "mainly in households though there feral dog populations North America";
            }

            return "Habitat Unknown";
        }

        public int? GetPopulationBasedOnCountry(string country)
        {
            switch (country)
            {
                case "Australia":
                    return 2323234;
                case "United States":
                    return 6663234;
                case "Canada":
                    return 6663234;
                default:
                    return null;

            }

        }
    }

}

