using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TravelCard.DomainModel.Entities
{
    [MetadataType(typeof(TCBarCodeMetaData))]
    public partial class TCBarCode
    {



    }

    public class TCBarCodeMetaData
    {

        

        [DisplayName("Bar Code ID")]
        public int TCBarCodeID { get; set; }

        [DisplayName("Part Set Up ID")]
        public int PartSetUpID { get; set; }

        [DisplayName("Bar Code Text")]
        public int BarCodeText { get; set; }

        [DisplayName("Bar Code File")]
        public int BarCodeFile { get; set; }


     


    }
}
