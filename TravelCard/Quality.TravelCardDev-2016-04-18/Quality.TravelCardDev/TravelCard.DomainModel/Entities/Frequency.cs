using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TravelCard.DomainModel.Entities
{
    [MetadataType(typeof(FrequencyMetaData))]
    public partial class Frequency
    {
        public string FullFrequencyDesc
        {

            get {
                return this.FrequencyID.ToString() +"--"+ this.Description_EN ;
            }
        
        }


    }

    public class FrequencyMetaData
    {

     
        
        [DisplayName("Frequency ID")]
        public int FrequencyID { get; set; }

       
        [DisplayName("Frequency(English)")]
        public string Description_EN { get; set; }

        [DisplayName("Frequency(Spanish)")]
        public string Description_MX { get; set; }

        [DisplayName("Frequency(Chinese)")]
        public string Description_CN { get; set; }

        [DisplayName("Notes")]
        public string Notes { get; set; }
     

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }


        [DisplayName("Last Editor")]
        public string LastEditBy { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        [DisplayName("Create Date")]
        public DateTime CreateDate { get; set; }

        [DisplayName("Last Edit Date")]
        public DateTime LastEditDate { get; set; }



    }
}
