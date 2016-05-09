using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;
using TravelCard.DomainModel.Abstract;
using System.Data.Objects;

namespace TravelCard.DomainModel.Repositories
{
    public class PartSetUpRepository : IPartSetUpRepository
    {
        private Quality_devEntities _qualityEntities;
        private IQueryable<PartSetUp> _partsetup;
       


        public IQueryable<PartSetUp> PartSetUp
        {
            get { return _partsetup; }
        }
        
        
        public PartSetUpRepository(string connectionString)
        {

            _qualityEntities = new Quality_devEntities(connectionString);
            ObjectQuery<PartSetUp> partsetupQuery = _qualityEntities.PartSetUp;
            _partsetup = partsetupQuery;
        
        
        }

        public void UpdateAttachments(PartSetUp partsetup_)
        {
            var setuptoupdate = _qualityEntities.PartSetUp
                .FirstOrDefault(x => x.PartSetUpID == partsetup_.PartSetUpID);
            var drawingfile = partsetup_.DrawingFile != null || partsetup_.DrawingFile != "" ? partsetup_.DrawingFile : null;
            var deviationfile = partsetup_.DeviationFile != null || partsetup_.DeviationFile != "" ? partsetup_.DeviationFile : null;
            var deviationfile2 = partsetup_.DeviationFile2 != null || partsetup_.DeviationFile2 != "" ? partsetup_.DeviationFile2 : null;
            var diesetup = partsetup_.DieSetUpFile != null || partsetup_.DieSetUpFile != "" ? partsetup_.DieSetUpFile : null;
            var setupdrawing = partsetup_.DrawingFile != null || partsetup_.SetupDrawingFile != "" ? partsetup_.SetupDrawingFile : null;
            var qualityalertfile = partsetup_.QualityAlertFile != null || partsetup_.QualityAlertFile != "" ? partsetup_.QualityAlertFile : null;
            var qualityalertfile2 = partsetup_.QualityAlert2 != null || partsetup_.QualityAlert2 != "" ? partsetup_.QualityAlert2 : null;
            var redlightgreenlightfile = partsetup_.RedLightGreenLightFile != null || partsetup_.RedLightGreenLightFile != "" ? partsetup_.RedLightGreenLightFile : null;
            var additionalfile = partsetup_.AdditionalFile != null || partsetup_.AdditionalFile != "" ? partsetup_.AdditionalFile : null;

            if (drawingfile != null)
            {
                setuptoupdate.DrawingFile = drawingfile;
            }
            if (deviationfile != null)
            {

                var startdate = partsetup_.DeviationFileStartDte != null ? partsetup_.DeviationFileStartDte : null;
                var enddate = partsetup_.DeviationFileEndDte != null ? partsetup_.DeviationFileEndDte : null;
                setuptoupdate.DeviationFile = deviationfile;

                setuptoupdate.DeviationFileStartDte = startdate;
                setuptoupdate.DeviationFileEndDte = enddate;
            }

            if (deviationfile2 != null)
            {

                var startdate = partsetup_.DeviationFile2StartDte != null ? partsetup_.DeviationFile2StartDte : null;
                var enddate = partsetup_.DeviationFile2EndDte != null ? partsetup_.DeviationFile2EndDte : null;
                setuptoupdate.DeviationFile2 = deviationfile2;

                setuptoupdate.DeviationFile2StartDte = startdate;
                setuptoupdate.DeviationFile2EndDte = enddate;
            }
            if (diesetup != null)
            {
                setuptoupdate.DieSetUpFile = diesetup;
            }
            if (setupdrawing != null)
            {
                setuptoupdate.SetupDrawingFile =setupdrawing;
            }

            if (qualityalertfile != null)
            {
                setuptoupdate.QualityAlertFile =qualityalertfile;
                var startdate = partsetup_.QualityAlertStartDte != null ? partsetup_.QualityAlertStartDte : null;
                var enddate = partsetup_.QualityAlertEndDte != null ? partsetup_.QualityAlertEndDte : null;
                setuptoupdate.QualityAlertStartDte = startdate;
                setuptoupdate.QualityAlertEndDte = enddate;

            }


            if (qualityalertfile2 != null)
            {
                setuptoupdate.QualityAlert2 = qualityalertfile2;
                var startdate = partsetup_.QualityAlert2StartDte != null ? partsetup_.QualityAlert2StartDte : null;
                var enddate = partsetup_.QualityAlert2EndDte != null ? partsetup_.QualityAlert2EndDte : null;
                setuptoupdate.QualityAlert2StartDte = startdate;
                setuptoupdate.QualityAlert2EndDte = enddate;

            }

            if (redlightgreenlightfile != null)
            {
                setuptoupdate.RedLightGreenLightFile = partsetup_.RedLightGreenLightFile;
            }

            if (additionalfile != null)
            {
                setuptoupdate.AdditionalFile = partsetup_.AdditionalFile;
            }
            
            
            setuptoupdate.HasQualityAlert = partsetup_.HasQualityAlert;
            
           

            _qualityEntities.SaveChanges(SaveOptions.None);


        }
        public void UpdateDrawingFileEdits(PartSetUp partsetup_)
        {
            try
            {
                var setuptoupdate = _qualityEntities.PartSetUp
                            .FirstOrDefault(x => x.PartSetUpID == partsetup_.PartSetUpID);
                setuptoupdate.DrawingFile = partsetup_.DrawingFile;
                setuptoupdate.LastEditBy = partsetup_.LastEditBy;
                setuptoupdate.LastEditDate = partsetup_.LastEditDate;
               
                _qualityEntities.SaveChanges(SaveOptions.None);
                _qualityEntities.Detach(partsetup_);

            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();

                //TODO:Log the error


            }

        }

