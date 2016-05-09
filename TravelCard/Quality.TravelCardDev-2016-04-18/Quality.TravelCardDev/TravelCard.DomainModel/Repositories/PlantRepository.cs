using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;
using TravelCard.DomainModel.Abstract;
using System.Data.Objects;



namespace TravelCard.DomainModel.Repositories
{
    public class PlantRepository : IPlantRepository
    {
        private Quality_devEntities _qualityEntities;
        private IQueryable<Plant> _plant;


        public IQueryable<Plant> Plant
        {
            get { return _plant; }
        }


        public PlantRepository(string connectionString)
        {

            _qualityEntities = new Quality_devEntities(connectionString);
            ObjectQuery<Plant> plantQuery = _qualityEntities.Plants;
            _plant = plantQuery;


        }








    }





}
