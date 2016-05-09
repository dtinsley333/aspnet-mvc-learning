using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Syspro.DomainModel.Abstract
{
   public class IBomStructureRepository
    {

       IQueryable<Syspro.DomainModel.Entities.BomStructure> BomStructure { get; set; }



    }
}
