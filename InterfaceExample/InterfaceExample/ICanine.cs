using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceExample
{
    public interface ICanine
    {
        string SpeciesName { get; set; }  //example-the scientific name for dog is Canis lupus familiaris
        string CommonName { get; set; }//dog, coyote, wolf, dingo
        bool IsDomestic { get; set; }
        bool IsEndangered { get; set; }
        int GestationDays { get; set; }
        string CoatColor { get; set; }
        string Diet { get; set; }
        int AverageWeight { get; set; }
        int AverageLifeSpan { get; set; }
        string GetHabitatBasedOnCountry(string country);
        int? GetPopulationBasedOnCountry(string country);  //made nullable since code needs to be able to handle unfound country code, country codes frequently change.
    }
}
