using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ideal.DomainModel.Abstract
{
    public interface IPartsRepository<T>
    {

       IQueryable<T> Parts { get;}
       IEnumerable<T> GetByID(string id,string plant);
       String GetPartDescription(string id);
    }
}
