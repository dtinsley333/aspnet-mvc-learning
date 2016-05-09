using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Objects;
using System.Text;
using Syspro.DomainModel.Abstract;
using Syspro.DomainModel.Entities;


namespace Syspro.DomainModel.Repositories
{
   
        public class BomStructureRepository : IBomStructureRepository
        {
            private SysproCompanyIEntities _sysproEntities;
            private IQueryable<BomStructure> _bomstructure;


            public IQueryable<BomStructure> BomStructure
            {
                get { return _bomstructure; }
            }


            public BomStructureRepository(string connectionString)
            {

                _sysproEntities = new SysproCompanyIEntities(connectionString);
                ObjectQuery<BomStructure> bomstructureQuery = _sysproEntities.BomStructures;
                _bomstructure = bomstructureQuery;


            }



        }




    }

