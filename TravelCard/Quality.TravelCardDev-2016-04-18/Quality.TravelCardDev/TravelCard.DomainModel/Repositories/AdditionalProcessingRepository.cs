using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;
using TravelCard.DomainModel.Abstract;
using System.Data.Objects;

namespace TravelCard.DomainModel.Repositories
{
   
   
   public class AdditionalProcessingRepository : IAdditionalProcessingRepository
    {
        private Quality_devEntities _qualityEntities;
        private IQueryable<AdditionalProcessing> _additionalprocessing;

        public AdditionalProcessingRepository(string connectionString)
        {
            _qualityEntities = new Quality_devEntities(connectionString);
            ObjectQuery<AdditionalProcessing> additionalprocessingQuery = _qualityEntities.AdditionalProcessings;
            _additionalprocessing = additionalprocessingQuery;
        
        }


       public IQueryable<AdditionalProcessing> AdditionalProcess
        {
            get { return _additionalprocessing; }
        }

       public void Update(AdditionalProcessing additionalprocess_)
        {
        
           var processtoupdate = _qualityEntities.AdditionalProcessings
                .FirstOrDefault(x => x.ProcessingID == additionalprocess_.ProcessingID);

          //  processtoupdate.PartSetUpID = additionalprocess_.PartSetUpID;
            processtoupdate.Notes = additionalprocess_.Notes;
            processtoupdate.SequenceID = additionalprocess_.SequenceID;
            processtoupdate.IsActive = additionalprocess_.IsActive;
            processtoupdate.Description = additionalprocess_.Description;
            processtoupdate.DescriptionES = additionalprocess_.DescriptionES;
            processtoupdate.DescriptionCN = additionalprocess_.DescriptionCN;
            processtoupdate.LastEditedBy = additionalprocess_.LastEditedBy;
            processtoupdate.LastEditDate = additionalprocess_.LastEditDate;
            _qualityEntities.SaveChanges(SaveOptions.None);

        }

       public void UpdateAdditionalProcessingSequenceOrder(AdditionalProcessing additionalprocess_)
       {
           var additionalprocesstoupdate = _qualityEntities.AdditionalProcessings
               .FirstOrDefault(x => x.ProcessingID == additionalprocess_.ProcessingID);

           additionalprocesstoupdate.SequenceID = additionalprocess_.SequenceID;
           additionalprocesstoupdate.LastEditedBy = additionalprocess_.LastEditedBy;
           additionalprocesstoupdate.LastEditDate = DateTime.Now;
           _qualityEntities.SaveChanges(SaveOptions.None);

       }







       public void InsertClonedAdditionalProcess(Int16 partsetupid_,string descEnglish_,string descSpanish_, string notes_,string sequenceid_,string lastupdatedby_,DateTime lastupdateddate_)
        
 
       {
           try
           {
               
                   AdditionalProcessing processtoinsert = new AdditionalProcessing
                   {

                       PartSetUpID = partsetupid_,
                       Notes = notes_,
                       SequenceID =sequenceid_,
                       IsActive = true,

                       Description = descEnglish_,
                       DescriptionES = descSpanish_,
                       LastEditDate = lastupdateddate_,
                       LastEditedBy = lastupdatedby_

                   };
                 
                   _qualityEntities.AddToAdditionalProcessings(processtoinsert);


                   _qualityEntities.SaveChanges(SaveOptions.None);
                   _qualityEntities.Detach(processtoinsert);
               

          
           }
           catch (Exception ex)
           {
               string errormessage = ex.ToString();

               //TODO:Log the error


           }

       }



       public int Insert(AdditionalProcessing additionalprocess_)
        {
            try
            {
                var processtoinsert = new AdditionalProcessing
                {
                   PartSetUpID=additionalprocess_.PartSetUpID,
                   Notes=additionalprocess_.Notes,
                   SequenceID=additionalprocess_.SequenceID,
                   IsActive=additionalprocess_.IsActive,
                   Description=additionalprocess_.Description,
                   DescriptionES=additionalprocess_.DescriptionES,
                   DescriptionCN = additionalprocess_.DescriptionCN,
                   LastEditDate=additionalprocess_.LastEditDate,
                   LastEditedBy=additionalprocess_.LastEditedBy
 
                };
           
                _qualityEntities.AddToAdditionalProcessings(processtoinsert);

              
                _qualityEntities.SaveChanges(SaveOptions.None);
              //  _qualityEntities.Detach(additionalprocess_);
                return processtoinsert.ProcessingID;

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
