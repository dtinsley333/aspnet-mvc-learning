using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;

namespace TravelCard.DomainModel.Abstract
{
    public interface IPartSpecificationSequenceRepository
    {

        IQueryable<PartSpecificationSequence> PartSpecificationSequence { get; }
    }
}
