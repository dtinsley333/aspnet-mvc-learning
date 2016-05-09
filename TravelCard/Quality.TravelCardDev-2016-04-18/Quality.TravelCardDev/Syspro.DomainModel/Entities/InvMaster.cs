using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Syspro.DomainModel.Entities
{
  [MetadataType(typeof(InvMasterMetaData))]
    public partial class InvMaster
    {







    }

  public class InvMasterMetaData
  {
      [DisplayName("Stock Code")]
      public string Stockcode { get; set; }

      [DisplayName("Description")]
      public string Description { get; set; }
  
  }
}
