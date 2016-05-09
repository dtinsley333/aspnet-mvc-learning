using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;
using TravelCard.DomainModel.Abstract;
using System.Data.Objects;



namespace TravelCard.DomainModel.Repositories
{
    public class MeasurementUnitRepository : IMeasurementUnitRepository
    {
        private Quality_devEntities _qualityEntities;
        private IQueryable<MeasurementUnit> _measurementunit;


        public IQueryable<MeasurementUnit> MeasurementUnit
        {
            get { return _measurementunit; }
        }


        public MeasurementUnitRepository(string connectionString)
        {

            _qualityEntities = new Quality_devEntities(connectionString);
            ObjectQuery<MeasurementUnit> measurementunitQuery = _qualityEntities.MeasurementUnits;
            _measurementunit = measurementunitQuery;


        }


      



     



    }





}
