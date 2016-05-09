using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ideal.DomainModel.Entities
{
    public class Part
    {

        // "SELECT DCSCIM.ITMID,DCSCIM.ITMDESC,DCSCIM.ALTDESC,DCSCIM.DTECRT From DCSCIM Where DCSCIM.ITMID='{0}'", itemid);
        public virtual string Id { get; set; }
        public virtual string ITMDESC { get; set; }
        public virtual string ALTDESC { get; set; }
        public virtual string DTECRT { get; set; }
        //TODO: add PartGroupings


    }
}
