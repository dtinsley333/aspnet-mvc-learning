using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelCard.DomainModel.Abstract;
using TravelCard.DomainModel.Entities;

namespace Quality.ViewModels
{
    public class PartSetUpViewModel
    {

        public string ReturnUrl { get; set; }//url to return to after data is saved
        public TravelCard.DomainModel.Entities.PartSetUp PartSetUp { get; set; }
        public TravelCard.DomainModel.Entities.AdditionalProcessing AdditionalProcess { get; set; }
        public TravelCard.DomainModel.Entities.PartSpecification PartSpecification { get; set; }
        public TravelCard.DomainModel.Entities.TravelCard TravelCard { get; set; }
        public string UserMessage { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.PartSetUp> PartSetUps { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.PartCategory> PartCategories { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.AdditionalProcessing> AdditionalProcesses { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.PartSpecification> PartSpecifications { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.TravelCard> TravelCards { get; set; }
        public string DocType { get; set; }
        public string DocumentTitle { get; set; }
        public string DeviationFilePath { get; set; }
        public string DeviationFile2Path { get; set; }
        public string SetUpFilePath { get; set; }
        public string QualityAlertFilePath { get; set; }
        public string RedLightGreenLightFile { get; set; }
        public string AdditionalFile { get; set; }
        public string DrawingFilePath { get; set; }
        public string DieSetUpFilePath { get; set; }
        public string ItemID { get; set; }
        public string DrawingNumber{get;set;}
        public bool DrawingFileDelete { get; set; }
        public bool IsCurrentlyReleaseReady { get; set; }
        public bool SetUpFileDelete { get; set; }
        public bool QualityAlertFileDelete { get; set; }
        public bool RedLightGreenLightSetUpfileDelete { get; set; }
        public bool AdditionalFileDelete { get; set; }
        public bool DeviationFileDelete { get; set; }
        public bool DieSetUpFileDelete { get; set; }
        public string PartCategory { get; set; }
        public Int16 PartSetUpID { get; set; }
        public bool CanUserEdit { get; set; }
        public bool CanUserApprove { get; set; }
        public bool HasSearchCriteria { get; set; }
        public string Watermark { get; set; }

        public SelectList PartCategorySelectList
        {
            get {

                if (PartCategories != null)
                {
                    return new SelectList(PartCategories
                        .Where(a => a.IsActive)
                        .OrderBy(n => n.CategoryName),
                        "CategoryID", "CategoryName");
                }
                else
                {
                    return null;
                }
            
            
            
            }
        
        }


        public SelectList DrawingFilePathSelectList
        {
            get
            {

                if (PartSetUps != null)
                {
                    return new SelectList(PartSetUps
                    .Where(a => a.DrawingFile != null).OrderBy(n => n.DrawingFile),
                        "DrawingFile", "DrawingFile");
                }
                else
                {
                    return null;
                }



            }

        }

        public SelectList DeviationFile1PathSelectList
        {
            get
            {

                if (PartSetUps != null)
                {
                    return new SelectList(PartSetUps
                    .Where(a => a.DeviationFile != null).OrderBy(n => n.DeviationFile).Distinct(),
                        "DeviationFile", "DeviationFile");
                }
                else
                {
                    return null;
                }



            }

        }


        public SelectList DeviationFile2PathSelectList
        {
            get
            {

                if (PartSetUps != null)
                {
                    return new SelectList(PartSetUps
                    .Where(a => a.DeviationFile2 != null).OrderBy(n => n.DeviationFile2).Distinct(),
                        "DeviationFile2", "DeviationFile2");
                }
                else
                {
                    return null;
                }



            }

        }

        public SelectList QualityAlertFile1PathSelectList
        {
            get
            {

                if (PartSetUps != null)
                {
                    return new SelectList(PartSetUps
                    .Where(a => a.QualityAlertFile != null).OrderBy(n => n.QualityAlertFile).Distinct(),
                        "QualityAlertFile", "QualityAlertFile");
                }
                else
                {
                    return null;
                }



            }

        }
        public SelectList QualityAlertFile2PathSelectList
        {
            get
            {

                if (PartSetUps != null)
                {
                    return new SelectList(PartSetUps
                    .Where(a => a.QualityAlert2 != null).OrderBy(n => n.QualityAlert2).Distinct(),
                        "QualityAlert2", "QualityAlert2");
                }
                else
                {
                    return null;
                }



            }

        }




    }
}