        public void UpdateQualityAlertFile1Edits(PartSetUp partsetup_)
        {
            try
            {
                var setuptoupdate = _qualityEntities.PartSetUp
                            .FirstOrDefault(x => x.PartSetUpID == partsetup_.PartSetUpID);
                setuptoupdate.QualityAlertFile = partsetup_.QualityAlertFile;
                setuptoupdate.QualityAlertStartDte = partsetup_.QualityAlertStartDte;
                setuptoupdate.QualityAlertEndDte = partsetup_.QualityAlertEndDte;
                setuptoupdate.LastEditBy = partsetup_.LastEditBy;
                setuptoupdate.LastEditDate = partsetup_.LastEditDate;

                _qualityEntities.SaveChanges(SaveOptions.None);
                _qualityEntities.Detach(partsetup_);

            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();

                //TODO:Log the error


            }

        }

        public void UpdateQualityAlertFile2Edits(PartSetUp partsetup_)
        {
            try
            {
                var setuptoupdate = _qualityEntities.PartSetUp
                            .FirstOrDefault(x => x.PartSetUpID == partsetup_.PartSetUpID);
                setuptoupdate.QualityAlert2 = partsetup_.QualityAlert2;
                setuptoupdate.QualityAlert2StartDte = partsetup_.QualityAlert2StartDte;
                setuptoupdate.QualityAlert2EndDte = partsetup_.QualityAlert2EndDte;
                setuptoupdate.LastEditBy = partsetup_.LastEditBy;
                setuptoupdate.LastEditDate = partsetup_.LastEditDate;

                _qualityEntities.SaveChanges(SaveOptions.None);
                _qualityEntities.Detach(partsetup_);

            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();

                //TODO:Log the error


            }

        }   




        public void UpdateDeviationFile1Edits(PartSetUp partsetup_)
        {
            try
            {
                var setuptoupdate = _qualityEntities.PartSetUp
                            .FirstOrDefault(x => x.PartSetUpID == partsetup_.PartSetUpID);
                setuptoupdate.DeviationFile = partsetup_.DeviationFile;
                setuptoupdate.DeviationFileStartDte = partsetup_.DeviationFileStartDte;
                setuptoupdate.DeviationFileEndDte = partsetup_.DeviationFileEndDte;
                setuptoupdate.LastEditBy = partsetup_.LastEditBy;
                setuptoupdate.LastEditDate = partsetup_.LastEditDate;

                _qualityEntities.SaveChanges(SaveOptions.None);
                _qualityEntities.Detach(partsetup_);

            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();

                //TODO:Log the error


            }

        }

        public void UpdateDeviationFile2Edits(PartSetUp partsetup_)
        {
            try
            {
                var setuptoupdate = _qualityEntities.PartSetUp
                            .FirstOrDefault(x => x.PartSetUpID == partsetup_.PartSetUpID);
                setuptoupdate.DeviationFile2 = partsetup_.DeviationFile2;
                setuptoupdate.DeviationFile2StartDte = partsetup_.DeviationFile2StartDte;
                setuptoupdate.DeviationFile2EndDte = partsetup_.DeviationFile2EndDte;
                setuptoupdate.LastEditBy = partsetup_.LastEditBy;
                setuptoupdate.LastEditDate = partsetup_.LastEditDate;

                _qualityEntities.SaveChanges(SaveOptions.None);
                _qualityEntities.Detach(partsetup_);

            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();

                //TODO:Log the error


            }

        }


