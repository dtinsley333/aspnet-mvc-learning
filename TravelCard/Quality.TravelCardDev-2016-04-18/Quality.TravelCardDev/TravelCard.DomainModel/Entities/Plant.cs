using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TravelCard.DomainModel.Entities
{
    [MetadataType(typeof(PlantMetaData))]
    public partial class Plant
    {



    }

    public class PlantMetaData
    {

        [DisplayName("Plant Code ID")]
        public int PlantCodeID { get; set; }

        [Required(ErrorMessage = "A Plant code is required.")]
        [DisplayName("Plant Code")]
        public string PlantCode { get; set; }

        [DisplayName("PlantName")]
        [Required(ErrorMessage = "A Plant name is required.")]
        public string PlantName { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        



    }
}
