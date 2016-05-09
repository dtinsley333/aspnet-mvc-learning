
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;

namespace TravelCard.DomainModel.Abstract
{
    public interface ITravelCardRepository
    {

        IQueryable<TravelCard.DomainModel.Entities.TravelCard> TravelCard { get; }

        void Update(TravelCard.DomainModel.Entities.TravelCard travelcard_);
        int Insert(TravelCard.DomainModel.Entities.TravelCard travelcard_);

    }
}
