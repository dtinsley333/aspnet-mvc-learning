
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TravelCard.DomainModel.Entities
{
   [MetadataType(typeof(PartSpecificationMetaData))]
    public partial class PartSpecification
    {
    }

   public class PartSpecificationMetaData
   {

       [DisplayName("Part Specification ID")]
       public int SpecificationID { get; set; }

       [DisplayName("Part Set Up ID")]
       public int PartSetUpID { get; set; }
       [DisplayName("Measurement Unit")]
       public int unitID { get; set; }

        [Required(ErrorMessage = "A measurement method is required.")]
       [DisplayName("Measurement Method ID")]
      public int MeasurementMethodID { get; set; }

     [RegularExpression("[0-9]+")] 
       [DisplayName("Operation Code")]
       public Int16? OperationCode { get; set; }

      [DisplayName("Frequency ID")]
     public Int16? FrequencyID { get; set; }

       [Required(ErrorMessage = "A description of the characteristic is required.")]
      
       [DisplayName("Characteristic")]
       public string Characteristic { get; set; }

       [DisplayName("Characteristic (Chinese-Characteristic)")]
       public string CharacteristicCN { get; set; }

       [DisplayName("característica (Spanish-Characteristic)")]
       public string CharacteristicES { get; set; }

       [Required(ErrorMessage="Please select the order in which you want to display this item. Items are ordered on screen first by the display order then ordered by characteristic.")]
       [DisplayName("Order")]
       public int SequenceID { get; set; }

      
       [RegularExpression("[-+.\0-9\\d\\s]+",ErrorMessage="Numeric values only.")]
       [DisplayName("Lower Specification")]
       public string LowSpec { get; set; }

       [DisplayName("Especificación de menor(Spanish-Lower Spec.)")]
       public string LowSpecES { get; set; }

       [RegularExpression("[-+.\0-9\\d\\s]+", ErrorMessage="Numeric values only.")]
       [DisplayName("Higher Specification")]
       public string HighSpec { get; set; }

       [DisplayName("mayor especificación (Spanish-Higher Spec.)")]
       public string HighSpecES { get; set; }

       [DisplayName("Gage Number")]
       public string Gauges { get; set; }

       [DisplayName("calibre (Spanish-Gage(s))")]
       public string GaugesES { get; set; }
       
      




       [DisplayName("Sample Size")]
       public string SampleSize { get; set; }

       [DisplayName("Tamaño de la muestra(Spanish-Sample Size)")]
       public string SampleSizeES { get; set; }

       [DisplayName("Is Component Characteristic")]
       public bool IsComponentCharacteristic { get; set; }

       [DisplayName("Is Machine Set Up Characteristic")]
       public bool IsMachineSetUpCharacteristic { get; set; }
      
       [DisplayName("Is Part Characteristic")]
       public bool IsPartCharacteristic { get; set; }

       [DisplayName("Is Die Set Up Characteristic")]
       public bool IsDieSetUpCharcteristic { get; set; }



       [DisplayName("Is Active")]
       public bool IsActive { get; set; }

        [DisplayName("Notes")]
       public string Notes { get; set; }

      [DisplayName("Last Edit Date")]
        public DateTime LastEditDate { get; set; }

    [DisplayName("Last Edited By")]
        public string LastEditBy { get; set; }

   
   }
}
