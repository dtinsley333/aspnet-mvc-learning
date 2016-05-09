using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TravelCard.DomainModel.Entities
{
    [MetadataType(typeof(TravelCardMetaData))]
    public partial class TravelCard
    {
        public string TravelCardDesc
        {

            get
            {
                return this.TCBarCodeText + ": Printed: " + this.PrintDate.ToString()+"- Plant:"+this.PrintLocation;
            }

        }




    }

    public class TravelCardMetaData
    {

        [DisplayName("OMI ID")]
        public int TCID { get; set; }

        [DisplayName("Part Set Up ID")]
        public int PartSetUpID { get; set; }

        [DisplayName("Bar Code Text")]
        public string TCBarCodeText { get; set; }


        [DisplayName("Is ContinuationCard")]
        public bool IsContinuationCard { get; set; }

        [DisplayName("Is Draft")]
        public string IsDraft { get; set; }

        [DisplayName("Operation Code")]
        public string OperationCode { get; set; }

        [DisplayName("Language")]
        public int LanguageID { get; set; }

        [DisplayName("Print Date")]
        public DateTime PrintDate { get; set; }

        [DisplayName("Print Location")]
        public String PrintLocation { get; set; }

        [DisplayName("Printed By")]
        public String PrintedBy { get; set; }

        [DisplayName("Notes")]
        public string Notes { get; set; }


    }
}
