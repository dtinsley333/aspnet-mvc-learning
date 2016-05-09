using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;

namespace TravelCard.DomainModel.Abstract
{
    public interface IMeasurementUnitRepository
    {

        IQueryable<MeasurementUnit> MeasurementUnit { get; }

       // void Update(MeasurementUnit measurementmethod_);
       // int Insert(MeasurementUnit measurementmethod_);

    }
}
