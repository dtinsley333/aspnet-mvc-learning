using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;

namespace TravelCard.DomainModel.Abstract
{
    public interface IPartSetUpRepository
    {
     
        IQueryable<PartSetUp>PartSetUp{get;}
        void Update(PartSetUp partsetup_);
        void UpdateAttachments(PartSetUp partsetup_);
        void UpdateDrawingFileEdits(PartSetUp partsetup_);
        void UpdateQualityAlertFile1Edits(PartSetUp partsetup_); 
        void UpdateQualityAlertFile2Edits(PartSetUp partsetup_);
        void UpdateDeviationFile1Edits(PartSetUp partsetup_);
        void UpdateDeviationFile2Edits(PartSetUp partsetup_);
        void UpdateCommunicationNoteEdits(PartSetUp partsetup_);
        void UpdateReleaseReadyEdits(PartSetUp partsetup_);
        int Insert(PartSetUp partsetup_);
      
    }
}
