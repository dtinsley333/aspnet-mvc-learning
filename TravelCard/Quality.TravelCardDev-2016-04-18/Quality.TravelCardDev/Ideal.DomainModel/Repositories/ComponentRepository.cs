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
  public  class ComponentRepository : Ideal.DomainModel.Abstract.IComponentsRepository<Component>
    {

        IList<Ideal.DomainModel.Entities.Component> Ideal.DomainModel.Abstract.IComponentsRepository<Component>.GetByID(string ITMID,string Plant)
        {
            ITMID = ITMID.Trim();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Component));
                var thelist = session.CreateCriteria<Component>()
                    .Add(Restrictions.Eq("PRTID", ITMID))
                      .Add(Restrictions.Eq("PLT", Plant))
                                       .List<Component>() ;


                session.Connection.Close();
              //  session.Clear();
             //   session.Dispose();

              thelist = thelist.Where(x => x.PRTID != x.Id).ToList();
              //  thelist = thelist.Where(x => x.PLT == "06").ToList();
                return thelist;

            }
        }
        IList<Ideal.DomainModel.Entities.Component> Ideal.DomainModel.Abstract.IComponentsRepository<Component>.GetParentComponentByID(string ITMID,string Plant)
        {
            ITMID = ITMID.Trim();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Component));
                var thelist = session.CreateCriteria<Component>()
                    .Add(Restrictions.Eq("Id", ITMID))
                            .Add(Restrictions.Eq("PLT", Plant))
                                       .List<Component>();


                session.Connection.Close();
             //   session.Clear();
            //    session.Dispose();

             //   thelist = thelist.Where(x => x.PRTID == x.Id).ToList();
              //  thelist = thelist.Where(x => x.PLT == x.PRTPLT).ToList();
                return thelist;

            }
        }

        public IQueryable<Component>Components
        {
            get { throw new NotImplementedException(); }
        }


    }
}
