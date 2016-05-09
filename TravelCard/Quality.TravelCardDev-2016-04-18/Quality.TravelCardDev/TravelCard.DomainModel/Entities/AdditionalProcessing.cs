
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TravelCard.DomainModel.Entities
{
   
    [MetadataType(typeof(AdditionalProcessingMetaData))]
    public partial class AdditionalProcessing
    {

        public string FullAdditionalProcessDesc
        {

            get
            {
                return this.Description +"-" + this.ProcessingID.ToString();
            }

        }



    }

    public class AdditionalProcessingMetaData
    { 
    [DisplayName("Additional Processing ID")]
        public int ProcessingID{get;set;}

    [DisplayName("Part Set Up ID")]
        public int PartSetUpID { get; set; }

    [DisplayName("Notes")]
        public string Notes { get; set; }

   
    [DisplayName("Description(English)")]
    public string Description { get; set; }

    [DisplayName("Description(Chinese)")]
    public string DescriptionCN { get; set; }

    [DisplayName("descripción (Spanish)")]
     public string DescriptionES { get; set; }

    [DisplayName("Active")]
        public bool IsActive { get; set; }

    [DisplayName("Order")]
         public int SequenceID { get; set; }

    [DisplayName("Last Edit")]
         public DateTime LastEditDate { get; set; }

    [DisplayName("Last Edited By")]
         public string LastEditedBy { get; set; }




    
    }



}
