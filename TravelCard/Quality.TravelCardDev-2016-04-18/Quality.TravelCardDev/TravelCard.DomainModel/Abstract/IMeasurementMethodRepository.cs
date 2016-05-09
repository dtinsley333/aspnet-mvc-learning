using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;

namespace TravelCard.DomainModel.Abstract
{
    public interface IMeasurementMethodRepository
    {

        IQueryable<MeasurementMethod> MeasurementMethod { get; }
    
      void Update(MeasurementMethod measurementmethod_);
       int Insert(MeasurementMethod measurementmethod_);

    }
}
