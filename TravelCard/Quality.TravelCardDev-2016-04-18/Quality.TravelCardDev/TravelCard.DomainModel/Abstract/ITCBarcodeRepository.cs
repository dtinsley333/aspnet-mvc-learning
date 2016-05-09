
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;

namespace TravelCard.DomainModel.Abstract
{
    public interface ITCBarCodeRepository
    {

        IQueryable<TravelCard.DomainModel.Entities.TCBarCode> TCBarCode { get; }

        void Update(TravelCard.DomainModel.Entities.TCBarCode tcbarcode_);
        int Insert(TravelCard.DomainModel.Entities.TCBarCode tcbarcode_);

    }
}
