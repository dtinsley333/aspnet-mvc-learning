using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TravelCard.DomainModel.Entities
{
    [MetadataType(typeof(LanguageMetaData))]
    public partial class Language
    {



    }

    public class LanguageMetaData
    {

        [DisplayName("Language ID")]
        public int LanguageID { get; set; }

        [Required(ErrorMessage = "A Language code is required.")]
        [DisplayName("Language Code")]
        public string LanguageCode { get; set; }

        [DisplayName("LanguageName")]
        [Required(ErrorMessage = "A Language name is required.")]
        public string PlantName { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }





    }
}
