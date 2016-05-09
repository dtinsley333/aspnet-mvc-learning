using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ideal.DomainModel.Entities;
using Ideal.DomainModel.Abstract;
using NHibernate;
using NHibernate.Criterion;

namespace Ideal.DomainModel.Repositories
{
    public class PartRepository : Ideal.DomainModel.Abstract.IPartsRepository<Part>
    {



        IEnumerable<Ideal.DomainModel.Entities.Part> Ideal.DomainModel.Abstract.IPartsRepository<Part>.GetByID(string ITMID, string Plant)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Part));
                List<Part> thelist=session.CreateCriteria<Part>().Add(Restrictions.Eq("Id", ITMID)).List<Part>().ToList();
                session.Connection.Close();
               // session.Clear();
               // session.Dispose();
                return thelist;
          
            }
        }

        public string GetPartDescription(string partid)
        {
         using (ISession session = NHibernateHelper.OpenSession())
            {
         ICriteria criteria = session.CreateCriteria(typeof(Part));
                string thename=session.CreateCriteria<Part>().Add(Restrictions.Eq("Id", partid)).ToString();
                session.Connection.Close();
               // session.Clear();
                return thename;

             
        }
        }

        public IQueryable<Part> Parts
        {
            get { throw new NotImplementedException(); }
        }
    }
}
