using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TravelCard.DomainModel.Entities
{
    [MetadataType(typeof(PartSpecificationSequenceMetaData))]
    public partial class PartSpecificationSequence
    {
        public string FullSequenceOrderDesc
        {

            get
            {
                return this.SequenceOrderID.ToString() + "-" + this.SequenceText.ToString();
            }

        }


    }

    public class PartSpecificationSequenceMetaData
    {

        [DisplayName("Sequence ID")]
        public int SequenceOrderID { get; set; }

       
        [DisplayName("Sequence Order")]
        public string SequenceOrder { get; set; }

        [DisplayName("Sequence Text")]
      
        public string SequenceText { get; set; }

      





    }
}
