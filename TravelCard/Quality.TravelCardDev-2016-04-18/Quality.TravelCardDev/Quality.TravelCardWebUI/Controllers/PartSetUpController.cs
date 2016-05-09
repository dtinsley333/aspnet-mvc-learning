using System;
using System.Collections.Generic;
using Quality.WebUI.Controllers;
using Quality.ViewModels;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using TravelCard.DomainModel.Repositories;
using TravelCard.DomainModel.Abstract;
using TravelCard.DomainModel.Entities;
using System.Configuration;
using System.IO;



namespace Quality.Controllers
{
    public class PartSetUpController : BaseController
    {

        IPartSetUpRepository _partsetupRepository;
        IPartCategoryRepository _partCategoryRepository;
        IAdditionalProcessingRepository _additionalprocessingRepository;
        IPartSpecificationRepository _partspecificationRepository;
        ITravelCardRepository _travelcardRepository;
        

        public PartSetUpController(IPartSetUpRepository partsetupRespository_,IPartCategoryRepository partcategoryRepository_,IAdditionalProcessingRepository additionalprocessingRepository_,IPartSpecificationRepository partspecificationRepository_,ITravelCardRepository travelcardRepository_)
        {
            _partsetupRepository = partsetupRespository_;
            _partCategoryRepository = partcategoryRepository_;
            _additionalprocessingRepository=additionalprocessingRepository_;
            _partspecificationRepository = partspecificationRepository_;
            _travelcardRepository = travelcardRepository_;
        }

        [HttpGet]
        public ActionResult PartSetUpGlobalReleaseReadyEditor()
        {
            bool usercanedit = CanUserEdit();
            bool canuserapprove = CanUserApprove();
            var viewModel = new PartSetUpViewModel
            {
                PartSetUps = _partsetupRepository.PartSetUp,
                AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
                PartSpecifications = _partspecificationRepository.PartSpecification,
                TravelCards = _travelcardRepository.TravelCard,
                PartCategories = _partCategoryRepository.PartCategory,
                IsCurrentlyReleaseReady=true,
                CanUserEdit = usercanedit,
                HasSearchCriteria = false,
                DrawingFileDelete = false

            };

            return View("PartSetUpGlobalReleaseReadyEditor", viewModel);
        }
      
        [HttpPost]
        public ActionResult PartSetUpGlobalReleaseReadyEditor(PartSetUpViewModel partsetup_)
        {
        bool usercanedit = CanUserEdit();




            if (ModelState["PartSetUp.CategoryID"] != null)
                ModelState["PartSetUp.CategoryID"].Errors.Clear();

            if (ModelState["PartSetUp.PartID"] != null)
                ModelState["PartSetUp.PartID"].Errors.Clear();
            if (ModelState["PartSetUp.PackCode"] != null)
                ModelState["PartSetUp.PackCode"].Errors.Clear();

            if (ModelState["PartSetUp.DrawingNumber"] != null)
                ModelState["PartSetUp.DrawingNumber"].Errors.Clear();

            if (ModelState["PartSetUp.Revision"] != null)
                ModelState["PartSetUp.Revision"].Errors.Clear();
            if (ModelState["PartSetUp.IsReleaseReady"] != null)
                ModelState["PartSetUp.IsReleaseReady"].Errors.Clear();

            if (ModelState["PartSetUp.PartSetUpID"] != null)
                ModelState["PartSetUp.PartSetUpID"].Errors.Clear();

            if (ModelState["PartSetUp.DrawingFile"] != null)
                ModelState["PartSetUp.DrawingFile"].Errors.Clear();

            var viewModel = new PartSetUpViewModel
            {
                PartSetUps = _partsetupRepository.PartSetUp,
                AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
                PartSpecifications = _partspecificationRepository.PartSpecification,
                TravelCards = _travelcardRepository.TravelCard,
                PartCategories = _partCategoryRepository.PartCategory,
                CanUserEdit = usercanedit,
                HasSearchCriteria = false
            };


            if (partsetup_.PartSetUp.PartID != null)
            {

                string partidsearchcriteria = partsetup_.PartSetUp.PartID.Trim();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartID.Contains(partidsearchcriteria));
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.PartSetUpID != 0)
            {

                int setupidsearchcriteria = partsetup_.PartSetUp.PartSetUpID;
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartSetUpID == setupidsearchcriteria);
                viewModel.HasSearchCriteria = true;
            }


