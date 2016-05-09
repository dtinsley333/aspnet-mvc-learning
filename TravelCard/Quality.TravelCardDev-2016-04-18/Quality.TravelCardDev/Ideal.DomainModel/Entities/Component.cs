using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ideal.DomainModel.Entities
{
   public class Component
    {                                                          

        public virtual string Id { get; set; }
     
        public virtual string PRTID { get; set; }             
        public virtual string PRTPLT { get; set; }             
        public virtual string PLT { get; set; }             
        public virtual string USGRAT { get; set; }              
        public virtual decimal BEGEFF { get; set; }             
        public virtual decimal ENDEFF { get; set; }
        public virtual string ITMDESC { get; set; }    
    }
}
