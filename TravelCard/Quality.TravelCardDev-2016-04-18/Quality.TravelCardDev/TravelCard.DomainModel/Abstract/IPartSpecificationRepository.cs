
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;

namespace TravelCard.DomainModel.Abstract
{
    public interface IPartSpecificationRepository
    {

        IQueryable<PartSpecification> PartSpecification { get; }
        void Update(PartSpecification partspecification_);
        int Insert(PartSpecification partspecification_);
        IEnumerable<PartSpecification> PartSpecifications { get; }
        void InsertClonedPartSpecification(PartSpecification partspecification_);
        void UpdateSpecificationSequenceOrder(PartSpecification partspecification_);
    }
}
