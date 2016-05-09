using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Syspro.DomainModel.Entities
{
    [MetadataType(typeof(BomStructureMetaData))]
    public partial class BomStructure
    {



    }

    public class BomStructureMetaData
    {
        [DisplayName("Parent Part")]
        public string ParentPart { get; set; }

        [DisplayName("Component")]
        public string Component { get; set; }

    }
}