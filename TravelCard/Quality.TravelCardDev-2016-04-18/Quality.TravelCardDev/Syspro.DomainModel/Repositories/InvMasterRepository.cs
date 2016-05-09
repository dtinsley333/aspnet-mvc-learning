using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Objects;
using System.Text;
using Syspro.DomainModel.Abstract;
using Syspro.DomainModel.Entities;


namespace Syspro.DomainModel.Repositories
{
  
        public class InvMasterRepository : IInvMasterRepository
        {
            private SysproCompanyIEntities _sysproEntities;
            private IQueryable<InvMaster> _invmaster;


            public IQueryable<InvMaster> InvMaster
            {
                get { return _invmaster; }
            }


            public InvMasterRepository(string connectionString)
            {

             _sysproEntities = new SysproCompanyIEntities(connectionString);
            ObjectQuery<InvMaster> invmasterQuery = _sysproEntities.InvMasters;
             _invmaster = invmasterQuery;


            }



        }




    }

