using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TravelCard.DomainModel.Entities
{
    [MetadataType(typeof(UserSettingMetaData))]
    public partial class UserSetting
    {



    }

    public class UserSettingMetaData
    {

        [DisplayName("User ID")]
        public int UserID { get; set; }

      
      
        [DisplayName("PlantName")]
        [Required(ErrorMessage = "A Plant name is required.")]
        public int PlantCodeID { get; set; }

        [DisplayName("Language")]
        public string LanguageID { get; set; }





    }
}