        public void UpdateCommunicationNoteEdits(PartSetUp partsetup_)
        {
            try
            {
                var setuptoupdate = _qualityEntities.PartSetUp
                            .FirstOrDefault(x => x.PartSetUpID == partsetup_.PartSetUpID);
                setuptoupdate.CommunicationNote = partsetup_.CommunicationNote;
                setuptoupdate.LastEditBy = partsetup_.LastEditBy;
                setuptoupdate.LastEditDate = partsetup_.LastEditDate;

                _qualityEntities.SaveChanges(SaveOptions.None);
                _qualityEntities.Detach(partsetup_);

            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();

                //TODO:Log the error


            }

        }






        public void UpdateReleaseReadyEdits(PartSetUp partsetup_)
        {
            try
            {
           
                
                var setuptoupdate = _qualityEntities.PartSetUp
                            .FirstOrDefault(x => x.PartSetUpID == partsetup_.PartSetUpID);
                setuptoupdate.IsReleaseReady = partsetup_.IsReleaseReady;
                setuptoupdate.LastEditBy = partsetup_.LastEditBy;
                setuptoupdate.LastEditDate = partsetup_.LastEditDate;
                if (partsetup_.IsReleaseReady)
                {
                    setuptoupdate.LastApprovalDateTime = partsetup_.LastApprovalDateTime;
                    setuptoupdate.LastApprover = partsetup_.LastApprover;
                
                 }
              

                _qualityEntities.SaveChanges(SaveOptions.None);
                _qualityEntities.Detach(partsetup_);

            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();

                //TODO:Log the error


            }

        }



        public void Update(PartSetUp partsetup_)
        {
            var setuptoupdate = _qualityEntities.PartSetUp
                .FirstOrDefault(x => x.PartSetUpID == partsetup_.PartSetUpID);
                setuptoupdate.CategoryID = partsetup_.CategoryID;
                setuptoupdate.Revision = partsetup_.Revision;
                setuptoupdate.IsReleaseReady = partsetup_.IsReleaseReady;
                setuptoupdate.PartComment = partsetup_.PartComment;
                setuptoupdate.PartRemarks = partsetup_.PartRemarks;
                setuptoupdate.DrawingNumber = partsetup_.DrawingNumber;
                setuptoupdate.LastApprovalDateTime = partsetup_.LastApprovalDateTime;
                setuptoupdate.PackCode = partsetup_.PackCode;
                setuptoupdate.Notes = partsetup_.Notes;
                setuptoupdate.CommunicationNote = partsetup_.CommunicationNote;
                setuptoupdate.LastApprover = partsetup_.LastApprover;
                setuptoupdate.LastEditDate = partsetup_.LastEditDate;
                setuptoupdate.LastEditBy = partsetup_.LastEditBy;

                _qualityEntities.SaveChanges(SaveOptions.None);

       
        }

        public int Insert(PartSetUp partsetup_)
        {
            try
            {
                var partsetuptoinsert = new PartSetUp
                {
                    PartID = partsetup_.PartID,
                    CategoryID = partsetup_.CategoryID,
                    CreatedBy = partsetup_.CreatedBy,
                    CreateDate = partsetup_.CreateDate,
                    DrawingFile = partsetup_.DrawingFile,
                    DeviationFile = partsetup_.DeviationFile,
                    DieSetUpFile=partsetup_.DieSetUpFile,
                    Revision = partsetup_.Revision,
                    SetupDrawingFile = partsetup_.SetupDrawingFile,
                    HasQualityAlert = partsetup_.HasQualityAlert,
                    QualityAlertFile = partsetup_.QualityAlertFile,
                    RedLightGreenLightFile=partsetup_.RedLightGreenLightFile,
                    PackCode=partsetup_.PackCode,
                    IsReleaseReady = partsetup_.IsReleaseReady,
                    PartRemarks=partsetup_.PartRemarks,
                    PartComment=partsetup_.PartComment,
                    DrawingNumber=partsetup_.DrawingNumber,
                    LastApprovalDateTime = partsetup_.LastApprovalDateTime,
                    Notes=partsetup_.Notes,
                    CommunicationNote=partsetup_.CommunicationNote,
                    LastApprover = partsetup_.LastApprover,
                    LastEditDate = partsetup_.LastEditDate,
                    LastEditBy = partsetup_.LastEditBy

                };

                _qualityEntities.AddToPartSetUp(partsetuptoinsert);
                _qualityEntities.SaveChanges(true);
                return partsetuptoinsert.PartSetUpID;

            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();
                return 0;
            }

           
      
        }
    }





}