            int? categoryidsearch = partsetup_.PartSetUp.CategoryID;
            if (categoryidsearch != 0)
            {

                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.CategoryID == categoryidsearch);
                viewModel.HasSearchCriteria = true;


            }

            if (partsetup_.PartSetUp.PackCode != null)
            {
                string packcodesearch = partsetup_.PartSetUp.PackCode.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PackCode == packcodesearch);
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.DrawingFile != null)
            {
                string drawingfilesearch = partsetup_.PartSetUp.DrawingFile.Trim();
            
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingFile== drawingfilesearch.Replace("//","/"));
                viewModel.HasSearchCriteria = true;
            }


            if (partsetup_.PartSetUp.DrawingNumber != null)
            {
                string drawingnumbersearch = partsetup_.PartSetUp.DrawingNumber.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingNumber == drawingnumbersearch);
                viewModel.HasSearchCriteria = true;
            }
            if (partsetup_.PartSetUp.Revision != null)
            {
                string revisionsearch = partsetup_.PartSetUp.Revision.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(c => c.Revision == revisionsearch);
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.IsReleaseReady)
            {
                bool releasereadysearch;
                releasereadysearch = partsetup_.PartSetUp.IsReleaseReady;
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.IsReleaseReady == true);
                viewModel.HasSearchCriteria = true;
            }


            return View("PartSetUpGlobalReleaseReadyEditor", viewModel);
        }

        [HttpGet]
        public ActionResult PartSetUpGlobalDeviation2FileEditor()
        {
            bool usercanedit = CanUserEdit();
            var viewModel = new PartSetUpViewModel
            {
                PartSetUps = _partsetupRepository.PartSetUp,

                PartSpecifications = _partspecificationRepository.PartSpecification,

                PartCategories = _partCategoryRepository.PartCategory,
                CanUserEdit = usercanedit,
                HasSearchCriteria = false,
                DeviationFileDelete = false

            };




            return View("PartSetUpGlobalDeviation2FileEditor", viewModel);
        }



        [HttpGet]
       public ActionResult PartSetUpGlobalDeviationFileEditor()
        {
            bool usercanedit = CanUserEdit();
            var viewModel = new PartSetUpViewModel
            {
                PartSetUps = _partsetupRepository.PartSetUp,
               
                PartSpecifications = _partspecificationRepository.PartSpecification,
               
                PartCategories = _partCategoryRepository.PartCategory,
                CanUserEdit = usercanedit,
                HasSearchCriteria = false,
                DeviationFileDelete = false

            };
            return View("PartSetUpGlobalDeviationFileEditor", viewModel);
        }


        [HttpGet]
        public ActionResult PartSetUpGlobalQualityAlertFileEditor()
        {
            bool usercanedit = CanUserEdit();
            var viewModel = new PartSetUpViewModel
            {
                PartSetUps = _partsetupRepository.PartSetUp,

                PartSpecifications = _partspecificationRepository.PartSpecification,

                PartCategories = _partCategoryRepository.PartCategory,
                CanUserEdit = usercanedit,
                HasSearchCriteria = false,
                QualityAlertFileDelete = false

            };
            return View("PartSetUpGlobalQualityAlertFileEditor", viewModel);
        }

        [HttpPost]
        public ActionResult PartSetUpGlobalQualityAlertFile2Update(PartSetUpViewModel partsetup_)
        {

            bool usercanedit = CanUserEdit();


            if (ModelState["PartSetUp.CategoryID"] != null)
                ModelState["PartSetUp.CategoryID"].Errors.Clear();

            if (ModelState["PartSetUp.PartID"] != null)
                ModelState["PartSetUp.PartID"].Errors.Clear();
            if (ModelState["PartSetUp.PackCode"] != null)
                ModelState["PartSetUp.PackCode"].Errors.Clear();

            if (ModelState["PartSetUp.DrawingNumber"] != null)
                ModelState["PartSetUp.DrawingNumber"].Errors.Clear();

            if (ModelState["PartSetUp.Revision"] != null)
                ModelState["PartSetUp.Revision"].Errors.Clear();
            if (ModelState["PartSetUp.IsReleaseReady"] != null)
                ModelState["PartSetUp.IsReleaseReady"].Errors.Clear();

            if (ModelState["PartSetUp.PartSetUpID"] != null)
                ModelState["PartSetUp.PartSetUpID"].Errors.Clear();

            if (ModelState["PartSetUp.DrawingFile"] != null)
                ModelState["PartSetUp.DrawingFile"].Errors.Clear();

            var viewModel = new PartSetUpViewModel
            {
                PartSetUps = _partsetupRepository.PartSetUp,
                AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
                PartSpecifications = _partspecificationRepository.PartSpecification,
                TravelCards = _travelcardRepository.TravelCard,
                PartCategories = _partCategoryRepository.PartCategory,
                CanUserEdit = usercanedit,
                HasSearchCriteria = false
            };


            if (partsetup_.PartSetUp.PartID != null)
            {

                string partidsearchcriteria = partsetup_.PartSetUp.PartID.Trim();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartID.Contains(partidsearchcriteria));
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.PartSetUpID != 0)
            {

                int setupidsearchcriteria = partsetup_.PartSetUp.PartSetUpID;
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartSetUpID == setupidsearchcriteria);
                viewModel.HasSearchCriteria = true;
            }


            int? categoryidsearch = partsetup_.PartSetUp.CategoryID;
            if (categoryidsearch != 0)
            {

                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.CategoryID == categoryidsearch);
                viewModel.HasSearchCriteria = true;


            }

            if (partsetup_.PartSetUp.PackCode != null)
            {
                string packcodesearch = partsetup_.PartSetUp.PackCode.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PackCode == packcodesearch);
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.QualityAlert2 != null)
            {
                string qualityalertfile2search = partsetup_.PartSetUp.QualityAlert2.Trim();

                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.QualityAlert2 == qualityalertfile2search.Replace("//", "/"));
                viewModel.HasSearchCriteria = true;
            }


            if (partsetup_.PartSetUp.DrawingNumber != null)
            {
                string drawingnumbersearch = partsetup_.PartSetUp.DrawingNumber.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingNumber == drawingnumbersearch);
                viewModel.HasSearchCriteria = true;
            }
            if (partsetup_.PartSetUp.Revision != null)
            {
                string revisionsearch = partsetup_.PartSetUp.Revision.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(c => c.Revision == revisionsearch);
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.IsReleaseReady)
            {
                bool releasereadysearch;
                releasereadysearch = partsetup_.PartSetUp.IsReleaseReady;
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.IsReleaseReady == true);
                viewModel.HasSearchCriteria = true;
            }

            return View("PartSetUpGlobalQualityAlertFile2Editor", viewModel);

        }
        
        
        
        
        
        [HttpGet]
        public ActionResult PartSetUpGlobalQualityAlertFile2Editor()
        {
            bool usercanedit = CanUserEdit();
            var viewModel = new PartSetUpViewModel
            {
                PartSetUps = _partsetupRepository.PartSetUp,

                PartSpecifications = _partspecificationRepository.PartSpecification,

                PartCategories = _partCategoryRepository.PartCategory,
                CanUserEdit = usercanedit,
                HasSearchCriteria = false,
                QualityAlertFileDelete = false

            };
            return View("PartSetUpGlobalQualityAlertFile2Editor", viewModel);
        }


        [HttpPost]
        public ActionResult PartSetUpGlobalQualityAlertFile1Update(PartSetUpViewModel partsetup_) 
     
        {

            bool usercanedit = CanUserEdit();


            if (ModelState["PartSetUp.CategoryID"] != null)
                ModelState["PartSetUp.CategoryID"].Errors.Clear();

            if (ModelState["PartSetUp.PartID"] != null)
                ModelState["PartSetUp.PartID"].Errors.Clear();
            if (ModelState["PartSetUp.PackCode"] != null)
                ModelState["PartSetUp.PackCode"].Errors.Clear();

            if (ModelState["PartSetUp.DrawingNumber"] != null)
                ModelState["PartSetUp.DrawingNumber"].Errors.Clear();

            if (ModelState["PartSetUp.Revision"] != null)
                ModelState["PartSetUp.Revision"].Errors.Clear();
            if (ModelState["PartSetUp.IsReleaseReady"] != null)
                ModelState["PartSetUp.IsReleaseReady"].Errors.Clear();

            if (ModelState["PartSetUp.PartSetUpID"] != null)
                ModelState["PartSetUp.PartSetUpID"].Errors.Clear();

            if (ModelState["PartSetUp.DrawingFile"] != null)
                ModelState["PartSetUp.DrawingFile"].Errors.Clear();

            var viewModel = new PartSetUpViewModel
            {
                PartSetUps = _partsetupRepository.PartSetUp,
                AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
                PartSpecifications = _partspecificationRepository.PartSpecification,
                TravelCards = _travelcardRepository.TravelCard,
                PartCategories = _partCategoryRepository.PartCategory,
                CanUserEdit = usercanedit,
                HasSearchCriteria = false
            };


            if (partsetup_.PartSetUp.PartID != null)
            {

                string partidsearchcriteria = partsetup_.PartSetUp.PartID.Trim();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartID.Contains(partidsearchcriteria));
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.PartSetUpID != 0)
            {

                int setupidsearchcriteria = partsetup_.PartSetUp.PartSetUpID;
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartSetUpID == setupidsearchcriteria);
                viewModel.HasSearchCriteria = true;
            }


            int? categoryidsearch = partsetup_.PartSetUp.CategoryID;
            if (categoryidsearch != 0)
            {

                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.CategoryID == categoryidsearch);
                viewModel.HasSearchCriteria = true;


            }

            if (partsetup_.PartSetUp.PackCode != null)
            {
                string packcodesearch = partsetup_.PartSetUp.PackCode.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PackCode == packcodesearch);
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.QualityAlertFile != null)
            {
                string qualityalertfile1search = partsetup_.PartSetUp.QualityAlertFile.Trim();

                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.QualityAlertFile == qualityalertfile1search.Replace("//", "/"));
                viewModel.HasSearchCriteria = true;
            }


            if (partsetup_.PartSetUp.DrawingNumber != null)
            {
                string drawingnumbersearch = partsetup_.PartSetUp.DrawingNumber.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingNumber == drawingnumbersearch);
                viewModel.HasSearchCriteria = true;
            }
            if (partsetup_.PartSetUp.Revision != null)
            {
                string revisionsearch = partsetup_.PartSetUp.Revision.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(c => c.Revision == revisionsearch);
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.IsReleaseReady)
            {
                bool releasereadysearch;
                releasereadysearch = partsetup_.PartSetUp.IsReleaseReady;
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.IsReleaseReady == true);
                viewModel.HasSearchCriteria = true;
            }

            return View("PartSetUpGlobalQualityAlertFileEditor", viewModel);

        }


        [HttpGet]
        public ActionResult PartSetUpGlobalDrawingFileEditor()
        {
            bool usercanedit = CanUserEdit();
            var viewModel = new PartSetUpViewModel
            {
                PartSetUps = _partsetupRepository.PartSetUp,
                AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
                PartSpecifications = _partspecificationRepository.PartSpecification,
                TravelCards = _travelcardRepository.TravelCard,
                PartCategories = _partCategoryRepository.PartCategory,
                CanUserEdit = usercanedit,
                HasSearchCriteria = false,
                DrawingFileDelete=false
           
            };

            return View("PartSetUpGlobalDrawingFileEditor", viewModel);
        }

        [HttpPost]
        public ActionResult PartSetUpGlobalDeviationFileUpdate(PartSetUpViewModel partsetup_)
        {
            bool usercanedit = CanUserEdit();


            if (ModelState["PartSetUp.CategoryID"] != null)
                ModelState["PartSetUp.CategoryID"].Errors.Clear();

            if (ModelState["PartSetUp.PartID"] != null)
                ModelState["PartSetUp.PartID"].Errors.Clear();
            if (ModelState["PartSetUp.PackCode"] != null)
                ModelState["PartSetUp.PackCode"].Errors.Clear();

            if (ModelState["PartSetUp.DrawingNumber"] != null)
                ModelState["PartSetUp.DrawingNumber"].Errors.Clear();

            if (ModelState["PartSetUp.Revision"] != null)
                ModelState["PartSetUp.Revision"].Errors.Clear();
            if (ModelState["PartSetUp.IsReleaseReady"] != null)
                ModelState["PartSetUp.IsReleaseReady"].Errors.Clear();

            if (ModelState["PartSetUp.PartSetUpID"] != null)
                ModelState["PartSetUp.PartSetUpID"].Errors.Clear();

            if (ModelState["PartSetUp.DrawingFile"] != null)
                ModelState["PartSetUp.DrawingFile"].Errors.Clear();

            var viewModel = new PartSetUpViewModel
            {
                PartSetUps = _partsetupRepository.PartSetUp,
                AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
                PartSpecifications = _partspecificationRepository.PartSpecification,
                TravelCards = _travelcardRepository.TravelCard,
                PartCategories = _partCategoryRepository.PartCategory,
                CanUserEdit = usercanedit,
                HasSearchCriteria = false
            };


            if (partsetup_.PartSetUp.PartID != null)
            {

                string partidsearchcriteria = partsetup_.PartSetUp.PartID.Trim();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartID.Contains(partidsearchcriteria));
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.PartSetUpID != 0)
            {

                int setupidsearchcriteria = partsetup_.PartSetUp.PartSetUpID;
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartSetUpID == setupidsearchcriteria);
                viewModel.HasSearchCriteria = true;
            }


            int? categoryidsearch = partsetup_.PartSetUp.CategoryID;
            if (categoryidsearch != 0)
            {

                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.CategoryID == categoryidsearch);
                viewModel.HasSearchCriteria = true;


            }

            if (partsetup_.PartSetUp.PackCode != null)
            {
                string packcodesearch = partsetup_.PartSetUp.PackCode.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PackCode == packcodesearch);
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.DeviationFile != null)
            {
                string deviationfilesearch = partsetup_.PartSetUp.DeviationFile.Trim();

                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DeviationFile == deviationfilesearch.Replace("//", "/"));
                viewModel.HasSearchCriteria = true;
            }


            if (partsetup_.PartSetUp.DrawingNumber != null)
            {
                string drawingnumbersearch = partsetup_.PartSetUp.DrawingNumber.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingNumber == drawingnumbersearch);
                viewModel.HasSearchCriteria = true;
            }
            if (partsetup_.PartSetUp.Revision != null)
            {
                string revisionsearch = partsetup_.PartSetUp.Revision.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(c => c.Revision == revisionsearch);
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.IsReleaseReady)
            {
                bool releasereadysearch;
                releasereadysearch = partsetup_.PartSetUp.IsReleaseReady;
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.IsReleaseReady == true);
                viewModel.HasSearchCriteria = true;
            }


            return View("PartSetUpGlobalDeviationFileEditor", viewModel);
        }


        [HttpPost]
        public ActionResult PartSetUpGlobalDeviationFile2Update(PartSetUpViewModel partsetup_)
        {
            bool usercanedit = CanUserEdit();




            if (ModelState["PartSetUp.CategoryID"] != null)
                ModelState["PartSetUp.CategoryID"].Errors.Clear();

            if (ModelState["PartSetUp.PartID"] != null)
                ModelState["PartSetUp.PartID"].Errors.Clear();
            if (ModelState["PartSetUp.PackCode"] != null)
                ModelState["PartSetUp.PackCode"].Errors.Clear();

            if (ModelState["PartSetUp.DrawingNumber"] != null)
                ModelState["PartSetUp.DrawingNumber"].Errors.Clear();

            if (ModelState["PartSetUp.Revision"] != null)
                ModelState["PartSetUp.Revision"].Errors.Clear();
            if (ModelState["PartSetUp.IsReleaseReady"] != null)
                ModelState["PartSetUp.IsReleaseReady"].Errors.Clear();

            if (ModelState["PartSetUp.PartSetUpID"] != null)
                ModelState["PartSetUp.PartSetUpID"].Errors.Clear();

            if (ModelState["PartSetUp.DrawingFile"] != null)
                ModelState["PartSetUp.DrawingFile"].Errors.Clear();

            var viewModel = new PartSetUpViewModel
            {
                PartSetUps = _partsetupRepository.PartSetUp,
                AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
                PartSpecifications = _partspecificationRepository.PartSpecification,
                TravelCards = _travelcardRepository.TravelCard,
                PartCategories = _partCategoryRepository.PartCategory,
                CanUserEdit = usercanedit,
                HasSearchCriteria = false
            };


            if (partsetup_.PartSetUp.PartID != null)
            {

                string partidsearchcriteria = partsetup_.PartSetUp.PartID.Trim();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartID.Contains(partidsearchcriteria));
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.PartSetUpID != 0)
            {

                int setupidsearchcriteria = partsetup_.PartSetUp.PartSetUpID;
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartSetUpID == setupidsearchcriteria);
                viewModel.HasSearchCriteria = true;
            }


            int? categoryidsearch = partsetup_.PartSetUp.CategoryID;
            if (categoryidsearch != 0)
            {

                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.CategoryID == categoryidsearch);
                viewModel.HasSearchCriteria = true;


            }

            if (partsetup_.PartSetUp.PackCode != null)
            {
                string packcodesearch = partsetup_.PartSetUp.PackCode.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PackCode == packcodesearch);
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.DeviationFile2 != null)
            {
                string deviationfilesearch = partsetup_.PartSetUp.DeviationFile2.Trim();

                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DeviationFile2 == deviationfilesearch.Replace("//", "/"));
                viewModel.HasSearchCriteria = true;
            }


            if (partsetup_.PartSetUp.DrawingNumber != null)
            {
                string drawingnumbersearch = partsetup_.PartSetUp.DrawingNumber.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingNumber == drawingnumbersearch);
                viewModel.HasSearchCriteria = true;
            }
            if (partsetup_.PartSetUp.Revision != null)
            {
                string revisionsearch = partsetup_.PartSetUp.Revision.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(c => c.Revision == revisionsearch);
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.IsReleaseReady)
            {
                bool releasereadysearch;
                releasereadysearch = partsetup_.PartSetUp.IsReleaseReady;
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.IsReleaseReady == true);
                viewModel.HasSearchCriteria = true;
            }


            return View("PartSetUpGlobalDeviation2FileEditor", viewModel);
        }



        [HttpPost]
        public ActionResult PartSetUpGlobalDrawingFileUpdate(PartSetUpViewModel partsetup_)
        {
            bool usercanedit = CanUserEdit();




            if (ModelState["PartSetUp.CategoryID"] != null)
                ModelState["PartSetUp.CategoryID"].Errors.Clear();

            if (ModelState["PartSetUp.PartID"] != null)
                ModelState["PartSetUp.PartID"].Errors.Clear();
            if (ModelState["PartSetUp.PackCode"] != null)
                ModelState["PartSetUp.PackCode"].Errors.Clear();

            if (ModelState["PartSetUp.DrawingNumber"] != null)
                ModelState["PartSetUp.DrawingNumber"].Errors.Clear();

            if (ModelState["PartSetUp.Revision"] != null)
                ModelState["PartSetUp.Revision"].Errors.Clear();
            if (ModelState["PartSetUp.IsReleaseReady"] != null)
                ModelState["PartSetUp.IsReleaseReady"].Errors.Clear();

            if (ModelState["PartSetUp.PartSetUpID"] != null)
                ModelState["PartSetUp.PartSetUpID"].Errors.Clear();

            if (ModelState["PartSetUp.DrawingFile"] != null)
                ModelState["PartSetUp.DrawingFile"].Errors.Clear();

            var viewModel = new PartSetUpViewModel
            {
                PartSetUps = _partsetupRepository.PartSetUp,
                AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
                PartSpecifications = _partspecificationRepository.PartSpecification,
                TravelCards = _travelcardRepository.TravelCard,
                PartCategories = _partCategoryRepository.PartCategory,
                CanUserEdit = usercanedit,
                HasSearchCriteria = false
            };


            if (partsetup_.PartSetUp.PartID != null)
            {

                string partidsearchcriteria = partsetup_.PartSetUp.PartID.Trim();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartID.Contains(partidsearchcriteria));
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.PartSetUpID != 0)
            {

                int setupidsearchcriteria = partsetup_.PartSetUp.PartSetUpID;
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartSetUpID == setupidsearchcriteria);
                viewModel.HasSearchCriteria = true;
            }


            int? categoryidsearch = partsetup_.PartSetUp.CategoryID;
            if (categoryidsearch != 0)
            {

                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.CategoryID == categoryidsearch);
                viewModel.HasSearchCriteria = true;


            }

            if (partsetup_.PartSetUp.PackCode != null)
            {
                string packcodesearch = partsetup_.PartSetUp.PackCode.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PackCode == packcodesearch);
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.DrawingFile != null)
            {
                string drawingfilesearch = partsetup_.PartSetUp.DrawingFile.Trim();
            
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingFile== drawingfilesearch.Replace("//","/"));
                viewModel.HasSearchCriteria = true;
            }


            if (partsetup_.PartSetUp.DrawingNumber != null)
            {
                string drawingnumbersearch = partsetup_.PartSetUp.DrawingNumber.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingNumber == drawingnumbersearch);
                viewModel.HasSearchCriteria = true;
            }
            if (partsetup_.PartSetUp.Revision != null)
            {
                string revisionsearch = partsetup_.PartSetUp.Revision.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(c => c.Revision == revisionsearch);
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.IsReleaseReady)
            {
                bool releasereadysearch;
                releasereadysearch = partsetup_.PartSetUp.IsReleaseReady;
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.IsReleaseReady == true);
                viewModel.HasSearchCriteria = true;
            }


            return View("PartSetUpGlobalDrawingFileEditor", viewModel);
        }

        [HttpGet]
        public ActionResult PartSetUpGlobalRemarkEditor()
        {
            bool usercanedit = CanUserEdit();
            bool canuserapprove = CanUserApprove();
            var viewModel = new PartSetUpViewModel
            {
                PartSetUps = _partsetupRepository.PartSetUp,
              //  AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
               // PartSpecifications = _partspecificationRepository.PartSpecification,
               // TravelCards = _travelcardRepository.TravelCard,
                PartCategories = _partCategoryRepository.PartCategory,
               // IsCurrentlyReleaseReady = true,
                CanUserEdit = usercanedit,
                CanUserApprove=UserCanApprove,
                Watermark=String.Empty
               

            };

            return View("PartSetUpGlobalRemarkEditor", viewModel);
        }
        [HttpPost]
        public ActionResult PartSetUpGlobalRemarkEditor(PartSetUpViewModel partsetup_)
        {
            bool usercanedit = CanUserEdit();

            if (ModelState["PartSetUp.CategoryID"] != null)
                ModelState["PartSetUp.CategoryID"].Errors.Clear();

            if (ModelState["PartSetUp.PartID"] != null)
                ModelState["PartSetUp.PartID"].Errors.Clear();
            if (ModelState["PartSetUp.PackCode"] != null)
                ModelState["PartSetUp.PackCode"].Errors.Clear();

            if (ModelState["PartSetUp.DrawingNumber"] != null)
                ModelState["PartSetUp.DrawingNumber"].Errors.Clear();

            if (ModelState["PartSetUp.Revision"] != null)
                ModelState["PartSetUp.Revision"].Errors.Clear();
            if (ModelState["PartSetUp.IsReleaseReady"] != null)
                ModelState["PartSetUp.IsReleaseReady"].Errors.Clear();

            if (ModelState["PartSetUp.PartSetUpID"] != null)
                ModelState["PartSetUp.PartSetUpID"].Errors.Clear();

            if (ModelState["PartSetUp.DrawingFile"] != null)
                ModelState["PartSetUp.DrawingFile"].Errors.Clear();

            var viewModel = new PartSetUpViewModel
            {
                PartSetUps = _partsetupRepository.PartSetUp,
                AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
                PartSpecifications = _partspecificationRepository.PartSpecification,
                TravelCards = _travelcardRepository.TravelCard,
                PartCategories = _partCategoryRepository.PartCategory,
                CanUserEdit = usercanedit,
                HasSearchCriteria = false
            };


            if (partsetup_.PartSetUp.PartID != null)
            {

                string partidsearchcriteria = partsetup_.PartSetUp.PartID.Trim();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartID.Contains(partidsearchcriteria));
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.PartSetUpID != 0)
            {

                int setupidsearchcriteria = partsetup_.PartSetUp.PartSetUpID;
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartSetUpID == setupidsearchcriteria);
                viewModel.HasSearchCriteria = true;
            }


            int? categoryidsearch = partsetup_.PartSetUp.CategoryID;
            if (categoryidsearch != 0)
            {

                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.CategoryID == categoryidsearch);
                viewModel.HasSearchCriteria = true;


            }

            if (partsetup_.PartSetUp.PackCode != null)
            {
                string packcodesearch = partsetup_.PartSetUp.PackCode.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PackCode == packcodesearch);
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.DrawingFile != null)
            {
              string drawingfilesearch = partsetup_.PartSetUp.DrawingFile.Trim();
              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingFile == drawingfilesearch.Replace("//", "/"));
              viewModel.HasSearchCriteria = true;
            }


            if (partsetup_.PartSetUp.DrawingNumber != null)
            {
                string drawingnumbersearch = partsetup_.PartSetUp.DrawingNumber.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingNumber == drawingnumbersearch);
                viewModel.HasSearchCriteria = true;
            }
            if (partsetup_.PartSetUp.Revision != null)
            {
                string revisionsearch = partsetup_.PartSetUp.Revision.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(c => c.Revision == revisionsearch);
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.IsReleaseReady)
            {
                bool releasereadysearch;
                releasereadysearch = partsetup_.PartSetUp.IsReleaseReady;
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.IsReleaseReady == true);
                viewModel.HasSearchCriteria = true;
            }


            return View("PartSetUpGlobalRemarkEditor", viewModel);
        }
    



        [HttpGet]
        public ActionResult PartSetUpSearch()
       {
           bool usercanedit = CanUserEdit();
           var viewModel = new PartSetUpViewModel
           {
               PartSetUps = _partsetupRepository.PartSetUp,
               AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
               PartSpecifications = _partspecificationRepository.PartSpecification,
               TravelCards=_travelcardRepository.TravelCard,
               PartCategories = _partCategoryRepository.PartCategory,
               CanUserEdit = usercanedit,
               HasSearchCriteria=false
           };  

           return View("PartSetUpSearch", viewModel);
     }

      
        
        [HttpPost]
       public ActionResult PartSetUpSearchResult(PartSetUpViewModel partsetup_)
       {
           bool usercanedit = CanUserEdit();

          

   
               if (ModelState["PartSetUp.CategoryID"] != null)
                ModelState["PartSetUp.CategoryID"].Errors.Clear();
              
           if (ModelState["PartSetUp.PartID"] != null)
                   ModelState["PartSetUp.PartID"].Errors.Clear();
           if (ModelState["PartSetUp.PackCode"] != null)
               ModelState["PartSetUp.PackCode"].Errors.Clear();

           if (ModelState["PartSetUp.DrawingNumber"] != null)
               ModelState["PartSetUp.DrawingNumber"].Errors.Clear();

           if (ModelState["PartSetUp.Revision"] != null)
               ModelState["PartSetUp.Revision"].Errors.Clear();
           if (ModelState["PartSetUp.IsReleaseReady"] != null)
               ModelState["PartSetUp.IsReleaseReady"].Errors.Clear();

           if (ModelState["PartSetUp.PartSetUpID"] != null)
               ModelState["PartSetUp.PartSetUpID"].Errors.Clear();


           var viewModel = new PartSetUpViewModel
           {
               PartSetUps = _partsetupRepository.PartSetUp,
               AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
               PartSpecifications = _partspecificationRepository.PartSpecification,
               TravelCards = _travelcardRepository.TravelCard,
               PartCategories = _partCategoryRepository.PartCategory,
               CanUserEdit = usercanedit,
               HasSearchCriteria = false
           };


           if (partsetup_.PartSetUp.PartID != null)
           {

           string partidsearchcriteria=partsetup_.PartSetUp.PartID.Trim();
           viewModel.PartSetUps=viewModel.PartSetUps.Where(a => a.PartID.Contains(partidsearchcriteria));
           viewModel.HasSearchCriteria = true;
           }

           if (partsetup_.PartSetUp.PartSetUpID != 0)
           {

               int setupidsearchcriteria = partsetup_.PartSetUp.PartSetUpID;
               viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartSetUpID == setupidsearchcriteria);
               viewModel.HasSearchCriteria = true;
           }


          int? categoryidsearch = partsetup_.PartSetUp.CategoryID;
           if (categoryidsearch!=0)
           {
           
               viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.CategoryID == categoryidsearch);
               viewModel.HasSearchCriteria = true;
           
           
           }

           if (partsetup_.PartSetUp.PackCode != null)
           {
               string packcodesearch = partsetup_.PartSetUp.PackCode.Trim().ToUpper();
               viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PackCode==packcodesearch);
               viewModel.HasSearchCriteria = true;
           }

           if (partsetup_.PartSetUp.DrawingNumber != null)
           {
               string drawingfilesearch = partsetup_.PartSetUp.DrawingNumber.Trim().ToUpper();
               viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingNumber==drawingfilesearch);
               viewModel.HasSearchCriteria = true;
           }
           if (partsetup_.PartSetUp.Revision != null)
           {
               string revisionsearch = partsetup_.PartSetUp.Revision.Trim().ToUpper();
               viewModel.PartSetUps = viewModel.PartSetUps.Where(c => c.Revision==revisionsearch);
               viewModel.HasSearchCriteria = true;
           }

           if (partsetup_.PartSetUp.IsReleaseReady)
           {
               bool releasereadysearch;
               releasereadysearch = partsetup_.PartSetUp.IsReleaseReady;
               viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.IsReleaseReady==true);
               viewModel.HasSearchCriteria = true;
           }


           return View("PartSetUpSearch", viewModel);
       }

        
        
        [HttpGet]
        public ActionResult PartSetUpListing()
        {

            bool usercanedit = CanUserEdit();
            var viewModel = new PartSetUpViewModel
            {
               PartSetUps=_partsetupRepository.PartSetUp,
               AdditionalProcesses=_additionalprocessingRepository.AdditionalProcess,  
               PartSpecifications=_partspecificationRepository.PartSpecification,
               PartCategories=_partCategoryRepository.PartCategory,
               CanUserEdit = usercanedit
            };
      
            viewModel.PartSetUps.OrderBy(a => a.PartCategory.CategoryName);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult GetDeviationFile(PartSetUpViewModel viewModel_)
        {
            string savelocation = ConfigurationManager.AppSettings["DeviationFile"];

            string pdfextension = ".pdf";

            string deviationfilepath = (viewModel_.DeviationFilePath != null) ? viewModel_.DeviationFilePath : "";
            int length = deviationfilepath.Length;




            if (deviationfilepath != "")
            {
                deviationfilepath = deviationfilepath.ToLower();

                if (!deviationfilepath.ToLower().Contains(pdfextension))
                {
                    string errormessage = String.Format("* Deviation file must be saved in .pdf format in the {0} location.", savelocation);
                    viewModel_.UserMessage = errormessage;
                    viewModel_.DeviationFilePath = deviationfilepath;


                    return View(viewModel_);
                }
            }


            var partsetupViewModel = new PartSetUpViewModel
            {
                PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == viewModel_.PartSetUpID)

            };

            GetUserInfo();
            if (viewModel_.DeviationFileDelete)
            {
                deviationfilepath = null;
            }
            string name = username;
            deviationfilepath = Path.GetFileName(deviationfilepath);
            //do the saving
            int partsetupid = viewModel_.PartSetUpID;
            DateTime? startdate = viewModel_.PartSetUp.DeviationFileStartDte;
            DateTime? enddate = viewModel_.PartSetUp.DeviationFileEndDte;
            if (deviationfilepath == null)
            {
                partsetupViewModel.PartSetUp.DeviationFile = null;
                enddate = null;
                startdate = null;
            }
            else {
                partsetupViewModel.PartSetUp.DeviationFile = savelocation + deviationfilepath;

            }
            partsetupViewModel.PartSetUp.DeviationFileStartDte = startdate;
            partsetupViewModel.PartSetUp.DeviationFileEndDte = enddate;
            partsetupViewModel.PartSetUp.LastEditBy = name;
            partsetupViewModel.PartSetUp.LastEditDate = DateTime.Now;
            _partsetupRepository.UpdateAttachments(partsetupViewModel.PartSetUp);
            this.ShowSaveSuccessfull();

            TravelCardViewModel viewModelTC = new TravelCardViewModel
            {
                ItemID = viewModel_.ItemID,
                ShowPartSetUpDetails = true,

                PartSetUpID = Convert.ToInt16(partsetupid)
            };

            return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);
        }

        [HttpPost]
        public ActionResult GetDrawingFile(PartSetUpViewModel viewModel_)
        {
            {

                string savelocation = ConfigurationManager.AppSettings["DrawingFile"];

                string pdfextension = ".pdf";

                string drawingfilepath = (viewModel_.DrawingFilePath != null) ? viewModel_.DrawingFilePath : "";
                int length = drawingfilepath.Length;




                if (drawingfilepath != "")
                {
                    drawingfilepath = drawingfilepath.ToLower();

                    if (!drawingfilepath.ToLower().Contains(pdfextension))
                    {
                        string errormessage = String.Format("* Drawing file must be saved in .pdf format in the {0} location.", savelocation);
                        viewModel_.UserMessage = errormessage;
                        viewModel_.DrawingFilePath = drawingfilepath;


                        return View(viewModel_);
                    }
                }


                var partsetupViewModel = new PartSetUpViewModel
                {
                    PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == viewModel_.PartSetUpID)

                };

                GetUserInfo();
               
                if (viewModel_.DrawingFileDelete||viewModel_.DrawingFilePath==null)
                {
                    drawingfilepath = null;
                }
                string name = username;
               
                //do the saving
                int partsetupid = viewModel_.PartSetUpID;
                if (drawingfilepath ==null)
                {
                    partsetupViewModel.PartSetUp.DrawingFile = null;
                }
                else {
                    drawingfilepath = Path.GetFileName(drawingfilepath);
                    partsetupViewModel.PartSetUp.DrawingFile = savelocation + drawingfilepath;
                }
                
                partsetupViewModel.PartSetUp.LastEditBy = name;
                partsetupViewModel.PartSetUp.LastEditDate = DateTime.Now;
                _partsetupRepository.UpdateAttachments(partsetupViewModel.PartSetUp);
                this.ShowSaveSuccessfull();

                TravelCardViewModel viewModelTC = new TravelCardViewModel
                {
                    ItemID = viewModel_.ItemID,
                    ShowPartSetUpDetails = true,

                    PartSetUpID = Convert.ToInt16(partsetupid)
                };

                return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);
            }


        }

        [HttpGet]
        public ActionResult GetDrawingFile(int partsetupid_)
        {


            var viewModel = new PartSetUpViewModel
            {

                PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(x => x.PartSetUpID == partsetupid_),
                PartSetUpID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartSetUpID).FirstOrDefault(),
               DrawingFileDelete = false,
                ItemID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartID).FirstOrDefault(),
                UserMessage = "",


            };
            viewModel.DrawingFilePath = viewModel.PartSetUp.DrawingFile;
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetDeviationFile(int partsetupid_)
        {


            var viewModel = new PartSetUpViewModel
            {

                PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(x => x.PartSetUpID == partsetupid_),
                 PartSetUpID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartSetUpID).FirstOrDefault(),
                         DeviationFileDelete = false,
                           ItemID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartID).FirstOrDefault(),
                                UserMessage="",
                               
      
            };
            viewModel.DeviationFilePath = viewModel.PartSetUp.DeviationFile;
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetDeviationFile2(int partsetupid_)
        {


            var viewModel = new PartSetUpViewModel
            {

                PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(x => x.PartSetUpID == partsetupid_),
                PartSetUpID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartSetUpID).FirstOrDefault(),
                DeviationFileDelete = false,
                ItemID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartID).FirstOrDefault(),
                UserMessage = "",


            };
            viewModel.DeviationFilePath = viewModel.PartSetUp.DeviationFile2;
            return View(viewModel);
        }



        [HttpPost]
        public ActionResult GetDeviationFile2(PartSetUpViewModel viewModel_)
        {
            string savelocation = ConfigurationManager.AppSettings["DeviationFile"];

            string pdfextension = ".pdf";

            string deviationfilepath = (viewModel_.DeviationFilePath != null) ? viewModel_.DeviationFilePath : "";
            int length = deviationfilepath.Length;




            if (deviationfilepath != "")
            {
                deviationfilepath = deviationfilepath.ToLower();

                if (!deviationfilepath.ToLower().Contains(pdfextension))
                {
                    string errormessage = String.Format("* Deviation file must be saved in .pdf format in the {0} location.", savelocation);
                    viewModel_.UserMessage = errormessage;
                    viewModel_.DeviationFilePath = deviationfilepath;


                    return View(viewModel_);
                }
            }


            var partsetupViewModel = new PartSetUpViewModel
            {
                PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == viewModel_.PartSetUpID)

            };

            GetUserInfo();
            if (viewModel_.DeviationFileDelete)
            {
                deviationfilepath = null;
            }
            string name = username;
            deviationfilepath = Path.GetFileName(deviationfilepath);
            //do the saving
            int partsetupid = viewModel_.PartSetUpID;
            DateTime? startdate = viewModel_.PartSetUp.DeviationFile2StartDte;
            DateTime? enddate = viewModel_.PartSetUp.DeviationFile2EndDte;
            if (deviationfilepath == null)
            {
                partsetupViewModel.PartSetUp.DeviationFile2 = null;
                enddate = null;
                startdate = null;
            }
            else
            {
                partsetupViewModel.PartSetUp.DeviationFile2 = savelocation + deviationfilepath;

            }
            partsetupViewModel.PartSetUp.DeviationFile2StartDte = startdate;
            partsetupViewModel.PartSetUp.DeviationFile2EndDte = enddate;
            partsetupViewModel.PartSetUp.LastEditBy = name;
            partsetupViewModel.PartSetUp.LastEditDate = DateTime.Now;
            _partsetupRepository.UpdateAttachments(partsetupViewModel.PartSetUp);


            this.ShowSaveSuccessfull();

            TravelCardViewModel viewModelTC = new TravelCardViewModel
            {
                ItemID = viewModel_.ItemID,
                ShowPartSetUpDetails = true,

                PartSetUpID = Convert.ToInt16(partsetupid)
            };

            return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);
        }


        [HttpPost]
        public ActionResult GetDieSetUpFile(PartSetUpViewModel viewModel_)
        {
      
            string savelocation = ConfigurationManager.AppSettings["DieSetUpInstructions"];

            string pdfextension = ".pdf";

            string diesetupinstructionsfilepath = (viewModel_.DieSetUpFilePath != null) ? viewModel_.DieSetUpFilePath : "";
            int length = diesetupinstructionsfilepath.Length;




            if (diesetupinstructionsfilepath != "")
            {
                diesetupinstructionsfilepath = diesetupinstructionsfilepath.ToLower();

                if (!diesetupinstructionsfilepath.ToLower().Contains(pdfextension))
                {
                    string errormessage = String.Format("* Die Set Up Instruction file must be saved in .pdf format in the {0} location.", savelocation);
                    viewModel_.UserMessage = errormessage;
                    viewModel_.DieSetUpFilePath = diesetupinstructionsfilepath;


                    return View(viewModel_);
                }
            }


            var partsetupViewModel = new PartSetUpViewModel
            {
                PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == viewModel_.PartSetUpID)

            };

            GetUserInfo();
            if (viewModel_.DieSetUpFileDelete)
            {
                diesetupinstructionsfilepath = null;
            }
            string name = username;
            diesetupinstructionsfilepath = Path.GetFileName(diesetupinstructionsfilepath);
            //do the saving
            int partsetupid = viewModel_.PartSetUpID;
            if (diesetupinstructionsfilepath == null)
            {
                partsetupViewModel.PartSetUp.DieSetUpFile = null;
            }
            else
            {
                partsetupViewModel.PartSetUp.DieSetUpFile = savelocation + diesetupinstructionsfilepath;
            }
            partsetupViewModel.PartSetUp.LastEditBy = name;
            partsetupViewModel.PartSetUp.LastEditDate = DateTime.Now;
            _partsetupRepository.UpdateAttachments(partsetupViewModel.PartSetUp);
            this.ShowSaveSuccessfull();

            TravelCardViewModel viewModelTC = new TravelCardViewModel
            {
                ItemID = viewModel_.ItemID,
                ShowPartSetUpDetails = true,

                PartSetUpID = Convert.ToInt16(partsetupid)
            };

            return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);




        }


        [HttpPost]
        public ActionResult GetQualityAlertFile(PartSetUpViewModel viewModel_)
        {
            string savelocation = ConfigurationManager.AppSettings["QualityAlertFile"];

            string pdfextension = ".pdf";

            string qualityalertfilepath = (viewModel_.QualityAlertFilePath != null) ? viewModel_.QualityAlertFilePath : "";
            int length = qualityalertfilepath.Length;




            if (qualityalertfilepath != "")
            {
                qualityalertfilepath = qualityalertfilepath.ToLower();

                if (!qualityalertfilepath.ToLower().Contains(pdfextension))
                {
                    string errormessage = String.Format("* Quality alert file must be saved in .pdf format in the {0} location.", savelocation);
                    viewModel_.UserMessage = errormessage;
                    viewModel_.QualityAlertFilePath = qualityalertfilepath;


                    return View(viewModel_);
                }
            }


            var partsetupViewModel = new PartSetUpViewModel
            {
                PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == viewModel_.PartSetUpID)

            };

            GetUserInfo();
            
            if (viewModel_.QualityAlertFileDelete)
            {
                qualityalertfilepath = null;
            }
            string name = username;
            qualityalertfilepath = Path.GetFileName(qualityalertfilepath);
            //do the saving
            int partsetupid = viewModel_.PartSetUpID;
            DateTime? startdate = viewModel_.PartSetUp.QualityAlertStartDte;
            DateTime? enddate = viewModel_.PartSetUp.QualityAlertEndDte;
            if (qualityalertfilepath == null)
            {
                partsetupViewModel.PartSetUp.QualityAlertFile = null;
                enddate = null;
                startdate = null;

            }
            else 
            {
                partsetupViewModel.PartSetUp.QualityAlertFile = savelocation + qualityalertfilepath;
                partsetupViewModel.PartSetUp.QualityAlertStartDte = startdate;
                partsetupViewModel.PartSetUp.QualityAlertEndDte = enddate;
            }

           
            if (partsetupViewModel.PartSetUp.QualityAlertFile!=null || partsetupViewModel.PartSetUp.QualityAlertFile != null)
            {
                partsetupViewModel.PartSetUp.HasQualityAlert = true;
            }
            else
            {
                partsetupViewModel.PartSetUp.HasQualityAlert = false;
            }
            partsetupViewModel.PartSetUp.LastEditBy = name;
            partsetupViewModel.PartSetUp.LastEditDate = DateTime.Now;
            _partsetupRepository.UpdateAttachments(partsetupViewModel.PartSetUp);
            this.ShowSaveSuccessfull();

            TravelCardViewModel viewModelTC = new TravelCardViewModel
            {
                ItemID = viewModel_.ItemID,
                ShowPartSetUpDetails = true,

                PartSetUpID = Convert.ToInt16(partsetupid)
            };

            return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);
        }
       [HttpPost]
        public ActionResult GetGreenLightRedLightFile(PartSetUpViewModel viewModel_)
        {

            string savelocation = ConfigurationManager.AppSettings["RedLightGreenLightFile"];

            string pdfextension = ".pdf";

            string redlightgreenlightfilepath = (viewModel_.RedLightGreenLightFile != null) ? viewModel_.RedLightGreenLightFile : "";
            int length = redlightgreenlightfilepath.Length;




            if (redlightgreenlightfilepath != "")
            {
                redlightgreenlightfilepath = redlightgreenlightfilepath.ToLower();

                if (!redlightgreenlightfilepath.ToLower().Contains(pdfextension))
                {
                    string errormessage = String.Format("* Set Up Instruction file must be saved in .pdf format in the {0} location.", savelocation);
                    viewModel_.UserMessage = errormessage;
                    viewModel_.RedLightGreenLightFile = redlightgreenlightfilepath;


                    return View(viewModel_);
                }
            }


            var partsetupViewModel = new PartSetUpViewModel
            {
                PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == viewModel_.PartSetUpID)

            };

            GetUserInfo();
        
            if (viewModel_.RedLightGreenLightSetUpfileDelete)
            {
                redlightgreenlightfilepath = null;
            }
            string name = username;
            redlightgreenlightfilepath = Path.GetFileName(redlightgreenlightfilepath);
            //do the saving
            int partsetupid = viewModel_.PartSetUpID;
            if (redlightgreenlightfilepath != null)
            {
                partsetupViewModel.PartSetUp.RedLightGreenLightFile = savelocation + redlightgreenlightfilepath;
            }
            else 
            {
                partsetupViewModel.PartSetUp.RedLightGreenLightFile = null;
            }
            partsetupViewModel.PartSetUp.LastEditBy = name;
            partsetupViewModel.PartSetUp.LastEditDate = DateTime.Now;
            _partsetupRepository.UpdateAttachments(partsetupViewModel.PartSetUp);
            this.ShowSaveSuccessfull();

            TravelCardViewModel viewModelTC = new TravelCardViewModel
            {
                ItemID = viewModel_.ItemID,
                ShowPartSetUpDetails = true,

                PartSetUpID = Convert.ToInt16(partsetupid)
            };

            return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);




        }

       [HttpGet]
       public ActionResult GetAdditionalFile(int partsetupid_)
       {


           var viewModel = new PartSetUpViewModel
           {

               PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(x => x.PartSetUpID == partsetupid_),
               PartSetUpID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartSetUpID).FirstOrDefault(),
               AdditionalFileDelete = false,
               ItemID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartID).FirstOrDefault(),
               UserMessage = "",


           };
           viewModel.AdditionalFile = viewModel.PartSetUp.AdditionalFile;
           return View(viewModel);
       }
       [HttpPost]
       public ActionResult GetAdditionalFile(PartSetUpViewModel viewModel_)
       {

           string savelocation = ConfigurationManager.AppSettings["AdditionalFile"];

           string pdfextension = ".pdf";

           string additionalfilepath = (viewModel_.AdditionalFile != null) ? viewModel_.AdditionalFile : "";
           int length = additionalfilepath.Length;




           if (additionalfilepath != "")
           {
               additionalfilepath = additionalfilepath.ToLower();

               if (!additionalfilepath.ToLower().Contains(pdfextension))
               {
                   string errormessage = String.Format("* Additional file must be saved in .pdf format in the {0} location.", savelocation);
                   viewModel_.UserMessage = errormessage;
                   viewModel_.AdditionalFile = additionalfilepath;


                   return View(viewModel_);
               }
           }


           var partsetupViewModel = new PartSetUpViewModel
           {
               PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == viewModel_.PartSetUpID)

           };

           GetUserInfo();

           if (viewModel_.AdditionalFileDelete)
           {
               additionalfilepath = null;
           }
           string name = username;
           additionalfilepath = Path.GetFileName(additionalfilepath);
           //do the saving
           int partsetupid = viewModel_.PartSetUpID;
           if (additionalfilepath != null)
           {
               partsetupViewModel.PartSetUp.AdditionalFile = savelocation + additionalfilepath;
           }
           else
           {
               partsetupViewModel.PartSetUp.AdditionalFile = null;
           }
           partsetupViewModel.PartSetUp.LastEditBy = name;
           partsetupViewModel.PartSetUp.LastEditDate = DateTime.Now;
           _partsetupRepository.UpdateAttachments(partsetupViewModel.PartSetUp);
           this.ShowSaveSuccessfull();

           TravelCardViewModel viewModelTC = new TravelCardViewModel
           {
               ItemID = viewModel_.ItemID,
               ShowPartSetUpDetails = true,

               PartSetUpID = Convert.ToInt16(partsetupid)
           };

           return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);


       }



        [HttpGet]
        public ActionResult GetGreenLightRedLightFile(int partsetupid_)
        {


            var viewModel = new PartSetUpViewModel
            {

                PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(x => x.PartSetUpID == partsetupid_),
                PartSetUpID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartSetUpID).FirstOrDefault(),
                DieSetUpFileDelete = false,
                ItemID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartID).FirstOrDefault(),
                UserMessage = "",


            };
            viewModel.RedLightGreenLightFile = viewModel.PartSetUp.RedLightGreenLightFile;
            return View(viewModel);
        }



        [HttpGet]
        public ActionResult GetDieSetUpFile(int partsetupid_)
        {


            var viewModel = new PartSetUpViewModel
            {

                PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(x => x.PartSetUpID == partsetupid_),
                PartSetUpID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartSetUpID).FirstOrDefault(),
                DieSetUpFileDelete = false,
                ItemID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartID).FirstOrDefault(),
                UserMessage = "",


            };
            viewModel.DieSetUpFilePath = viewModel.PartSetUp.DieSetUpFile;
            return View(viewModel);
        }


        [HttpGet]
        public ActionResult GetQualityAlertFile2(int partsetupid_)
        {


            var viewModel = new PartSetUpViewModel
            {

                PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(x => x.PartSetUpID == partsetupid_),
                PartSetUpID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartSetUpID).FirstOrDefault(),
                QualityAlertFileDelete = false,
                ItemID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartID).FirstOrDefault(),
                UserMessage = "",


            };
            viewModel.QualityAlertFilePath = viewModel.PartSetUp.QualityAlert2;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult GetQualityAlertFile2(PartSetUpViewModel viewModel_)
        {
            string savelocation = ConfigurationManager.AppSettings["QualityAlertFile"];

            string pdfextension = ".pdf";

            string qualityalertfilepath = (viewModel_.QualityAlertFilePath != null) ? viewModel_.QualityAlertFilePath : "";
            int length = qualityalertfilepath.Length;




            if (qualityalertfilepath != "")
            {
                qualityalertfilepath = qualityalertfilepath.ToLower();

                if (!qualityalertfilepath.ToLower().Contains(pdfextension))
                {
                    string errormessage = String.Format("* Qualiity alert file must be saved in .pdf format in the {0} location.", savelocation);
                    viewModel_.UserMessage = errormessage;
                    viewModel_.QualityAlertFilePath = qualityalertfilepath;


                    return View(viewModel_);
                }
            }


            var partsetupViewModel = new PartSetUpViewModel
            {
                PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == viewModel_.PartSetUpID)

            };

            GetUserInfo();

            if (viewModel_.QualityAlertFileDelete)
            {
                qualityalertfilepath = null;
            }
            string name = username;
            qualityalertfilepath = Path.GetFileName(qualityalertfilepath);
            //do the saving
            int partsetupid = viewModel_.PartSetUpID;
            DateTime? startdate = viewModel_.PartSetUp.QualityAlert2StartDte;
            DateTime? enddate = viewModel_.PartSetUp.QualityAlert2EndDte;
            if (qualityalertfilepath == null)
            {
                partsetupViewModel.PartSetUp.QualityAlert2 = null;
                enddate = null;
                startdate = null;

            }
            else
            {
                partsetupViewModel.PartSetUp.QualityAlert2 = savelocation + qualityalertfilepath;
                partsetupViewModel.PartSetUp.QualityAlert2StartDte = startdate;
                partsetupViewModel.PartSetUp.QualityAlert2EndDte = enddate;
            }


            if (partsetupViewModel.PartSetUp.QualityAlertFile != null || partsetupViewModel.PartSetUp.QualityAlertFile != null)
            {
                partsetupViewModel.PartSetUp.HasQualityAlert = true;
            }
            else
            {
                partsetupViewModel.PartSetUp.HasQualityAlert = false;
            }
            partsetupViewModel.PartSetUp.LastEditBy = name;
            partsetupViewModel.PartSetUp.LastEditDate = DateTime.Now;
            _partsetupRepository.UpdateAttachments(partsetupViewModel.PartSetUp);
            this.ShowSaveSuccessfull();

            TravelCardViewModel viewModelTC = new TravelCardViewModel
            {
                ItemID = viewModel_.ItemID,
                ShowPartSetUpDetails = true,

                PartSetUpID = Convert.ToInt16(partsetupid)
            };

            return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);
        }
        [HttpPost]
        public ActionResult GlobalQualityAlertFile2Edits(PartSetUpViewModel partsetup_, FormCollection result)
        {

            //this action saves the the drawing file path for the selected part set ups
            if (ModelState["PartSetUp.CategoryID"] != null)
                ModelState["PartSetUp.CategoryID"].Errors.Clear();

            if (ModelState["PartSetUp.PartID"] != null)
                ModelState["PartSetUp.PartID"].Errors.Clear();
            if (ModelState["PartSetUp.PackCode"] != null)
                ModelState["PartSetUp.PackCode"].Errors.Clear();

            if (ModelState["PartSetUp.DrawingNumber"] != null)
                ModelState["PartSetUp.DrawingNumber"].Errors.Clear();

            if (ModelState["PartSetUp.Revision"] != null)
                ModelState["PartSetUp.Revision"].Errors.Clear();
            if (ModelState["PartSetUp.IsReleaseReady"] != null)
                ModelState["PartSetUp.IsReleaseReady"].Errors.Clear();

            if (ModelState["PartSetUp.PartSetUpID"] != null)
                ModelState["PartSetUp.PartSetUpID"].Errors.Clear();

            if (ModelState["PartSetUp.DrawingFile"] != null)
                ModelState["PartSetUp.DrawingFile"].Errors.Clear();

            var partsetupViewModel = new PartSetUpViewModel
            {


            };
            //buildPath, browse controls for chrome and firefox don't pick the path, only internet explorer does


            string savelocation = ConfigurationManager.AppSettings["QualityAlertFile"];

            string pdfextension = ".pdf";

            string qualityalertfilepath = (partsetup_.QualityAlertFilePath != null) ? partsetup_.QualityAlertFilePath : "";
            DateTime? start = (partsetup_.PartSetUp.QualityAlert2StartDte != null) ? partsetup_.PartSetUp.QualityAlert2StartDte : null;
            DateTime? end = (partsetup_.PartSetUp.QualityAlert2EndDte != null) ? partsetup_.PartSetUp.QualityAlert2EndDte : null;

            int length = qualityalertfilepath.Length;




            if (qualityalertfilepath != "")
            {
                qualityalertfilepath = qualityalertfilepath.ToLower();

                if (!qualityalertfilepath.ToLower().Contains(pdfextension))
                {
                    string errormessage = String.Format("* Quality Alert File file must be saved in .pdf format in the {0} location.", savelocation);
                    partsetup_.UserMessage = errormessage;
                    partsetup_.QualityAlertFilePath = qualityalertfilepath;


                    return View(partsetup_);
                }
            }
            string formattedfilepath = "";

            if (partsetup_.QualityAlertFileDelete)
            {
                qualityalertfilepath = null;
                end = null;
                start = null;
            }
            string name = username;

            //do the saving

            if (qualityalertfilepath == null || qualityalertfilepath.Length == 0)
            {

                formattedfilepath = qualityalertfilepath = null;
            }
            else
            {
                qualityalertfilepath = Path.GetFileName(qualityalertfilepath);

                formattedfilepath = qualityalertfilepath = savelocation + qualityalertfilepath;
            }



            var selecteditems = from x in result.AllKeys
                                where result[x] != "false"

                                select x;


            foreach (var item in selecteditems)
            {
                if (item.Contains("PartSetUptoUpdate_"))
                {
                    GetUserInfo();
                    string theitem = item.ToString();

                    theitem = theitem.Replace("PartSetUptoUpdate_", "");


                    int partsetupidtomodify = Convert.ToInt16(theitem);
                    var qualityalertfilename = formattedfilepath;

                    DateTime lasteditdate = DateTime.Now;
                    string lasteditedby = username;
                    bool setpartsetupqualityalertfiletonull = partsetup_.QualityAlertFileDelete;
                    if (setpartsetupqualityalertfiletonull)
                    {
                        qualityalertfilename = null;
                    }

                    partsetupViewModel.PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == partsetupidtomodify);

                    var partsetup = partsetupViewModel.PartSetUp;

                    partsetup.QualityAlert2 = qualityalertfilename;

                    partsetup.QualityAlert2StartDte = start;
                    partsetup.QualityAlert2EndDte = end;
                    partsetup.LastEditBy = username;
                    partsetup.LastEditDate = DateTime.Now;



                    _partsetupRepository.UpdateQualityAlertFile2Edits(partsetup);
                    partsetup = null;
                    this.ShowSaveSuccessfull();




                }


            }
            bool canuseredit = CanUserEdit();
            bool canuserapprove = CanUserApprove();




            var viewModel = new PartSetUpViewModel
            {
                PartSetUps = _partsetupRepository.PartSetUp,
                AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
                PartSpecifications = _partspecificationRepository.PartSpecification,
                TravelCards = _travelcardRepository.TravelCard,
                PartCategories = _partCategoryRepository.PartCategory,
                CanUserEdit = canuseredit,
                CanUserApprove = canuserapprove,
                HasSearchCriteria = false
            };


            if (partsetup_.PartSetUp.PartID != null)
            {

                string partidsearchcriteria = partsetup_.PartSetUp.PartID.Trim();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartID.Contains(partidsearchcriteria));
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.PartSetUpID != 0)
            {

                int setupidsearchcriteria = partsetup_.PartSetUp.PartSetUpID;
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartSetUpID == setupidsearchcriteria);
                viewModel.HasSearchCriteria = true;
            }


            int? categoryidsearch = partsetup_.PartSetUp.CategoryID;
            if (categoryidsearch != 0)
            {

                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.CategoryID == categoryidsearch);
                viewModel.HasSearchCriteria = true;


            }

            if (partsetup_.PartSetUp.PackCode != null)
            {
                string packcodesearch = partsetup_.PartSetUp.PackCode.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PackCode == packcodesearch);
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.DrawingFile != null)
            {
                string drawingfilesearch = partsetup_.PartSetUp.DrawingFile.Trim();

                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingFile == drawingfilesearch.Replace("//", "/"));
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.QualityAlertFile != null)
            {
                string qualityAlertFileSearch = partsetup_.PartSetUp.QualityAlertFile.Trim();

                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.QualityAlertFile == qualityAlertFileSearch.Replace("//", "/"));
                viewModel.HasSearchCriteria = true;
            }




            if (partsetup_.PartSetUp.DrawingNumber != null)
            {
                string drawingnumbersearch = partsetup_.PartSetUp.DrawingNumber.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingNumber == drawingnumbersearch);
                viewModel.HasSearchCriteria = true;
            }
            if (partsetup_.PartSetUp.Revision != null)
            {
                string revisionsearch = partsetup_.PartSetUp.Revision.Trim().ToUpper();
                viewModel.PartSetUps = viewModel.PartSetUps.Where(c => c.Revision == revisionsearch);
                viewModel.HasSearchCriteria = true;
            }

            if (partsetup_.PartSetUp.IsReleaseReady)
            {
                bool releasereadysearch;
                releasereadysearch = partsetup_.PartSetUp.IsReleaseReady;
                viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.IsReleaseReady == true);
                viewModel.HasSearchCriteria = true;
            }


            return View("PartSetUpGlobalQualityAlertFile2Editor", viewModel);
        }
        [HttpGet]
        public ActionResult GetQualityAlertFile(int partsetupid_)
        {


            var viewModel = new PartSetUpViewModel
            {

                PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(x => x.PartSetUpID == partsetupid_),
                PartSetUpID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartSetUpID).FirstOrDefault(),
                QualityAlertFileDelete = false,
                ItemID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartID).FirstOrDefault(),
                UserMessage = "",


            };
            viewModel.QualityAlertFilePath = viewModel.PartSetUp.QualityAlertFile;
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult PartSetUpCreate(string ItemID)
        {
            string id = ItemID.Trim();
            PartSetUpViewModel viewModel = new PartSetUpViewModel
            {
               ItemID=id,
            
              
               UserMessage="",
               PartCategories=_partCategoryRepository.PartCategory,
  
            };
            
            
            return View("PartSetUpCreate",viewModel);
        }

        [HttpPost]
        public ActionResult PartSetUpCreate(PartSetUpViewModel viewModel_)
        {
         
           
            if (ModelState["PartSetUp.PartSetUpID"] != null)
                ModelState["PartSetUp.PartSetUpID"].Errors.Clear();

   
            
          
            GetUserInfo();
            if (viewModel_.PartSetUp.CategoryID == 0)
            {
                ModelState.AddModelError("PartSetUpCreate", "Part Category is required.");
            }
   
         
          


            int partsetupid = 0;
            if (ModelState.IsValid)
            {
                DateTime curdate = DateTime.Now;
                
                string itemid = viewModel_.ItemID;
                string packcode = viewModel_.PartSetUp.PackCode != null ? viewModel_.PartSetUp.PackCode.ToUpper() : "";
                string drawingnumber = viewModel_.PartSetUp.DrawingNumber != null ? viewModel_.PartSetUp.DrawingNumber.ToUpper() : "";
                string revision = viewModel_.PartSetUp.Revision != null ? viewModel_.PartSetUp.Revision.ToUpper() : "";
                viewModel_.PartSetUp.PartID = viewModel_.ItemID;
                viewModel_.PartSetUp.CreateDate = curdate;
                viewModel_.PartSetUp.LastEditDate = curdate;
                viewModel_.PartSetUp.QualityAlertFile = viewModel_.QualityAlertFilePath;
                viewModel_.PartSetUp.SetupDrawingFile = viewModel_.SetUpFilePath;
                viewModel_.PartSetUp.DrawingFile = viewModel_.DrawingFilePath;
                viewModel_.PartSetUp.PackCode = packcode;
                viewModel_.PartSetUp.DieSetUpFile = viewModel_.DieSetUpFilePath;
                viewModel_.PartSetUp.DrawingNumber = drawingnumber;
                viewModel_.PartSetUp.Revision = revision;
                viewModel_.PartSetUp.PartComment = viewModel_.PartSetUp.PartComment;
                viewModel_.PartSetUp.PartRemarks = viewModel_.PartSetUp.PartRemarks;
                viewModel_.PartSetUp.Notes = viewModel_.PartSetUp.Notes;
                              viewModel_.PartSetUp.CommunicationNote = viewModel_.PartSetUp.CommunicationNote;
                               viewModel_.PartSetUp.CreatedBy = username;
                viewModel_.PartSetUp.LastEditBy = username;
               
                partsetupid = _partsetupRepository.Insert(viewModel_.PartSetUp);
                viewModel_.PartCategories = _partCategoryRepository.PartCategory;
                this.ShowSaveSuccessfull();
                //send user back to partmaintenance index
                TravelCardViewModel viewModelTC = new TravelCardViewModel
                {
                    ItemID = itemid,
                    ShowPartSetUpDetails=true,
                    PartSetUpID = Convert.ToInt16(partsetupid)
                };
                
                return RedirectToAction("PartMaintenanceIndexShowSavedResult","TravelCard", viewModelTC);

            }
            else
            {

                viewModel_.PartCategories = _partCategoryRepository.PartCategory;

                return View(viewModel_);
            }
        }

      
 
        
        public ActionResult PartSetUpDetails(int partsetup_ )
        {
            bool usercanedit = CanUserEdit();
            var viewModel = new PartSetUpViewModel
            {
                PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(x => x.PartSetUpID == partsetup_),
                CanUserEdit=usercanedit
            };

            var categoryname=_partCategoryRepository.PartCategory.Where(a => a.CategoryID == viewModel.PartSetUp.CategoryID).Select(b => b.CategoryName);
            viewModel.PartCategory = categoryname.FirstOrDefault();
              
           return View("PartSetupDetails", viewModel);
        }


        [HttpGet]
        public ActionResult PartSetUpEdit(int PartSetUpID, string message)

        {
            bool canuserapprove = CanUserApprove();
            bool canuseredit=CanUserEdit();
            var viewModel = new PartSetUpViewModel
            {
                PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(x => x.PartSetUpID == PartSetUpID),
                PartCategories = _partCategoryRepository.PartCategory,
                PartSetUpID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == PartSetUpID).Select(a=>a.PartSetUpID).FirstOrDefault(),
                ItemID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == PartSetUpID).Select(a => a.PartID).FirstOrDefault(),
                CanUserApprove=canuserapprove,
                CanUserEdit=canuseredit
            };
            bool isreleaseready = viewModel.PartSetUp.IsReleaseReady;
            viewModel.DrawingNumber = viewModel.PartSetUp.DrawingNumber;
            viewModel.IsCurrentlyReleaseReady = isreleaseready;
            return View("PartSetupEdit", viewModel);
        }
        [HttpGet]
        public ActionResult PartSetUpViewer(int partsetupid_)
        {

            Int16 setupid = Convert.ToInt16(partsetupid_);
            //send user back to partmaintenance index
            TravelCardViewModel viewModelTC = new TravelCardViewModel
            {
                PartSetUp=_partsetupRepository.PartSetUp.FirstOrDefault(a=>a.PartSetUpID==setupid),
                ShowPartSetUpDetails=true,
                PartSetUpID = setupid
            };
             var itemid=viewModelTC.PartSetUp.PartID;
             viewModelTC.ItemID = itemid;
            return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);
       
        }
        [HttpGet]
        public ActionResult GetSetUpInstructionsFile(int partsetupid_)
      {


          var viewModel = new PartSetUpViewModel
          {

              PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(x => x.PartSetUpID == partsetupid_),
              PartSetUpID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartSetUpID).FirstOrDefault(),
              SetUpFileDelete = false,
              ItemID = _partsetupRepository.PartSetUp.Where(x => x.PartSetUpID == partsetupid_).Select(a => a.PartID).FirstOrDefault(),
              UserMessage = "",


          };
          viewModel.SetUpFilePath = viewModel.PartSetUp.SetupDrawingFile;
          return View(viewModel);
      }


        [HttpPost]
       public ActionResult GetSetUpInstructionsFile(PartSetUpViewModel viewModel_)
      {
          string savelocation = ConfigurationManager.AppSettings["MachineSetUpInstructions"];
        

          string pdfextension = ".pdf";

          string setupinstructionsfilepath = (viewModel_.SetUpFilePath != null) ? viewModel_.SetUpFilePath : "";
          int length = setupinstructionsfilepath.Length;




          if (setupinstructionsfilepath != "")
          {
              setupinstructionsfilepath = setupinstructionsfilepath.ToLower();

              if (!setupinstructionsfilepath.ToLower().Contains(pdfextension))
              {
                  string errormessage = String.Format("* Set Up Instruction file must be saved in .pdf format in the {0} location.", savelocation);
                  viewModel_.UserMessage = errormessage;
                  viewModel_.SetUpFilePath = setupinstructionsfilepath;


                  return View(viewModel_);
              }
          }


          var partsetupViewModel = new PartSetUpViewModel
          {
              PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == viewModel_.PartSetUpID)

          };

          GetUserInfo();
         
          if (viewModel_.SetUpFileDelete)
          {
              setupinstructionsfilepath = null;
          }
          string name = username;
          setupinstructionsfilepath = Path.GetFileName(setupinstructionsfilepath);
          //do the saving
          int partsetupid = viewModel_.PartSetUpID;
          if (setupinstructionsfilepath == null)
          {
              partsetupViewModel.PartSetUp.SetupDrawingFile = null;
          }
          else {
              partsetupViewModel.PartSetUp.SetupDrawingFile = savelocation + setupinstructionsfilepath;
          }
        
          partsetupViewModel.PartSetUp.LastEditBy = name;
          partsetupViewModel.PartSetUp.LastEditDate = DateTime.Now;
          _partsetupRepository.UpdateAttachments(partsetupViewModel.PartSetUp);
          this.ShowSaveSuccessfull();

          TravelCardViewModel viewModelTC = new TravelCardViewModel
          {
              ItemID = viewModel_.ItemID,
              ShowPartSetUpDetails = true,

              PartSetUpID = Convert.ToInt16(partsetupid)
          };

          return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);
 
     
      }
      [HttpPost]
      public ActionResult GlobalRemarksEdits(PartSetUpViewModel partsetup_, FormCollection result)
      {
          //this action saves the the remarks/watermark for the selected part set ups
          if (ModelState["PartSetUp.CategoryID"] != null)
              ModelState["PartSetUp.CategoryID"].Errors.Clear();

          if (ModelState["PartSetUp.PartID"] != null)
              ModelState["PartSetUp.PartID"].Errors.Clear();
          if (ModelState["PartSetUp.PackCode"] != null)
              ModelState["PartSetUp.PackCode"].Errors.Clear();

          if (ModelState["PartSetUp.DrawingNumber"] != null)
              ModelState["PartSetUp.DrawingNumber"].Errors.Clear();

          if (ModelState["PartSetUp.Revision"] != null)
              ModelState["PartSetUp.Revision"].Errors.Clear();
          if (ModelState["PartSetUp.IsReleaseReady"] != null)
              ModelState["PartSetUp.IsReleaseReady"].Errors.Clear();

          if (ModelState["PartSetUp.PartSetUpID"] != null)
              ModelState["PartSetUp.PartSetUpID"].Errors.Clear();

          if (ModelState["PartSetUp.DrawingFile"] != null)
              ModelState["PartSetUp.DrawingFile"].Errors.Clear();

          var partsetupViewModel = new PartSetUpViewModel
      {


      };

          var selecteditems = from x in result.AllKeys
                              where result[x] != "false"

                              select x;

          foreach (var item in selecteditems)
          {
              if (item.Contains("PartSetUptoUpdate_"))
              {
                  GetUserInfo();
                  string theitem = item.ToString();
                  theitem = theitem.Replace("PartSetUptoUpdate_", "");
                  int partsetupidtomodify = Convert.ToInt16(theitem);
                  DateTime lasteditdate = DateTime.Now;
                  string lasteditedby = username;
                  string watermark = partsetup_.Watermark;
                  partsetupViewModel.PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == partsetupidtomodify);
                  var partsetup = partsetupViewModel.PartSetUp;
                  partsetup.CommunicationNote = watermark;
                  partsetup.LastEditBy = lasteditedby;
                  partsetup.LastEditDate = lasteditdate;
                  _partsetupRepository.UpdateCommunicationNoteEdits(partsetup);
                  partsetup = null;
                  this.ShowSaveSuccessfull();
              }

          }
          bool canuseredit = CanUserEdit();
          bool canuserapprove = CanUserApprove();



          var viewModel = new PartSetUpViewModel
          {
              PartSetUps = _partsetupRepository.PartSetUp,
              AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
              PartSpecifications = _partspecificationRepository.PartSpecification,
              TravelCards = _travelcardRepository.TravelCard,
              PartCategories = _partCategoryRepository.PartCategory,
              CanUserEdit = canuseredit,
              CanUserApprove = canuserapprove,
              HasSearchCriteria = false
          };


          if (partsetup_.PartSetUp.PartID != null)
          {

              string partidsearchcriteria = partsetup_.PartSetUp.PartID.Trim();
              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartID.Contains(partidsearchcriteria));
              viewModel.HasSearchCriteria = true;
          }

          if (partsetup_.PartSetUp.PartSetUpID != 0)
          {

              int setupidsearchcriteria = partsetup_.PartSetUp.PartSetUpID;
              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartSetUpID == setupidsearchcriteria);
              viewModel.HasSearchCriteria = true;
          }


          int? categoryidsearch = partsetup_.PartSetUp.CategoryID;
          if (categoryidsearch != 0)
          {

              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.CategoryID == categoryidsearch);
              viewModel.HasSearchCriteria = true;


          }

          if (partsetup_.PartSetUp.PackCode != null)
          {
              string packcodesearch = partsetup_.PartSetUp.PackCode.Trim().ToUpper();
              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PackCode == packcodesearch);
              viewModel.HasSearchCriteria = true;
          }

          if (partsetup_.PartSetUp.DrawingFile != null)
          {
              string drawingfilesearch = partsetup_.PartSetUp.DrawingFile.Trim();

              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingFile == drawingfilesearch.Replace("//", "/"));
              viewModel.HasSearchCriteria = true;
          }


          if (partsetup_.PartSetUp.DrawingNumber != null)
          {
              string drawingnumbersearch = partsetup_.PartSetUp.DrawingNumber.Trim().ToUpper();
              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingNumber == drawingnumbersearch);
              viewModel.HasSearchCriteria = true;
          }
          if (partsetup_.PartSetUp.Revision != null)
          {
              string revisionsearch = partsetup_.PartSetUp.Revision.Trim().ToUpper();
              viewModel.PartSetUps = viewModel.PartSetUps.Where(c => c.Revision == revisionsearch);
              viewModel.HasSearchCriteria = true;
          }

          if (partsetup_.PartSetUp.IsReleaseReady)
          {
              bool releasereadysearch;
              releasereadysearch = partsetup_.PartSetUp.IsReleaseReady;
              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.IsReleaseReady == true);
              viewModel.HasSearchCriteria = true;
          }


          return View("PartSetUpGlobalRemarkEditor", viewModel);
      }


     [HttpPost]
        public ActionResult GlobalReleaseReadyEdits(PartSetUpViewModel partsetup_, FormCollection result)
     {
   //this action saves the the drawing file path for the selected part set ups
          if (ModelState["PartSetUp.CategoryID"] != null)
              ModelState["PartSetUp.CategoryID"].Errors.Clear();

          if (ModelState["PartSetUp.PartID"] != null)
              ModelState["PartSetUp.PartID"].Errors.Clear();
          if (ModelState["PartSetUp.PackCode"] != null)
              ModelState["PartSetUp.PackCode"].Errors.Clear();

          if (ModelState["PartSetUp.DrawingNumber"] != null)
              ModelState["PartSetUp.DrawingNumber"].Errors.Clear();

          if (ModelState["PartSetUp.Revision"] != null)
              ModelState["PartSetUp.Revision"].Errors.Clear();
          if (ModelState["PartSetUp.IsReleaseReady"] != null)
              ModelState["PartSetUp.IsReleaseReady"].Errors.Clear();

          if (ModelState["PartSetUp.PartSetUpID"] != null)
              ModelState["PartSetUp.PartSetUpID"].Errors.Clear();

          if (ModelState["PartSetUp.DrawingFile"] != null)
              ModelState["PartSetUp.DrawingFile"].Errors.Clear();
          
              var partsetupViewModel = new PartSetUpViewModel
          {
            

          };

          var selecteditems = from x in result.AllKeys
                              where result[x] != "false"

                              select x;

          foreach (var item in selecteditems)
          {
              if (item.Contains("PartSetUptoUpdate_"))
              {
                  GetUserInfo();
                  string theitem = item.ToString();

                  theitem = theitem.Replace("PartSetUptoUpdate_", "");
                  int partsetupidtomodify = Convert.ToInt16(theitem);
                  DateTime lasteditdate = DateTime.Now;
                  string lasteditedby = username;
                  bool releaseready = partsetup_.IsCurrentlyReleaseReady;

                  partsetupViewModel.PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == partsetupidtomodify);

                  var partsetup = partsetupViewModel.PartSetUp;

                  partsetup.IsReleaseReady = releaseready; 
                  partsetup.LastEditBy = lasteditedby;
                  partsetup.LastEditDate = lasteditdate;
                  if (releaseready)
                  {
                      partsetup.LastApprover = lasteditedby;
                      partsetup.LastApprovalDateTime = lasteditdate;
                  }
                  else {

                      partsetup.LastApprover = null;
                      partsetup.LastApprovalDateTime = null;
                  }



                      _partsetupRepository.UpdateReleaseReadyEdits(partsetup);
                  partsetup = null;
                  this.ShowSaveSuccessfull();




              }
           

          }
         bool canuseredit=CanUserEdit();
          bool canuserapprove=CanUserApprove();

        

      
          var viewModel = new PartSetUpViewModel
          {
              PartSetUps = _partsetupRepository.PartSetUp,
              AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
              PartSpecifications = _partspecificationRepository.PartSpecification,
              TravelCards = _travelcardRepository.TravelCard,
              PartCategories = _partCategoryRepository.PartCategory,
              CanUserEdit = canuseredit,
              CanUserApprove=canuserapprove,
              HasSearchCriteria = false
          };


          if (partsetup_.PartSetUp.PartID != null)
          {

              string partidsearchcriteria = partsetup_.PartSetUp.PartID.Trim();
              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartID.Contains(partidsearchcriteria));
              viewModel.HasSearchCriteria = true;
          }

          if (partsetup_.PartSetUp.PartSetUpID != 0)
          {

              int setupidsearchcriteria = partsetup_.PartSetUp.PartSetUpID;
              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartSetUpID == setupidsearchcriteria);
              viewModel.HasSearchCriteria = true;
          }


          int? categoryidsearch = partsetup_.PartSetUp.CategoryID;
          if (categoryidsearch != 0)
          {

              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.CategoryID == categoryidsearch);
              viewModel.HasSearchCriteria = true;


          }

          if (partsetup_.PartSetUp.PackCode != null)
          {
              string packcodesearch = partsetup_.PartSetUp.PackCode.Trim().ToUpper();
              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PackCode == packcodesearch);
              viewModel.HasSearchCriteria = true;
          }

          if (partsetup_.PartSetUp.DrawingFile != null)
          {
              string drawingfilesearch = partsetup_.PartSetUp.DrawingFile.Trim();

              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingFile == drawingfilesearch.Replace("//", "/"));
              viewModel.HasSearchCriteria = true;
          }


          if (partsetup_.PartSetUp.DrawingNumber != null)
          {
              string drawingnumbersearch = partsetup_.PartSetUp.DrawingNumber.Trim().ToUpper();
              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingNumber == drawingnumbersearch);
              viewModel.HasSearchCriteria = true;
          }
          if (partsetup_.PartSetUp.Revision != null)
          {
              string revisionsearch = partsetup_.PartSetUp.Revision.Trim().ToUpper();
              viewModel.PartSetUps = viewModel.PartSetUps.Where(c => c.Revision == revisionsearch);
              viewModel.HasSearchCriteria = true;
          }

          if (partsetup_.PartSetUp.IsReleaseReady)
          {
              bool releasereadysearch;
              releasereadysearch = partsetup_.PartSetUp.IsReleaseReady;
              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.IsReleaseReady == true);
              viewModel.HasSearchCriteria = true;
          }


                return View("PartSetUpGlobalReleaseReadyEditor", viewModel);
      }



     [HttpPost]
     public ActionResult GlobalQualityAlertFileEdits(PartSetUpViewModel partsetup_, FormCollection result)
     {

         //this action saves the the drawing file path for the selected part set ups
         if (ModelState["PartSetUp.CategoryID"] != null)
             ModelState["PartSetUp.CategoryID"].Errors.Clear();

         if (ModelState["PartSetUp.PartID"] != null)
             ModelState["PartSetUp.PartID"].Errors.Clear();
         if (ModelState["PartSetUp.PackCode"] != null)
             ModelState["PartSetUp.PackCode"].Errors.Clear();

         if (ModelState["PartSetUp.DrawingNumber"] != null)
             ModelState["PartSetUp.DrawingNumber"].Errors.Clear();

         if (ModelState["PartSetUp.Revision"] != null)
             ModelState["PartSetUp.Revision"].Errors.Clear();
         if (ModelState["PartSetUp.IsReleaseReady"] != null)
             ModelState["PartSetUp.IsReleaseReady"].Errors.Clear();

         if (ModelState["PartSetUp.PartSetUpID"] != null)
             ModelState["PartSetUp.PartSetUpID"].Errors.Clear();

         if (ModelState["PartSetUp.DrawingFile"] != null)
             ModelState["PartSetUp.DrawingFile"].Errors.Clear();

         var partsetupViewModel = new PartSetUpViewModel
         {


         };
         //buildPath, browse controls for chrome and firefox don't pick the path, only internet explorer does


         string savelocation = ConfigurationManager.AppSettings["DeviationFile"];

         string pdfextension = ".pdf";

         string qualityalertfilepath = (partsetup_.QualityAlertFilePath != null) ? partsetup_.QualityAlertFilePath : "";
         DateTime? start = (partsetup_.PartSetUp.QualityAlertStartDte != null) ? partsetup_.PartSetUp.QualityAlertStartDte : null;
         DateTime? end = (partsetup_.PartSetUp.QualityAlertEndDte != null) ? partsetup_.PartSetUp.QualityAlertEndDte : null;

         int length = qualityalertfilepath.Length;




         if (qualityalertfilepath != "")
         {
             qualityalertfilepath = qualityalertfilepath.ToLower();

             if (!qualityalertfilepath.ToLower().Contains(pdfextension))
             {
                 string errormessage = String.Format("* Deviation file must be saved in .pdf format in the {0} location.", savelocation);
                 partsetup_.UserMessage = errormessage;
                 partsetup_.QualityAlertFilePath = qualityalertfilepath;


                 return View(partsetup_);
             }
         }
         string formattedfilepath = "";

         if (partsetup_.QualityAlertFileDelete)
         {
             qualityalertfilepath = null;
             end = null;
             start = null;
         }
         string name = username;

         //do the saving

         if (qualityalertfilepath == null || qualityalertfilepath.Length == 0)
         {

             formattedfilepath = qualityalertfilepath = null;
         }
         else
         {
             qualityalertfilepath = Path.GetFileName(qualityalertfilepath);

             formattedfilepath = qualityalertfilepath = savelocation + qualityalertfilepath;
         }



         var selecteditems = from x in result.AllKeys
                             where result[x] != "false"

                             select x;


         foreach (var item in selecteditems)
         {
             if (item.Contains("PartSetUptoUpdate_"))
             {
                 GetUserInfo();
                 string theitem = item.ToString();

                 theitem = theitem.Replace("PartSetUptoUpdate_", "");


                 int partsetupidtomodify = Convert.ToInt16(theitem);
                 var qualityalertfilename = formattedfilepath;

                 DateTime lasteditdate = DateTime.Now;
                 string lasteditedby = username;
                 bool setpartsetupqualityalertfiletonull = partsetup_.QualityAlertFileDelete;
                 if (setpartsetupqualityalertfiletonull)
                 {
                     qualityalertfilename = null;
                 }

                 partsetupViewModel.PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == partsetupidtomodify);

                 var partsetup = partsetupViewModel.PartSetUp;

                 partsetup.QualityAlertFile = qualityalertfilename;

                 partsetup.QualityAlertStartDte = start;
                 partsetup.QualityAlertEndDte = end;
                 partsetup.LastEditBy = username;
                 partsetup.LastEditDate = DateTime.Now;



                 _partsetupRepository.UpdateQualityAlertFile1Edits(partsetup);
                 partsetup = null;
                 this.ShowSaveSuccessfull();




             }


         }
         bool canuseredit = CanUserEdit();
         bool canuserapprove = CanUserApprove();




         var viewModel = new PartSetUpViewModel
         {
             PartSetUps = _partsetupRepository.PartSetUp,
             AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
             PartSpecifications = _partspecificationRepository.PartSpecification,
             TravelCards = _travelcardRepository.TravelCard,
             PartCategories = _partCategoryRepository.PartCategory,
             CanUserEdit = canuseredit,
             CanUserApprove = canuserapprove,
             HasSearchCriteria = false
         };


         if (partsetup_.PartSetUp.PartID != null)
         {

             string partidsearchcriteria = partsetup_.PartSetUp.PartID.Trim();
             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartID.Contains(partidsearchcriteria));
             viewModel.HasSearchCriteria = true;
         }

         if (partsetup_.PartSetUp.PartSetUpID != 0)
         {

             int setupidsearchcriteria = partsetup_.PartSetUp.PartSetUpID;
             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartSetUpID == setupidsearchcriteria);
             viewModel.HasSearchCriteria = true;
         }


         int? categoryidsearch = partsetup_.PartSetUp.CategoryID;
         if (categoryidsearch != 0)
         {

             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.CategoryID == categoryidsearch);
             viewModel.HasSearchCriteria = true;


         }

         if (partsetup_.PartSetUp.PackCode != null)
         {
             string packcodesearch = partsetup_.PartSetUp.PackCode.Trim().ToUpper();
             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PackCode == packcodesearch);
             viewModel.HasSearchCriteria = true;
         }

         if (partsetup_.PartSetUp.DrawingFile != null)
         {
             string drawingfilesearch = partsetup_.PartSetUp.DrawingFile.Trim();

             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingFile == drawingfilesearch.Replace("//", "/"));
             viewModel.HasSearchCriteria = true;
         }

         if (partsetup_.PartSetUp.QualityAlertFile != null)
         {
             string qualityAlertFileSearch = partsetup_.PartSetUp.QualityAlertFile.Trim();

             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.QualityAlertFile == qualityAlertFileSearch.Replace("//", "/"));
             viewModel.HasSearchCriteria = true;
         }




         if (partsetup_.PartSetUp.DrawingNumber != null)
         {
             string drawingnumbersearch = partsetup_.PartSetUp.DrawingNumber.Trim().ToUpper();
             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingNumber == drawingnumbersearch);
             viewModel.HasSearchCriteria = true;
         }
         if (partsetup_.PartSetUp.Revision != null)
         {
             string revisionsearch = partsetup_.PartSetUp.Revision.Trim().ToUpper();
             viewModel.PartSetUps = viewModel.PartSetUps.Where(c => c.Revision == revisionsearch);
             viewModel.HasSearchCriteria = true;
         }

         if (partsetup_.PartSetUp.IsReleaseReady)
         {
             bool releasereadysearch;
             releasereadysearch = partsetup_.PartSetUp.IsReleaseReady;
             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.IsReleaseReady == true);
             viewModel.HasSearchCriteria = true;
         }


         return View("PartSetUpGlobalQualityAlertFileEditor", viewModel);
     }














     [HttpPost]
     public ActionResult GlobalDeviationFileEdits(PartSetUpViewModel partsetup_, FormCollection result)
     {

         //this action saves the the drawing file path for the selected part set ups
         if (ModelState["PartSetUp.CategoryID"] != null)
             ModelState["PartSetUp.CategoryID"].Errors.Clear();

         if (ModelState["PartSetUp.PartID"] != null)
             ModelState["PartSetUp.PartID"].Errors.Clear();
         if (ModelState["PartSetUp.PackCode"] != null)
             ModelState["PartSetUp.PackCode"].Errors.Clear();

         if (ModelState["PartSetUp.DrawingNumber"] != null)
             ModelState["PartSetUp.DrawingNumber"].Errors.Clear();

         if (ModelState["PartSetUp.Revision"] != null)
             ModelState["PartSetUp.Revision"].Errors.Clear();
         if (ModelState["PartSetUp.IsReleaseReady"] != null)
             ModelState["PartSetUp.IsReleaseReady"].Errors.Clear();

         if (ModelState["PartSetUp.PartSetUpID"] != null)
             ModelState["PartSetUp.PartSetUpID"].Errors.Clear();

         if (ModelState["PartSetUp.DrawingFile"] != null)
             ModelState["PartSetUp.DrawingFile"].Errors.Clear();

         var partsetupViewModel = new PartSetUpViewModel
         {


         };
         //buildPath, browse controls for chrome and firefox don't pick the path, only internet explorer does


         string savelocation = ConfigurationManager.AppSettings["DeviationFile"];

         string pdfextension = ".pdf";

         string deviationfilepath = (partsetup_.DeviationFilePath != null) ? partsetup_.DeviationFilePath : "";
         DateTime? start = (partsetup_.PartSetUp.DeviationFileStartDte != null) ? partsetup_.PartSetUp.DeviationFileStartDte: null;
         DateTime? end = (partsetup_.PartSetUp.DeviationFileEndDte != null) ? partsetup_.PartSetUp.DeviationFileEndDte : null;
       
         int length = deviationfilepath.Length;




         if (deviationfilepath != "")
         {
             deviationfilepath = deviationfilepath.ToLower();

             if (!deviationfilepath.ToLower().Contains(pdfextension))
             {
                 string errormessage = String.Format("* Deviation file must be saved in .pdf format in the {0} location.", savelocation);
                 partsetup_.UserMessage = errormessage;
                 partsetup_.DeviationFilePath = deviationfilepath;


                 return View(partsetup_);
             }
         }
         string formattedfilepath = "";

         if (partsetup_.DeviationFileDelete)
         {
             deviationfilepath = null;
             end = null;
             start = null;
         }
         string name = username;

         //do the saving

         if (deviationfilepath == null || deviationfilepath.Length == 0)
         {

             formattedfilepath = deviationfilepath = null;
         }
         else
         {
             deviationfilepath = Path.GetFileName(deviationfilepath);

             formattedfilepath = deviationfilepath = savelocation + deviationfilepath;
         }



         var selecteditems = from x in result.AllKeys
                             where result[x] != "false"

                             select x;


         foreach (var item in selecteditems)
         {
             if (item.Contains("PartSetUptoUpdate_"))
             {
                 GetUserInfo();
                 string theitem = item.ToString();

                 theitem = theitem.Replace("PartSetUptoUpdate_", "");


                 int partsetupidtomodify = Convert.ToInt16(theitem);
                 var deviationfilename = formattedfilepath;

                 DateTime lasteditdate = DateTime.Now;
                 string lasteditedby = username;
                 bool setpartsetupdeviationfilettonull = partsetup_.DeviationFileDelete;
                 if (setpartsetupdeviationfilettonull)
                 {
                     deviationfilename = null;
                 }

                 partsetupViewModel.PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == partsetupidtomodify);

                 var partsetup = partsetupViewModel.PartSetUp;

                 partsetup.DeviationFile = deviationfilename;
              
                 partsetup.DeviationFileStartDte = start;
                 partsetup.DeviationFileEndDte = end;
                 partsetup.LastEditBy = username;
                 partsetup.LastEditDate = DateTime.Now;



                 _partsetupRepository.UpdateDeviationFile1Edits(partsetup);
                 partsetup = null;
                 this.ShowSaveSuccessfull();




             }


         }
         bool canuseredit = CanUserEdit();
         bool canuserapprove = CanUserApprove();




         var viewModel = new PartSetUpViewModel
         {
             PartSetUps = _partsetupRepository.PartSetUp,
             AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
             PartSpecifications = _partspecificationRepository.PartSpecification,
             TravelCards = _travelcardRepository.TravelCard,
             PartCategories = _partCategoryRepository.PartCategory,
             CanUserEdit = canuseredit,
             CanUserApprove = canuserapprove,
             HasSearchCriteria = false
         };


         if (partsetup_.PartSetUp.PartID != null)
         {

             string partidsearchcriteria = partsetup_.PartSetUp.PartID.Trim();
             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartID.Contains(partidsearchcriteria));
             viewModel.HasSearchCriteria = true;
         }

         if (partsetup_.PartSetUp.PartSetUpID != 0)
         {

             int setupidsearchcriteria = partsetup_.PartSetUp.PartSetUpID;
             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartSetUpID == setupidsearchcriteria);
             viewModel.HasSearchCriteria = true;
         }


         int? categoryidsearch = partsetup_.PartSetUp.CategoryID;
         if (categoryidsearch != 0)
         {

             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.CategoryID == categoryidsearch);
             viewModel.HasSearchCriteria = true;


         }

         if (partsetup_.PartSetUp.PackCode != null)
         {
             string packcodesearch = partsetup_.PartSetUp.PackCode.Trim().ToUpper();
             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PackCode == packcodesearch);
             viewModel.HasSearchCriteria = true;
         }

         if (partsetup_.PartSetUp.DrawingFile != null)
         {
             string drawingfilesearch = partsetup_.PartSetUp.DrawingFile.Trim();

             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingFile == drawingfilesearch.Replace("//", "/"));
             viewModel.HasSearchCriteria = true;
         }


         if (partsetup_.PartSetUp.DrawingNumber != null)
         {
             string drawingnumbersearch = partsetup_.PartSetUp.DrawingNumber.Trim().ToUpper();
             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingNumber == drawingnumbersearch);
             viewModel.HasSearchCriteria = true;
         }
         if (partsetup_.PartSetUp.Revision != null)
         {
             string revisionsearch = partsetup_.PartSetUp.Revision.Trim().ToUpper();
             viewModel.PartSetUps = viewModel.PartSetUps.Where(c => c.Revision == revisionsearch);
             viewModel.HasSearchCriteria = true;
         }

         if (partsetup_.PartSetUp.IsReleaseReady)
         {
             bool releasereadysearch;
             releasereadysearch = partsetup_.PartSetUp.IsReleaseReady;
             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.IsReleaseReady == true);
             viewModel.HasSearchCriteria = true;
         }


         return View("PartSetUpGlobalDeviationFileEditor", viewModel);
     }



     [HttpPost]
     public ActionResult GlobalDeviationFile2Edits(PartSetUpViewModel partsetup_, FormCollection result)
     {

         //this action saves the the drawing file path for the selected part set ups
         if (ModelState["PartSetUp.CategoryID"] != null)
             ModelState["PartSetUp.CategoryID"].Errors.Clear();

         if (ModelState["PartSetUp.PartID"] != null)
             ModelState["PartSetUp.PartID"].Errors.Clear();
         if (ModelState["PartSetUp.PackCode"] != null)
             ModelState["PartSetUp.PackCode"].Errors.Clear();

         if (ModelState["PartSetUp.DrawingNumber"] != null)
             ModelState["PartSetUp.DrawingNumber"].Errors.Clear();

         if (ModelState["PartSetUp.Revision"] != null)
             ModelState["PartSetUp.Revision"].Errors.Clear();
         if (ModelState["PartSetUp.IsReleaseReady"] != null)
             ModelState["PartSetUp.IsReleaseReady"].Errors.Clear();

         if (ModelState["PartSetUp.PartSetUpID"] != null)
             ModelState["PartSetUp.PartSetUpID"].Errors.Clear();

         if (ModelState["PartSetUp.DrawingFile"] != null)
             ModelState["PartSetUp.DrawingFile"].Errors.Clear();

         var partsetupViewModel = new PartSetUpViewModel
         {


         };
         //buildPath, browse controls for chrome and firefox don't pick the path, only internet explorer does


         string savelocation = ConfigurationManager.AppSettings["DeviationFile"];

         string pdfextension = ".pdf";

         string deviationfilepath = (partsetup_.DeviationFile2Path != null) ? partsetup_.DeviationFile2Path : "";
         DateTime? start = (partsetup_.PartSetUp.DeviationFile2StartDte != null) ? partsetup_.PartSetUp.DeviationFile2StartDte : null;
         DateTime? end = (partsetup_.PartSetUp.DeviationFile2EndDte != null) ? partsetup_.PartSetUp.DeviationFile2EndDte : null;

         int length = deviationfilepath.Length;




         if (deviationfilepath != "")
         {
             deviationfilepath = deviationfilepath.ToLower();

             if (!deviationfilepath.ToLower().Contains(pdfextension))
             {
                 string errormessage = String.Format("* Deviation file must be saved in .pdf format in the {0} location.", savelocation);
                 partsetup_.UserMessage = errormessage;
                 partsetup_.DeviationFilePath = deviationfilepath;


                 return View(partsetup_);
             }
         }
         string formattedfilepath = "";

         if (partsetup_.DeviationFileDelete)
         {
             deviationfilepath = null;
             end = null;
             start = null;
         }
         string name = username;

         //do the saving

         if (deviationfilepath == null || deviationfilepath.Length == 0)
         {

             formattedfilepath = deviationfilepath = null;
         }
         else
         {
             deviationfilepath = Path.GetFileName(deviationfilepath);

             formattedfilepath = deviationfilepath = savelocation + deviationfilepath;
         }



         var selecteditems = from x in result.AllKeys
                             where result[x] != "false"

                             select x;


         foreach (var item in selecteditems)
         {
             if (item.Contains("PartSetUptoUpdate_"))
             {
                 GetUserInfo();
                 string theitem = item.ToString();

                 theitem = theitem.Replace("PartSetUptoUpdate_", "");


                 int partsetupidtomodify = Convert.ToInt16(theitem);
                 var deviationfilename = formattedfilepath;

                 DateTime lasteditdate = DateTime.Now;
                 string lasteditedby = username;
                 bool setpartsetupdeviationfilettonull = partsetup_.DeviationFileDelete;
                 if (setpartsetupdeviationfilettonull)
                 {
                     deviationfilename = null;
                 }

                 partsetupViewModel.PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == partsetupidtomodify);

                 var partsetup = partsetupViewModel.PartSetUp;

                 partsetup.DeviationFile2 = deviationfilename;

                 partsetup.DeviationFile2StartDte = start;
                 partsetup.DeviationFile2EndDte = end;
                 partsetup.LastEditBy = username;
                 partsetup.LastEditDate = DateTime.Now;



                 _partsetupRepository.UpdateDeviationFile2Edits(partsetup);
                 partsetup = null;
                 this.ShowSaveSuccessfull();




             }


         }
         bool canuseredit = CanUserEdit();
         bool canuserapprove = CanUserApprove();




         var viewModel = new PartSetUpViewModel
         {
             PartSetUps = _partsetupRepository.PartSetUp,
             AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
             PartSpecifications = _partspecificationRepository.PartSpecification,
             TravelCards = _travelcardRepository.TravelCard,
             PartCategories = _partCategoryRepository.PartCategory,
             CanUserEdit = canuseredit,
             CanUserApprove = canuserapprove,
             HasSearchCriteria = false
         };


         if (partsetup_.PartSetUp.PartID != null)
         {

             string partidsearchcriteria = partsetup_.PartSetUp.PartID.Trim();
             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartID.Contains(partidsearchcriteria));
             viewModel.HasSearchCriteria = true;
         }

         if (partsetup_.PartSetUp.PartSetUpID != 0)
         {

             int setupidsearchcriteria = partsetup_.PartSetUp.PartSetUpID;
             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartSetUpID == setupidsearchcriteria);
             viewModel.HasSearchCriteria = true;
         }


         int? categoryidsearch = partsetup_.PartSetUp.CategoryID;
         if (categoryidsearch != 0)
         {

             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.CategoryID == categoryidsearch);
             viewModel.HasSearchCriteria = true;


         }

         if (partsetup_.PartSetUp.PackCode != null)
         {
             string packcodesearch = partsetup_.PartSetUp.PackCode.Trim().ToUpper();
             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PackCode == packcodesearch);
             viewModel.HasSearchCriteria = true;
         }

         if (partsetup_.PartSetUp.DrawingFile != null)
         {
             string drawingfilesearch = partsetup_.PartSetUp.DrawingFile.Trim();

             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingFile == drawingfilesearch.Replace("//", "/"));
             viewModel.HasSearchCriteria = true;
         }


         if (partsetup_.PartSetUp.DrawingNumber != null)
         {
             string drawingnumbersearch = partsetup_.PartSetUp.DrawingNumber.Trim().ToUpper();
             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingNumber == drawingnumbersearch);
             viewModel.HasSearchCriteria = true;
         }
         if (partsetup_.PartSetUp.Revision != null)
         {
             string revisionsearch = partsetup_.PartSetUp.Revision.Trim().ToUpper();
             viewModel.PartSetUps = viewModel.PartSetUps.Where(c => c.Revision == revisionsearch);
             viewModel.HasSearchCriteria = true;
         }

         if (partsetup_.PartSetUp.IsReleaseReady)
         {
             bool releasereadysearch;
             releasereadysearch = partsetup_.PartSetUp.IsReleaseReady;
             viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.IsReleaseReady == true);
             viewModel.HasSearchCriteria = true;
         }


         return View("PartSetUpGlobalDeviation2FileEditor", viewModel);
     }




      [HttpPost]
      public ActionResult GlobalDrawingFileEdits(PartSetUpViewModel partsetup_, FormCollection result)
      {

          //this action saves the the drawing file path for the selected part set ups
          if (ModelState["PartSetUp.CategoryID"] != null)
              ModelState["PartSetUp.CategoryID"].Errors.Clear();

          if (ModelState["PartSetUp.PartID"] != null)
              ModelState["PartSetUp.PartID"].Errors.Clear();
          if (ModelState["PartSetUp.PackCode"] != null)
              ModelState["PartSetUp.PackCode"].Errors.Clear();

          if (ModelState["PartSetUp.DrawingNumber"] != null)
              ModelState["PartSetUp.DrawingNumber"].Errors.Clear();

          if (ModelState["PartSetUp.Revision"] != null)
              ModelState["PartSetUp.Revision"].Errors.Clear();
          if (ModelState["PartSetUp.IsReleaseReady"] != null)
              ModelState["PartSetUp.IsReleaseReady"].Errors.Clear();

          if (ModelState["PartSetUp.PartSetUpID"] != null)
              ModelState["PartSetUp.PartSetUpID"].Errors.Clear();

          if (ModelState["PartSetUp.DrawingFile"] != null)
              ModelState["PartSetUp.DrawingFile"].Errors.Clear();
          
              var partsetupViewModel = new PartSetUpViewModel
          {
            

          };
//buildPath, browse controls for chrome and firefox don't pick the path, only internet explorer does


              string savelocation = ConfigurationManager.AppSettings["DrawingFile"];

              string pdfextension = ".pdf";

              string drawingfilepath = (partsetup_.DrawingFilePath != null) ? partsetup_.DrawingFilePath : "";
              int length = drawingfilepath.Length;




              if (drawingfilepath != "")
              {
                  drawingfilepath = drawingfilepath.ToLower();

                  if (!drawingfilepath.ToLower().Contains(pdfextension))
                  {
                      string errormessage = String.Format("* Drawing file must be saved in .pdf format in the {0} location.", savelocation);
                      partsetup_.UserMessage = errormessage;
                      partsetup_.DrawingFilePath = drawingfilepath;


                      return View(partsetup_);
                  }
              }
              string formattedfilepath = "";

              if (partsetup_.DrawingFileDelete)
              {
                  drawingfilepath = null;
              }
              string name = username;

              //do the saving
            
              if (drawingfilepath == null||drawingfilepath.Length==0)
              {
               
                  formattedfilepath = drawingfilepath = null;
              }
              else
              {
                  drawingfilepath = Path.GetFileName(drawingfilepath);
                
                formattedfilepath = drawingfilepath = savelocation + drawingfilepath;
              }



          var selecteditems = from x in result.AllKeys
                              where result[x] != "false"

                              select x;


          foreach (var item in selecteditems)
          {
              if (item.Contains("PartSetUptoUpdate_"))
              {
                  GetUserInfo();
                  string theitem = item.ToString();

                  theitem = theitem.Replace("PartSetUptoUpdate_", "");


                  int partsetupidtomodify = Convert.ToInt16(theitem);
                  var drawingfilename = formattedfilepath;

                  DateTime lasteditdate = DateTime.Now;
                  string lasteditedby = username;
                  bool setpartsetupdrawingfilettonull = partsetup_.DrawingFileDelete;
                  if (setpartsetupdrawingfilettonull)
                  {
                      drawingfilename = null;
                  }

                  partsetupViewModel.PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == partsetupidtomodify);

                var partsetup=partsetupViewModel.PartSetUp;

                  partsetup.DrawingFile = drawingfilename;
                  partsetup.LastEditBy = username;
                  partsetup.LastEditDate = DateTime.Now;
                  _partsetupRepository.UpdateDrawingFileEdits(partsetup);
                  partsetup = null;
                  this.ShowSaveSuccessfull();
              }
           

          }
          bool canuseredit=CanUserEdit();
          bool canuserapprove=CanUserApprove();

          var viewModel = new PartSetUpViewModel
          {
              PartSetUps = _partsetupRepository.PartSetUp,
              AdditionalProcesses = _additionalprocessingRepository.AdditionalProcess,
              PartSpecifications = _partspecificationRepository.PartSpecification,
              TravelCards = _travelcardRepository.TravelCard,
              PartCategories = _partCategoryRepository.PartCategory,
              CanUserEdit = canuseredit,
              CanUserApprove=canuserapprove,
              HasSearchCriteria = false
          };


          if (partsetup_.PartSetUp.PartID != null)
          {

              string partidsearchcriteria = partsetup_.PartSetUp.PartID.Trim();
              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartID.Contains(partidsearchcriteria));
              viewModel.HasSearchCriteria = true;
          }

          if (partsetup_.PartSetUp.PartSetUpID != 0)
          {

              int setupidsearchcriteria = partsetup_.PartSetUp.PartSetUpID;
              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PartSetUpID == setupidsearchcriteria);
              viewModel.HasSearchCriteria = true;
          }


          int? categoryidsearch = partsetup_.PartSetUp.CategoryID;
          if (categoryidsearch != 0)
          {

              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.CategoryID == categoryidsearch);
              viewModel.HasSearchCriteria = true;


          }

          if (partsetup_.PartSetUp.PackCode != null)
          {
              string packcodesearch = partsetup_.PartSetUp.PackCode.Trim().ToUpper();
              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.PackCode == packcodesearch);
              viewModel.HasSearchCriteria = true;
          }

          if (partsetup_.PartSetUp.DrawingFile != null)
          {
              string drawingfilesearch = partsetup_.PartSetUp.DrawingFile.Trim();

              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingFile == drawingfilesearch.Replace("//", "/"));
              viewModel.HasSearchCriteria = true;
          }


          if (partsetup_.PartSetUp.DrawingNumber != null)
          {
              string drawingnumbersearch = partsetup_.PartSetUp.DrawingNumber.Trim().ToUpper();
              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.DrawingNumber == drawingnumbersearch);
              viewModel.HasSearchCriteria = true;
          }
          if (partsetup_.PartSetUp.Revision != null)
          {
              string revisionsearch = partsetup_.PartSetUp.Revision.Trim().ToUpper();
              viewModel.PartSetUps = viewModel.PartSetUps.Where(c => c.Revision == revisionsearch);
              viewModel.HasSearchCriteria = true;
          }

          if (partsetup_.PartSetUp.IsReleaseReady)
          {
              bool releasereadysearch;
              releasereadysearch = partsetup_.PartSetUp.IsReleaseReady;
              viewModel.PartSetUps = viewModel.PartSetUps.Where(a => a.IsReleaseReady == true);
              viewModel.HasSearchCriteria = true;
          }


                return View("PartSetUpGlobalDrawingFileEditor", viewModel);
      }


        [HttpPost]
        public ActionResult PartSetUpEdit(PartSetUpViewModel viewModel_)


        {

            if (ModelState.IsValid)
            {
                GetUserInfo();
                if (viewModel_.PartSetUp.CategoryID==0)
                {

                    ModelState.AddModelError("PartSetUpCreate", "Part Category is required.");
                } 
               
                string itemid = viewModel_.ItemID;

             //  viewModel_.PartSetUp.CreateDate = DateTime.Now;
               viewModel_.PartSetUp.LastEditDate = DateTime.Now;
               string packcode = viewModel_.PartSetUp.PackCode != null ? viewModel_.PartSetUp.PackCode.ToUpper() : "";
               string drawingnumber = viewModel_.PartSetUp.DrawingNumber != null ? viewModel_.PartSetUp.DrawingNumber.ToUpper() : "";
               string revision = viewModel_.PartSetUp.Revision != null ? viewModel_.PartSetUp.Revision.ToUpper() : "";

               //if the current record is not release ready and the checkbox has been checked then record the user setting the release flag
                if (viewModel_.PartSetUp.IsReleaseReady&&viewModel_.IsCurrentlyReleaseReady==false)
                {
                    viewModel_.PartSetUp.LastApprovalDateTime = DateTime.Now;
                    viewModel_.PartSetUp.LastApprover = username;
                
                }
               //record the release ready flag has been set to false
                if (viewModel_.PartSetUp.IsReleaseReady==false && viewModel_.IsCurrentlyReleaseReady)
                {
                    viewModel_.PartSetUp.LastApprovalDateTime = DateTime.Now;
                    viewModel_.PartSetUp.LastApprover = username;

                }
                 viewModel_.PartSetUp.Notes = viewModel_.PartSetUp.Notes;
                 viewModel_.PartSetUp.CommunicationNote = viewModel_.PartSetUp.CommunicationNote;
                 viewModel_.PartSetUp.PartRemarks = viewModel_.PartSetUp.PartRemarks;
                 viewModel_.PartSetUp.PartComment = viewModel_.PartSetUp.PartComment;
                 viewModel_.PartSetUp.Revision = revision;
                 viewModel_.PartSetUp.DrawingNumber = drawingnumber;
                 viewModel_.PartSetUp.PackCode = packcode;
                 viewModel_.PartSetUp.LastEditBy = username;
                 _partsetupRepository.Update(viewModel_.PartSetUp);
                  viewModel_.PartCategories = _partCategoryRepository.PartCategory;
               
                 this.ShowSaveSuccessfull();
                  viewModel_.ItemID = viewModel_.PartSetUp.PartID;
                 Int16 partsetupid = viewModel_.PartSetUp.PartSetUpID;
              
                 //send user back to partmaintenance index
                  TravelCardViewModel viewModelTC = new TravelCardViewModel
                  {
                      ItemID = itemid,
                      ShowPartSetUpDetails=true,
                      PartSetUpID = partsetupid
                  };
                  return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);

            }
            else
            {

                viewModel_.PartCategories = _partCategoryRepository.PartCategory;
                return View(viewModel_);
            }
          
        }

    }
}
