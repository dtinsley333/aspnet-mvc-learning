using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Syspro.DomainModel.Abstract
{
   public class IInvMasterRepository
    {

       IQueryable<Syspro.DomainModel.Entities.InvMaster> InvMaster { get; set; }

    }
}
