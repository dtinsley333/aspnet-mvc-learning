using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;
using TravelCard.DomainModel.Abstract;
using System.Data.Objects;



namespace TravelCard.DomainModel.Repositories
{
    public class PartSpecificationSequenceRepository : IPartSpecificationSequenceRepository
    {
        private Quality_devEntities _qualityEntities;
        private IQueryable<PartSpecificationSequence> _partspecificationsequence;


        public IQueryable<PartSpecificationSequence> PartSpecificationSequence
        {
            get { return _partspecificationsequence; }
        }


        public PartSpecificationSequenceRepository(string connectionString)
        {

            _qualityEntities = new Quality_devEntities(connectionString);
            ObjectQuery<PartSpecificationSequence> partspecificationsequenceQuery = _qualityEntities.PartSpecificationSequences;
            _partspecificationsequence = partspecificationsequenceQuery;


        }








    }





}
