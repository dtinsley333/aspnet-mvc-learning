using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceExample.Canines
{
    public class Dingo:ICanine
    {
        public string SpeciesName { get; set; }  //example-the scientific name for dog is Canis lupus familiaris
        public string CommonName { get; set; }//dog, coyote, wolf, dingo
        public bool IsDomestic { get; set; }
        public bool IsEndangered { get; set; }
        public int GestationDays { get; set; }
        public string CoatColor { get; set; }
        public string Diet { get; set; }
        public int AverageWeight { get; set; }
        public int AverageLifeSpan { get; set; }
        public string GetHabitatBasedOnCountry(string country)
        {
            //put login here to figure out habitat, could hard code it
            //could get from a database, could get from an api.
            return "Habitat Not Found";
        }
        public int? GetPopulationBasedOnCountry(string country)
        {
            //put login here to figure out habitat, could hard code it
            //could get from a database, could get from an api.
            //if an the answer can't be found the return null
            return null;
        }
    }
}
