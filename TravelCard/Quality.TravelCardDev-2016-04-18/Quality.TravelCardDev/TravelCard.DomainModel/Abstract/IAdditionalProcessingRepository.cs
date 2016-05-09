using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;

namespace TravelCard.DomainModel.Abstract
{
    public interface IAdditionalProcessingRepository
    {

        IQueryable<AdditionalProcessing> AdditionalProcess { get; }
        void Update(AdditionalProcessing additionalprocess_);
        int Insert(AdditionalProcessing additionalprocess_);
        void InsertClonedAdditionalProcess(Int16 partsetup_,string descEnglish_,string descSpanish_,string notes_, string sequenceid,string Lasteditby_, DateTime lastupdatedate_);
        void UpdateAdditionalProcessingSequenceOrder(AdditionalProcessing additionalprocess_);
          
           
    }
}
