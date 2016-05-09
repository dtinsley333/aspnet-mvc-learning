using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ideal.DomainModel.Abstract
{
    public interface IComponentsRepository<T>
    {

        IQueryable<T> Components { get; }
        IList<T> GetByID(string id,string plant);
        IList<T> GetParentComponentByID(string id, string plant);
    }
}
