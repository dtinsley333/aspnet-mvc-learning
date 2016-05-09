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

namespace Quality.Controllers
{
    public class AdditionalProcessesController : BaseController
    {
        IAdditionalProcessingRepository _additionalprocessesRepository;
        IPartSetUpRepository _partsetupRepository;
        public AdditionalProcessesController(IAdditionalProcessingRepository additionalprocessesRepository_,IPartSetUpRepository partsetupRepository_)
        {
            _additionalprocessesRepository = additionalprocessesRepository_;
            _partsetupRepository = partsetupRepository_;
        
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PickAdditionalProcessestoClone(AdditionalProcessingViewModel cloneViewModel)
        {
            return View(cloneViewModel);
        }

        [HttpPost]
        public ActionResult SaveClonedProcesses(AdditionalProcessingViewModel viewModel,FormCollection result)
        {

           string itemid = viewModel.ItemID;

            Int16 partsetupid = viewModel.PartSetUp.PartSetUpID;

            GetUserInfo();
            try
            {
           
               var selecteditems=from x in result.AllKeys
                                  where result[x]!="false"
                                 
                                  select x;

                
                foreach (var item in selecteditems)
                {
                    if (item.Contains("ProcesstoClone_"))
                    {
                    
                       string theitem = item.ToString();
                     
                       theitem=theitem.Replace("ProcesstoClone_", "");
                      
                       
                            int theprocesstoclone = Convert.ToInt16(theitem);
                            var processtoclone = _additionalprocessesRepository.AdditionalProcess.FirstOrDefault(a => a.ProcessingID == theprocesstoclone);
                            string descriptionEnglish = processtoclone.Description;
                            string sequenceid = processtoclone.SequenceID;
                            string descriptionSpanish = processtoclone.DescriptionES;
                            string notes = "";
                            DateTime lasteditdate = DateTime.Now;
                            string lasteditedby = username;
                             _additionalprocessesRepository.InsertClonedAdditionalProcess(partsetupid, descriptionEnglish, descriptionSpanish, notes,sequenceid, lasteditedby, lasteditdate);
                            processtoclone = null;
                        this.ShowSaveSuccessfull();

                        }

                    }
              
              
                //send user back to partmaintenance index
                TravelCardViewModel viewModelTC = new TravelCardViewModel
                {
                    ItemID = itemid,
                    ShowPartSetUpDetails=true
                };
                return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);
            }
            catch (Exception ex)
            {
            //TODO: did not save. deal with error
                return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard");
            }
        }

        [HttpPost]
        public ActionResult CloneAdditionalProcesses(AdditionalProcessingViewModel viewModel)
        {

            if (viewModel.PartSetUp.PartID==String.Empty||viewModel.PartSetUp.PartID==null)
            {
       
                ModelState.AddModelError("AdditionalProcessesClone", "Please select a part set up to clone from.");
            } 
            
            if (ModelState.IsValid)
            {

                GetUserInfo();
                bool canuseredit = CanUserEdit();
                string parttoclonefrom = viewModel.PartSetUp.PartID;
                string parttocloneto = viewModel.ItemID;
                int partsetupid_ = viewModel.PartSetUp.PartSetUpID;


                var partsetuptoclonefrom = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartID == parttoclonefrom);
                int partsetupIDtoclonefrom = partsetuptoclonefrom.PartSetUpID;
                var additionalprocessestoclone = _additionalprocessesRepository.AdditionalProcess.Where(a => a.PartSetUpID == partsetupIDtoclonefrom).Where(c => c.IsActive).ToList();


                AdditionalProcessingViewModel cloneViewModel = new AdditionalProcessingViewModel
                {

                    PartSetupToCloneFrom = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartID == parttoclonefrom),
                    PartSetupToCloneTo = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartID == parttocloneto),
                    AdditionalProcesses = _additionalprocessesRepository.AdditionalProcess.Where(b => b.IsActive == true).OrderBy(x => x.SequenceID),
                    PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(z => z.PartSetUpID == partsetupid_),
                    PartSetUpID = viewModel.PartSetUpID,
                    CanUserEdit = canuseredit


                };



