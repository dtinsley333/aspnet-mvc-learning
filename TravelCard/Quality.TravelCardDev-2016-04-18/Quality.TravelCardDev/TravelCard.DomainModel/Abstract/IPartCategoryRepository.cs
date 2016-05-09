using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;

namespace TravelCard.DomainModel.Abstract
{
    public interface IPartCategoryRepository
    {

        IQueryable<PartCategory> PartCategory { get; }
        void Update(PartCategory partcategory_);
        int Insert(PartCategory partcategory_);

    }
}
