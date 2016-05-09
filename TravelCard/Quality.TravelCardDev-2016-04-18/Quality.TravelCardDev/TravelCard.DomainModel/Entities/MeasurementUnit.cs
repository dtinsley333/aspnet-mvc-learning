using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TravelCard.DomainModel.Entities
{

     [MetadataType(typeof(MeasurementUnitMetaData))]
    public partial class MeasurementUnits
    {
    }



     public class MeasurementUnitMetaData
     {

         [DisplayName("Measurement Unit ID")]
         public Int16 unitID { get; set; }

         [Required(ErrorMessage="A measurement unit name is required")]
         [DisplayName("Units")]
         public string Name{get;set;}

         [Required(ErrorMessage="An abbreviation for unit of measure is required.")]
         public string Abbreviation{get;set;}

         [DisplayName("Is Active")]
         public bool IsActive { get; set; }
     
     
     
     }



}
