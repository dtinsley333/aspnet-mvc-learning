using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TravelCard.DomainModel.Entities
{
    [MetadataType(typeof(MeasurementMethodMetaData))]
    public partial class MeasurementMethod
    {
        public string FullMeasurementDesc
        {

            get
            {
                return this.Description_EN +" - " + this.MeasurementMethodID.ToString();
            }

        }



    }

    public class MeasurementMethodMetaData
    {

        [DisplayName("Measurement Method ID")]
        public int MeasurementMethodID { get; set; }

        [Required(ErrorMessage = "An English description is required.")]
        [DisplayName("Measurement Method(English)")]
        public string Description_EN { get; set; }

        [DisplayName("Measurement Method(Spanish)")]
        public string Description_MX { get; set; }

        [DisplayName("Measurement Method(Chinese)")]
        public string Description_CN { get; set; }

   

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [DisplayName("Notes(Does not display on the travel card)")]
        public string Notes { get; set; }


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