                return View("PickAdditionalProcessestoClone", cloneViewModel);

            }
            else {
                return View("CloneAdditionalProcesses",viewModel);
            
            }

        }

      public JsonResult SetAdditionalProcessSequenceOrder(FormCollection formValue)
      {
          int processid = Convert.ToInt16(formValue["process.ProcessingID"]);
          string sequenceid = (formValue["AdditionalProcess.SequenceID"]);
         AdditionalProcessingViewModel viewModel = new AdditionalProcessingViewModel
          {
              AdditionalProcess = _additionalprocessesRepository.AdditionalProcess.FirstOrDefault(a => a.ProcessingID == processid)

          };
          var partsetup = _partsetupRepository.PartSetUp.FirstOrDefault(c => c.PartSetUpID == viewModel.AdditionalProcess.PartSetUpID);

          GetUserInfo();
          viewModel.AdditionalProcess.SequenceID = sequenceid;
          viewModel.AdditionalProcess.LastEditedBy = username;
          viewModel.AdditionalProcess.LastEditDate = DateTime.Now;
          _additionalprocessesRepository.UpdateAdditionalProcessingSequenceOrder(viewModel.AdditionalProcess);


          return Json(new AdditionalProcessing { SequenceID = sequenceid, ProcessingID = Convert.ToInt16((processid).ToString()) });


      } 
        

        [HttpGet]
      public ActionResult CloneAdditionalProcesses(int partsetupid_)
      {                  
          bool canuseredit = CanUserEdit();
          AdditionalProcessingViewModel viewModel = new AdditionalProcessingViewModel
          {
              AdditionalProcesses = _additionalprocessesRepository.AdditionalProcess.Where(a => a.PartSetUpID == partsetupid_).OrderBy(x => x.SequenceID),
              PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(z => z.PartSetUpID == partsetupid_),
              CanUserEdit = canuseredit

          };
          viewModel.ItemID = viewModel.PartSetUp.PartID;
          viewModel.PartSetUps = _partsetupRepository.PartSetUp.Where(y => y.PartSetUpID != partsetupid_).Where(c=>c.PartCategory.CategoryID==viewModel.PartSetUp.CategoryID).Where(a => a.AdditionalProcessings.Count > 0)
              .OrderBy(a => a.PartID);
         
          ViewData["ProcessesToclone"] = new SelectList(viewModel.AdditionalProcessingSequence.Select(a => a.Text));
          return View("CloneAdditionalProcesses", viewModel);
      }
                       
        public ActionResult AdditionalProcessDetails(int partsetupid_)
        {
            bool canuseredit = CanUserEdit();
            AdditionalProcessingViewModel viewModel = new AdditionalProcessingViewModel 
            {
                  AdditionalProcesses=_additionalprocessesRepository.AdditionalProcess.Where(a=>a.PartSetUpID==partsetupid_).Where(b=>b.IsActive==true).OrderBy(x=>x.SequenceID),
                  PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(z => z.PartSetUpID == partsetupid_),
                  CanUserEdit=canuseredit
            };
          //  viewModel.PartSetUps = _partsetupRepository.PartSetUp.Where(y=>y.PartSetUpID!=partsetupid_).OrderBy(a => a.PartID);
          //  ViewData["ProcessesToclone"] = new SelectList(viewModel.AdditionalProcessingSequence.Select(a => a.Text));
            return View("AdditionalProcessDetails", viewModel);
        
        }

        [HttpPost]
        public ActionResult AdditionalProcessessCreate(AdditionalProcessingViewModel viewModel_)
        {
            GetUserInfo();
            if (viewModel_.AdditionalProcess.Description == null)
            {
                ModelState.AddModelError("AdditionalProcessesAdd", "Description of the Additional Process is required.");
            } 
            

            //if (viewModel_.AdditionalProcess.SequenceID.)
            //{
            //    ModelState.AddModelError("AdditionalProcessMissingSequenceID", "Please select a display order for this process.");
            //}
            
            if (ModelState.IsValid)
            {
                string itemid = viewModel_.PartSetUp.PartID;
                Int16 setupid = viewModel_.PartSetUp.PartSetUpID;
                int processingid = 0;
                viewModel_.AdditionalProcess.PartSetUpID = setupid;
                viewModel_.AdditionalProcess.LastEditDate = DateTime.Now;
                viewModel_.AdditionalProcess.LastEditedBy = username;
                viewModel_.AdditionalProcess.SequenceID = viewModel_.AdditionalProcess.SequenceID;
                viewModel_.AdditionalProcess.Description = viewModel_.AdditionalProcess.Description.ToUpper();
                viewModel_.AdditionalProcess.DescriptionES = viewModel_.AdditionalProcess.DescriptionES;
                viewModel_.AdditionalProcess.DescriptionCN = viewModel_.AdditionalProcess.DescriptionCN;
                viewModel_.AdditionalProcess.IsActive = viewModel_.IsActive;
                processingid=_additionalprocessesRepository.Insert(viewModel_.AdditionalProcess);
                string anchor = "process" + processingid.ToString();
               
             
                
                this.ShowSaveSuccessfull();
                //send user back to partmaintenance index
                TravelCardViewModel viewModelTC = new TravelCardViewModel
                {
                    ItemID = itemid,
                    ShowPartSetUpDetails=true,
                    returnanchor=anchor
                };
                return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);

            }
            else
            {
                ViewData["SequenceID"] = new SelectList(viewModel_.AdditionalProcessingSequence.Select(a => a.Text));
                return View(viewModel_);
            }
        }

      
        
        
        
        
        [HttpGet]
        public ActionResult AdditionalProcessessCreate(int PartSetUpId)
        {
          
            AdditionalProcessingViewModel viewModel = new AdditionalProcessingViewModel
            {
             
                PartSetUp=_partsetupRepository.PartSetUp.FirstOrDefault(a=>a.PartSetUpID==PartSetUpId),
                IsActive = true
             
            };

            ViewData["SequenceID"] = new SelectList(viewModel.AdditionalProcessingSequence.Select(a => a.Text));
        
            
            return View(viewModel);

        }




       
        public ActionResult AdditionalProcessRemove(int processid_)
        {


            GetUserInfo();
            AdditionalProcessingViewModel viewModel = new AdditionalProcessingViewModel
            {
                AdditionalProcess = _additionalprocessesRepository.AdditionalProcess.FirstOrDefault(a => a.ProcessingID == processid_),
                PartSetUpID = _additionalprocessesRepository.AdditionalProcess.FirstOrDefault(z => z.ProcessingID == processid_).PartSetUpID




            };

            var partsetup = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == viewModel.PartSetUpID);
            viewModel.PartSetUp=partsetup;
            viewModel.AdditionalProcess.IsActive = false;

            viewModel.AdditionalProcess.LastEditedBy = username;
            viewModel.AdditionalProcess.LastEditDate = DateTime.Now;
            _additionalprocessesRepository.Update(viewModel.AdditionalProcess);

            string partid = viewModel.PartSetUp.PartID;
            //send user back to partmaintenance index
            TravelCardViewModel viewModelTC = new TravelCardViewModel
            {
                ItemID = partid,
                ShowPartSetUpDetails=true
            };
            return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);

        }












        [HttpGet]
        public ActionResult AdditionalProcessesEdit(int processid_)
  
        {
        
         AdditionalProcessingViewModel viewModel = new AdditionalProcessingViewModel 
            {
                  AdditionalProcess=_additionalprocessesRepository.AdditionalProcess.FirstOrDefault(a=>a.ProcessingID==processid_),
             PartSetUpID=_additionalprocessesRepository.AdditionalProcess.FirstOrDefault(z=>z.ProcessingID==processid_).PartSetUpID
           
            
               

            };
    //     int partsetup = viewModel.PartSetUp.PartSetUpID == viewModel.AdditionalProcess.PartSetUpID;

         viewModel.PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a=>a.PartSetUpID==viewModel.PartSetUpID);

         return View(viewModel);
        }

     


       [HttpPost]
        public ActionResult AdditionalProcessesEdit(AdditionalProcessingViewModel viewModel)
        {
        

            if (viewModel.AdditionalProcess.Description == null)
            {
                ModelState.AddModelError("AdditionalProcessesEdit", "Description of the Additional Process is required.");
            }

        GetUserInfo();
            if (ModelState.IsValid)
            {
           
              //  string itemid = viewModel.PartSetUp.PartID;
              string itemid = viewModel.PartSetUp.PartID;
              //  viewModel.AdditionalProcess.PartSetUpID = viewModel.AdditionalProcess.PartSetUpID;
              //  viewModel.AdditionalProcess.ProcessingID = viewModel.AdditionalProcess.ProcessingID;
                viewModel.AdditionalProcess.Description = viewModel.AdditionalProcess.Description.ToUpper();
                viewModel.AdditionalProcess.DescriptionES = viewModel.AdditionalProcess.DescriptionES;
                viewModel.AdditionalProcess.DescriptionCN = viewModel.AdditionalProcess.DescriptionCN;
                viewModel.AdditionalProcess.Notes = viewModel.AdditionalProcess.Notes;
                viewModel.AdditionalProcess.IsActive = viewModel.AdditionalProcess.IsActive;
                viewModel.AdditionalProcess.SequenceID = viewModel.AdditionalProcess.SequenceID;
                viewModel.AdditionalProcess.LastEditedBy = username;
                viewModel.AdditionalProcess.LastEditDate = DateTime.Now;
                _additionalprocessesRepository.Update(viewModel.AdditionalProcess);
                this.ShowSaveSuccessfull();
                string anchor = "process" + viewModel.AdditionalProcess.ProcessingID.ToString();
                //send user back to partmaintenance index
                TravelCardViewModel viewModelTC = new TravelCardViewModel
                {
                    ItemID = itemid,
                    ShowPartSetUpDetails=true,
                    returnanchor=anchor
                };
                return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);
            }
            else
         
            {
                ViewData["SequenceID"] = new SelectList(viewModel.AdditionalProcessingSequence.Select(a => a.Text));
                return View(viewModel);
            }
        }

    }
}
