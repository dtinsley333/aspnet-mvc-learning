using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;
using TravelCard.DomainModel.Abstract;
using System.Data.Objects;

namespace TravelCard.DomainModel.Repositories
{
  public  class PartSpecificationRepository : IPartSpecificationRepository
    {
        private Quality_devEntities _qualityEntities;
        private IQueryable<PartSpecification> _partSpecification;
        private IEnumerable<PartSpecification> _partSpecifications;

        public PartSpecificationRepository(string connectionString)
        {
            _qualityEntities = new Quality_devEntities(connectionString);
            ObjectQuery<PartSpecification> partSpecificationQuery = _qualityEntities.PartSpecifications;
            _partSpecification = partSpecificationQuery;
        }

        public IQueryable<PartSpecification> PartSpecification
        {
            get { return _partSpecification; }
        }


        public IEnumerable<PartSpecification> PartSpecifications   
        {
            get
            { return _partSpecifications; }
        
        }
        public void Update(PartSpecification partspecification_)
        {
          //test
            var partspecificationtoupdate = _qualityEntities.PartSpecifications
                .FirstOrDefault(x => x.SpecificationID == partspecification_.SpecificationID);

            partspecificationtoupdate.Notes = partspecification_.Notes;
            partspecificationtoupdate.Characteristic = partspecification_.Characteristic;
            partspecificationtoupdate.CharacteristicES = partspecification_.CharacteristicES;
            partspecificationtoupdate.CharacteristicCN = partspecification_.CharacteristicCN;
            partspecificationtoupdate.unitID = partspecification_.unitID;
            partspecificationtoupdate.LowSpec = partspecification_.LowSpec;
            partspecificationtoupdate.LowSpecES = partspecification_.LowSpecES;
            partspecificationtoupdate.HighSpec = partspecification_.HighSpec;
            partspecificationtoupdate.HighSpecES = partspecification_.HighSpecES;
            partspecificationtoupdate.IsActive = partspecification_.IsActive;
            partspecificationtoupdate.MeasurementMethodID = partspecification_.MeasurementMethodID;
            partspecificationtoupdate.Gauges = partspecification_.Gauges;
            partspecificationtoupdate.GaugesES = partspecification_.GaugesES;
            partspecificationtoupdate.SampleSize = partspecification_.SampleSize;
            partspecificationtoupdate.SampleSizeES = partspecification_.SampleSizeES;
            partspecificationtoupdate.FrequencyID = partspecification_.FrequencyID;
            
            partspecificationtoupdate.OperationCode = partspecification_.OperationCode;
            partspecificationtoupdate.IsComponentCharacteristic = partspecification_.IsComponentCharacteristic;
            partspecificationtoupdate.IsMachineSetUpCharacteristic = partspecification_.IsMachineSetUpCharacteristic;
            partspecificationtoupdate.IsPartCharacteristic = partspecification_.IsPartCharacteristic;
            partspecificationtoupdate.IsDieSetUpCharcteristic = partspecification_.IsDieSetUpCharcteristic;
            partspecificationtoupdate.LastEditBy = partspecification_.LastEditBy;
            partspecificationtoupdate.LastEditDate = DateTime.Now;
            _qualityEntities.SaveChanges(SaveOptions.None);

        }


        public void UpdateSpecificationSequenceOrder(PartSpecification partspecification_)
        {
            var partspecificationtoupdate = _qualityEntities.PartSpecifications
                .FirstOrDefault(x => x.SpecificationID == partspecification_.SpecificationID);

       
            partspecificationtoupdate.SequenceID = partspecification_.SequenceID;
        
            
            partspecificationtoupdate.LastEditBy = partspecification_.LastEditBy;
            partspecificationtoupdate.LastEditDate = DateTime.Now;
            _qualityEntities.SaveChanges(SaveOptions.None);

        }


        public void InsertClonedPartSpecification(PartSpecification partspecification_)
        {
            try
            {

                _qualityEntities.AddToPartSpecifications(partspecification_);
                _qualityEntities.SaveChanges(SaveOptions.None);
                _qualityEntities.Detach(partspecification_);

            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();

                //TODO:Log the error


            }

        }
      
      
      public int Insert(PartSpecification partspecification_)
        {

            try
            {
                var specificationtoinsert = new PartSpecification
                {
                    PartSetUpID = partspecification_.PartSetUpID,
                    SequenceID = partspecification_.SequenceID,
                    Characteristic=partspecification_.Characteristic,
                    CharacteristicES = partspecification_.CharacteristicES,
                    CharacteristicCN = partspecification_.CharacteristicCN,
                    unitID=partspecification_.unitID,
                    LowSpec = partspecification_.LowSpec,
                    LowSpecES = partspecification_.LowSpecES,
                    HighSpec = partspecification_.HighSpec,
                    HighSpecES = partspecification_.HighSpecES,
                    MeasurementMethodID = partspecification_.MeasurementMethodID,
                  
                    Gauges = partspecification_.Gauges,
                    GaugesES = partspecification_.GaugesES,
                    SampleSize=partspecification_.SampleSize,
                    SampleSizeES = partspecification_.SampleSizeES,
                    FrequencyID=partspecification_.FrequencyID,
                    OperationCode=partspecification_.OperationCode,
                    IsComponentCharacteristic=partspecification_.IsComponentCharacteristic,
                    IsMachineSetUpCharacteristic=partspecification_.IsMachineSetUpCharacteristic,
                    IsPartCharacteristic=partspecification_.IsPartCharacteristic,
                    IsDieSetUpCharcteristic=partspecification_.IsDieSetUpCharcteristic,
                    Notes = partspecification_.Notes,
                    IsActive = partspecification_.IsActive,
                    LastEditBy =partspecification_.LastEditBy,
                    LastEditDate = DateTime.Now
                };
                _qualityEntities.AddToPartSpecifications(specificationtoinsert);
                _qualityEntities.SaveChanges(SaveOptions.None);

                return specificationtoinsert.SpecificationID;


            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();
                return 0;
                //TODO:Log the error


            }

        }
    }
}