
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
using Ideal.DomainModel.Repositories;
using Ideal.DomainModel.Abstract;
using Ideal.DomainModel.Entities;

using System.Web.Mvc.Ajax;

namespace Quality.Controllers
{
    public class PartSpecificationController : BaseController
    {
        IPartSpecificationRepository _partspecificationRepository;
        IPartSpecificationSequenceRepository _partspecificationsequenceRepository;
        IMeasurementMethodRepository _measurementmethodRepository;
        IFrequencyRepository _frequencyRepository;
        IPartSpecificationRepository _componentpartspecificationRepository;
        IMeasurementUnitRepository _measurementunitRepository;
        IPartSetUpRepository _partsetupRepository;
        private IComponentsRepository<Component> _componentsRepository;
        private IPartsRepository<Part> _componentpartsRepository;
        public PartSpecificationController(IPartSpecificationRepository partspecificationRepository_, IComponentsRepository<Component> componentsRepository_, IPartsRepository<Part> componentpartsRepository_, IPartSetUpRepository partsetupRepository_, IPartSpecificationRepository componentpartspecificationRepository_,IMeasurementMethodRepository measurementmethodRepository_,IMeasurementUnitRepository measurementunitRepository_,IFrequencyRepository frequencyRepository_,IPartSpecificationSequenceRepository partspecificationsequenceRepository_)
        {
            _partspecificationRepository = partspecificationRepository_;
            _partsetupRepository = partsetupRepository_;
            _componentpartspecificationRepository = componentpartspecificationRepository_;
            _componentsRepository = componentsRepository_;
            _componentpartsRepository = componentpartsRepository_;
            _measurementmethodRepository = measurementmethodRepository_;
            _measurementunitRepository = measurementunitRepository_;
            _frequencyRepository = frequencyRepository_;
            _partspecificationsequenceRepository = partspecificationsequenceRepository_;

        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClonePartSpecifications(PartSpecificationViewModel viewModel)
        {

            if (viewModel.PartSetUp.PartID==String.Empty||viewModel.PartSetUp.PartID==null)
            {
                ModelState.AddModelError("PartSpecificationsToClone", "Please select a part set up to clone from.");
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
                var partspecificationstoclone = _partspecificationRepository.PartSpecification.Where(a => a.PartSetUpID == partsetupIDtoclonefrom).Where(c => c.IsActive).ToList();


                PartSpecificationViewModel cloneViewModel = new PartSpecificationViewModel
                {

                    PartSetupToCloneFrom = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartID == parttoclonefrom),
                    PartSetupToCloneTo = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartID == parttocloneto),
                    PartSpecifications = _partspecificationRepository.PartSpecification.Where(b => b.IsActive == true).OrderBy(x => x.SequenceID),
                    PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(z => z.PartSetUpID == partsetupid_),
                    PartSetUpID = viewModel.PartSetUpID,
                    CanUserEdit = canuseredit


                };



                return View("PickPartSpecificationstoClone", cloneViewModel);
            }
            else {

                PartSpecificationViewModel _viewModel = new PartSpecificationViewModel
                {
                    PartSpecifications = _partspecificationRepository.PartSpecification.Where(a => a.PartSetUpID == viewModel.PartSetUpID).Where(b => b.IsActive == true).OrderBy(x => x.SequenceID),
                    PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(z => z.PartSetUpID == viewModel.PartSetUpID),
                    PartSetUpID = viewModel.PartSetUpID,
                    

                };
                viewModel.ItemID = viewModel.PartSetUp.PartID;

                viewModel.PartSetUps = _partsetupRepository.PartSetUp.Where(y => y.PartSetUpID != viewModel.PartSetUpID).Where(c => c.PartCategory.CategoryID == viewModel.PartSetUp.CategoryID).Where(a => a.PartSpecifications.Count > 0)
                .OrderBy(a => a.PartID);
                return View("ClonePartSpecifications", viewModel);
            
            }
        }


        [HttpGet]
        public ActionResult ClonePartSpecifications(int partsetupid_)
        {
            bool canuseredit = CanUserEdit();
            PartSpecificationViewModel viewModel = new PartSpecificationViewModel
            {
                PartSpecifications = _partspecificationRepository.PartSpecification.Where(a => a.PartSetUpID == partsetupid_).Where(b => b.IsActive == true).OrderBy(x => x.SequenceID),
                PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(z => z.PartSetUpID == partsetupid_),
                PartSetUpID = partsetupid_,
                CanUserEdit = canuseredit

            };
            viewModel.ItemID = viewModel.PartSetUp.PartID;

            viewModel.PartSetUps = _partsetupRepository.PartSetUp.Where(y => y.PartSetUpID != partsetupid_).Where(c => c.PartCategory.CategoryID == viewModel.PartSetUp.CategoryID).Where(a => a.PartSpecifications.Count > 0)
            .OrderBy(a => a.PartID);


          //  ViewData["ProcessesToclone"] = new SelectList(viewModel.PartSpecificationSequenceSelectList.Select(a => a.Text));
            return View("ClonePartSpecifications", viewModel);
        }

        public ActionResult PartSpecificationRemove(int specificationid_)
        {


            GetUserInfo();
            PartSpecificationViewModel viewModel = new PartSpecificationViewModel
            {
                PartSpecification = _partspecificationRepository.PartSpecification.FirstOrDefault(a => a.SpecificationID == specificationid_),
                PartSetUpID = _partspecificationRepository.PartSpecification.FirstOrDefault(z => z.SpecificationID == specificationid_).PartSetUpID
              

            };
            int partsetupid = viewModel.PartSpecification.PartSetUpID;
            var partsetup = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == viewModel.PartSetUpID);
            viewModel.PartSetUp = partsetup;
            viewModel.PartSpecification.IsActive = false;

            viewModel.PartSpecification.LastEditBy = username;
            viewModel.PartSpecification.LastEditDate = DateTime.Now;
            _partspecificationRepository.Update(viewModel.PartSpecification);

            string partid = viewModel.PartSetUp.PartID;
            //send user back to partmaintenance index
            TravelCardViewModel viewModelTC = new TravelCardViewModel
            {
                ItemID = partid,
                ShowPartSetUpDetails=true
            };
            return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);

        }


        [HttpPost]
        public ActionResult SaveClonedPartSpecifications(PartSpecificationViewModel viewModel, FormCollection result)
        {

            string itemid = viewModel.ItemID;

            Int16 partsetupid = viewModel.PartSetUp.PartSetUpID;


            try
            {

                var selecteditems = from x in result.AllKeys
                                    where result[x] != "false"

                                    select x;


                foreach (var item in selecteditems)
                {
                    if (item.Contains("SpecificationtoClone_"))
                    {
                        GetUserInfo();
                        string theitem = item.ToString();

                        theitem = theitem.Replace("SpecificationtoClone_", "");


                        int thespecificationtoclone = Convert.ToInt16(theitem);
                        var specificationtoclone = _partspecificationRepository.PartSpecification.FirstOrDefault(a => a.SpecificationID == thespecificationtoclone);
                        string descriptionEnglish = specificationtoclone.Characteristic;
                        string descriptionSpanish = specificationtoclone.CharacteristicES;
                        string descriptionCHinese = specificationtoclone.CharacteristicCN;
                        int? operationcode = Convert.ToInt32(specificationtoclone.OperationCode);
                        int? unitofmeasure = Convert.ToInt16(specificationtoclone.unitID);
                        int? frequencyid = Convert.ToInt16(specificationtoclone.FrequencyID);
                        string sequenceid = specificationtoclone.SequenceID;
                        string notes = "";
                        DateTime lasteditdate = DateTime.Now;
                        string lasteditedby = username;
                        PartSpecification partspecs_ = new PartSpecification

                        {
                            PartSetUpID = partsetupid,
                            Characteristic = descriptionEnglish,
                            CharacteristicES = descriptionSpanish,
                            SequenceID = sequenceid,
                            Notes = "",
                            unitID = Convert.ToInt16(unitofmeasure),
                            LowSpec = specificationtoclone.LowSpec,
                            LowSpecES = specificationtoclone.LowSpecES,
                            HighSpec = specificationtoclone.HighSpec,
                            HighSpecES = specificationtoclone.HighSpecES,
                            SampleSize = specificationtoclone.SampleSize,
                            SampleSizeES = specificationtoclone.SampleSizeES,
                            FrequencyID = specificationtoclone.FrequencyID,
                            Gauges = specificationtoclone.Gauges,
                            OperationCode = Convert.ToInt16(operationcode),
                            MeasurementMethodID = specificationtoclone.MeasurementMethodID,

                            IsComponentCharacteristic = specificationtoclone.IsComponentCharacteristic,
                            IsMachineSetUpCharacteristic = specificationtoclone.IsMachineSetUpCharacteristic,
                            IsActive = true,
                            IsPartCharacteristic = specificationtoclone.IsPartCharacteristic,
                            IsDieSetUpCharcteristic = specificationtoclone.IsDieSetUpCharcteristic,
                            LastEditBy = username,
                            LastEditDate = DateTime.Now

                        };

                        _partspecificationRepository.InsertClonedPartSpecification(partspecs_);
                        partspecs_ = null;
                        this.ShowSaveSuccessfull();

                    }

                }


                //send user back to partmaintenance index
                TravelCardViewModel viewModelTC = new TravelCardViewModel
                {
                    ItemID = itemid,
                    ShowPartSetUpDetails = true
                };
                return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);
            }
            catch (Exception ex)
            {
                //TODO: did not save. deal with error
                return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard");
            }
        }
    
        public JsonResult SetSpecificationSequenceOrder(FormCollection formValue)
        {
            int partspecificationid=Convert.ToInt16(formValue["specification.SpecificationID"]);
            string sequenceid = (formValue["PartSpecification.SequenceID"]);
            PartSpecificationViewModel   viewModel = new PartSpecificationViewModel{
            
            PartSpecification=_partspecificationRepository.PartSpecification.FirstOrDefault(a=>a.SpecificationID==partspecificationid),
         
            
            
            
            
            };

          
            GetUserInfo();
           //get the fist character


            if (sequenceid.Length < 2)
                sequenceid = "0" + sequenceid;

            string formattedid = sequenceid;
            
            
            viewModel.PartSpecification.SequenceID = sequenceid;



          
            viewModel.PartSpecification.LastEditBy = username;
            viewModel.PartSpecification.LastEditDate = DateTime.Now;
             _partspecificationRepository.UpdateSpecificationSequenceOrder(viewModel.PartSpecification);
            var partsetup = _partsetupRepository.PartSetUp.FirstOrDefault(c => c.PartSetUpID == viewModel.PartSpecification.PartSetUpID);
            int specificationsequenceorder = Convert.ToInt16(viewModel.PartSpecification.SequenceID);
            viewModel.PartSpecificationSequence = _partspecificationsequenceRepository.PartSpecificationSequence.FirstOrDefault(c => c.SequenceOrder == specificationsequenceorder);
            string seqnum = viewModel.PartSpecificationSequence.SequenceOrder.ToString();




            return Json(new PartSpecification { SequenceID = sequenceid, SpecificationID = Convert.ToInt16((partspecificationid).ToString()) });
                
            
            }
    




        [HttpGet]
        public ActionResult PickPartSpecificationstoClone(PartSpecificationViewModel ViewModel)
        {
            return View(ViewModel);
        }
        public ActionResult PartSpecificationDetails(int partsetupid_)
        {

            bool canuseredit = CanUserEdit();
            PartSpecificationViewModel viewModel = new PartSpecificationViewModel
            {
              
                PartSpecifications = _partspecificationRepository.PartSpecification.Where(a => a.PartSetUpID == partsetupid_)
                .Where(b => b.IsActive == true).OrderBy(c => c.SequenceID)
                .Where(k=>k.IsPartCharacteristic),
                PartSetUp=_partsetupRepository.PartSetUp.FirstOrDefault(z=>z.PartSetUpID==partsetupid_),
                MeasurementMethods=_measurementmethodRepository.MeasurementMethod,
                MeasurementUnits=_measurementunitRepository.MeasurementUnit,
              PartSpecificationSequences=_partspecificationsequenceRepository.PartSpecificationSequence,
            
                CanUserEdit=canuseredit

            };

       //TODO: users said they did not want to bring in sub components.
            //    string partid = viewModel.PartSetUp.PartID;
          //  viewModel.Component = _componentsRepository.GetByID(partid);
            
       
           // IEnumerable<Component> subcomponents = _componentsRepository.GetByID(partid);
          // List<int> subcomponentpartsetupids = new List<int>();
          // foreach (string partno in subcomponents.Select(a => a.Id))
          // {
            //   partno.ToString().Trim();
            //   var partsetupidofsubcomponents = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartID == partno);
            //   if (partsetupidofsubcomponents != null)
            //   {
             //      subcomponentpartsetupids.Add(partsetupidofsubcomponents.PartSetUpID);
            //   }

         //  }

           //foreach (var setupid in subcomponentpartsetupids)
           //{
           //    viewModel.ComponentPartSpecifications = _componentpartspecificationRepository.PartSpecification.Where(a => a.PartSetUpID == setupid)
           //    .Where(c => c.IsComponentCharacteristic == true).OrderBy(c => c.SequenceID);
           //}

            ViewData["SequenceSelect"] = new SelectList(viewModel.PartSpecificationSequenceSelectList.Select(a => a.Text));

            return View("PartSpecificationDetails", viewModel);

        }
   
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PartSpecificationCreate(PartSpecificationViewModel viewModel_)
        {

            if (ModelState["PartSpecification.SpecificationID"] != null)
                ModelState["PartSpecification.SpecificationID"].Errors.Clear();

            if (ModelState["PartSpecification.IsActive"] != null)
                ModelState["PartSpecification"].Errors.Clear();
           
            if (viewModel_.PartSpecification.Characteristic == null)
            {
                ModelState.AddModelError("PartSpecificationError", "Characteristic is required..");
            }

            if (viewModel_.PartSpecification.MeasurementMethodID == null)
            {
                ModelState.AddModelError("PartSpecification.MeasurementMethodID", "Please select a measurement method..");
            }

            if (viewModel_.PartSpecification.SequenceID == null)
            {
                ModelState.AddModelError("PartSpecification.SequenceIDError", "Display order is required..");
            }

          

            if (ModelState.IsValid)
            {
                GetUserInfo();
                string itemid = viewModel_.PartSetUp.PartID;
                Int16 setupid = viewModel_.PartSetUp.PartSetUpID;
                int specificationid = 0;
                viewModel_.PartSpecification.PartSetUpID = setupid;
                viewModel_.PartSpecification.LastEditDate = DateTime.Now;
                viewModel_.PartSpecification.SequenceID = viewModel_.PartSpecification.SequenceID;
                viewModel_.PartSpecification.MeasurementMethodID = viewModel_.PartSpecification.MeasurementMethodID;
                viewModel_.PartSpecification.Notes = viewModel_.PartSpecification.Notes;
                viewModel_.PartSpecification.Characteristic = viewModel_.PartSpecification.Characteristic.ToUpper();
                viewModel_.PartSpecification.CharacteristicES = viewModel_.PartSpecification.CharacteristicES;
                viewModel_.PartSpecification.unitID = viewModel_.PartSpecification.unitID;
                viewModel_.PartSpecification.FrequencyID = viewModel_.PartSpecification.FrequencyID;
                viewModel_.PartSpecification.CharacteristicCN = viewModel_.PartSpecification.CharacteristicCN;
                viewModel_.PartSpecification.LowSpec = viewModel_.PartSpecification.LowSpec;
                viewModel_.PartSpecification.LowSpecES = viewModel_.PartSpecification.LowSpecES;
                viewModel_.PartSpecification.HighSpec = viewModel_.PartSpecification.HighSpec;
                viewModel_.PartSpecification.HighSpecES = viewModel_.PartSpecification.HighSpecES;
              

                viewModel_.PartSpecification.OperationCode = viewModel_.PartSpecification.OperationCode;
              
                if (viewModel_.PartSpecification.Gauges == null || viewModel_.PartSpecification.Gauges == "")
                {
                    viewModel_.PartSpecification.Gauges = "None";
                }
                else
                {
                    viewModel_.PartSpecification.Gauges = viewModel_.PartSpecification.Gauges;
                }
                viewModel_.PartSpecification.GaugesES = viewModel_.PartSpecification.GaugesES;
                viewModel_.PartSpecification.SampleSize = viewModel_.PartSpecification.SampleSize;
                viewModel_.PartSpecification.SampleSizeES = viewModel_.PartSpecification.SampleSizeES;
               
                viewModel_.PartSpecification.IsActive = viewModel_.IsActive;
                viewModel_.PartSpecification.IsPartCharacteristic = viewModel_.IsPartCharacteristic;
                viewModel_.PartSpecification.IsComponentCharacteristic = viewModel_.PartSpecification.IsComponentCharacteristic;
                viewModel_.PartSpecification.IsMachineSetUpCharacteristic = viewModel_.PartSpecification.IsMachineSetUpCharacteristic;
                viewModel_.PartSpecification.IsDieSetUpCharcteristic = viewModel_.PartSpecification.IsDieSetUpCharcteristic;
                viewModel_.PartSpecification.LastEditBy = username;
                specificationid = _partspecificationRepository.Insert(viewModel_.PartSpecification);

                string anchor = "specification" + specificationid.ToString();

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
              
             
                viewModel_.MeasurementMethods = _measurementmethodRepository.MeasurementMethod;
                viewModel_.PartSpecificationSequences = _partspecificationsequenceRepository.PartSpecificationSequence;
                viewModel_.MeasurementUnits = _measurementunitRepository.MeasurementUnit;
                viewModel_.Frequencies = _frequencyRepository.Frequency;
                return View("PartSpecificationCreate", viewModel_);
            }
        }






        [HttpGet]
        public ActionResult PartSpecificationCreate(int PartSetUpId)
        {

           PartSpecificationViewModel viewModel = new PartSpecificationViewModel
            {

                PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == PartSetUpId),
                IsActive=true,
                IsPartCharacteristic=true,
               PartSpecificationSequences=_partspecificationsequenceRepository.PartSpecificationSequence,
      
          MeasurementUnits=_measurementunitRepository.MeasurementUnit,
          Frequencies=_frequencyRepository.Frequency,
           MeasurementMethods=_measurementmethodRepository.MeasurementMethod
            };

     ViewData["MeasurementMethodID"] = new SelectList(viewModel.MeasurementMethodSelectList.Select(a => a.Text));

     ViewData["unitID"] = new SelectList(viewModel.MeasurementUnitSelectList.Select(a => a.Text));
     ViewData["PartSpecification.SequenceID"] = new SelectList(viewModel.PartSpecificationSequenceSelectList.Select(a => a.Text));



     ViewData["FrequencyID"] = new SelectList(viewModel.FrequencySelectList.Select(a => a.Text));
            return View(viewModel);

        }



        [HttpGet]
        public ActionResult PartSpecificationEdit(int specificationid_,string parentID_)
        {

           PartSpecificationViewModel viewModel = new PartSpecificationViewModel
            {
                PartSpecification = _partspecificationRepository.PartSpecification.FirstOrDefault(a => a.SpecificationID == specificationid_),
                PartSetUpID = _partspecificationRepository.PartSpecification.FirstOrDefault(z => z.SpecificationID == specificationid_).PartSetUpID,
                ParentPartID=parentID_,
                MeasurementMethods=_measurementmethodRepository.MeasurementMethod,
                MeasurementUnits=_measurementunitRepository.MeasurementUnit,
                Frequencies = _frequencyRepository.Frequency,
                PartSpecificationSequences=_partspecificationsequenceRepository.PartSpecificationSequence

            };

           ViewData["unitID"] = new SelectList(viewModel.MeasurementUnitSelectList.Select(a => a.Text));
         
            viewModel.PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == viewModel.PartSetUpID);
            viewModel.MeasurementUnitID=Convert.ToInt16(viewModel.PartSpecification.unitID);
            return View("PartSpecificationEdit",viewModel);
        }




        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PartSpecificationEdit(PartSpecificationViewModel viewModel,FormCollection result)
        {

           // int partspecificationid = Convert.ToInt16(result["specification.SpecificationID"]);

            if (ModelState["PartSpecification.SequenceID"] != null)
                ModelState["PartSpecification.SequenceID"].Errors.Clear();
            
            if (viewModel.PartSpecification.Characteristic == null)
            {
                ModelState.AddModelError("PartSpecificationEdit", "Characteristic is required..");
            }
            //   if (viewModel.PartSpecification.MeasurementMethodID == null)
         

           
          
            if (ModelState.IsValid)
            {
              GetUserInfo();
                string parentid = viewModel.ParentPartID;

              //  viewModel.PartSpecification.SequenceID = "01";
                viewModel.PartSpecification.Notes = viewModel.PartSpecification.Notes;
                viewModel.PartSpecification.Characteristic = viewModel.PartSpecification.Characteristic.ToUpper();
                viewModel.PartSpecification.CharacteristicES = viewModel.PartSpecification.CharacteristicES;
                viewModel.PartSpecification.CharacteristicCN = viewModel.PartSpecification.CharacteristicCN;
                viewModel.PartSpecification.LowSpec = viewModel.PartSpecification.LowSpec;
                viewModel.PartSpecification.LowSpecES = viewModel.PartSpecification.LowSpecES;
                viewModel.PartSpecification.unitID = viewModel.PartSpecification.unitID;
                viewModel.PartSpecification.HighSpec = viewModel.PartSpecification.HighSpec;
                viewModel.PartSpecification.HighSpecES = viewModel.PartSpecification.HighSpecES;
                if (viewModel.PartSpecification.OperationCode != null)
                {
                    viewModel.PartSpecification.OperationCode = Convert.ToInt16(viewModel.PartSpecification.OperationCode);
                }
                viewModel.PartSpecification.MeasurementMethodID = viewModel.PartSpecification.MeasurementMethodID;
              //  viewModel.PartSpecification.MeasurementMethod = viewModel.PartSpecification.MeasurementMethod;
              //  viewModel.PartSpecification.MeasurementMethodES = viewModel.PartSpecification.MeasurementMethodES;
                if (viewModel.PartSpecification.Gauges == null || viewModel.PartSpecification.Gauges == String.Empty)
                {
                    viewModel.PartSpecification.Gauges = "None";
                
                }
                else
                {
                    viewModel.PartSpecification.Gauges = viewModel.PartSpecification.Gauges;
                }
                viewModel.PartSpecification.GaugesES = viewModel.PartSpecification.GaugesES;
                viewModel.PartSpecification.SampleSize = viewModel.PartSpecification.SampleSize;
                viewModel.PartSpecification.SampleSizeES = viewModel.PartSpecification.SampleSizeES;
                viewModel.PartSpecification.FrequencyID = viewModel.PartSpecification.FrequencyID;
               
                viewModel.PartSpecification.IsActive = viewModel.PartSpecification.IsActive;
                viewModel.PartSpecification.IsComponentCharacteristic = viewModel.PartSpecification.IsComponentCharacteristic;
                viewModel.PartSpecification.IsPartCharacteristic = viewModel.PartSpecification.IsPartCharacteristic;
                viewModel.PartSpecification.IsMachineSetUpCharacteristic = viewModel.PartSpecification.IsMachineSetUpCharacteristic;
                viewModel.PartSpecification.IsDieSetUpCharcteristic = viewModel.PartSpecification.IsDieSetUpCharcteristic;
                viewModel.PartSpecification.LastEditBy = username;
                viewModel.PartSpecification.LastEditDate = DateTime.Now;
                _partspecificationRepository.Update(viewModel.PartSpecification);
                this.ShowSaveSuccessfull();

                int specificationid = viewModel.PartSpecification.SpecificationID;
                string anchor = "specification" + specificationid.ToString();

                //send user back to partmaintenance index
                TravelCardViewModel viewModelTC = new TravelCardViewModel
                {
                    ItemID = parentid,
                    ShowPartSetUpDetails=true,
                    returnanchor=anchor
                };
              
                return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);
            }
            else
            {
           
                viewModel.MeasurementMethods = _measurementmethodRepository.MeasurementMethod;
                viewModel.MeasurementUnits = _measurementunitRepository.MeasurementUnit;
                viewModel.Frequencies = _frequencyRepository.Frequency;
                return View("PartSpecificationEdit",viewModel);
            }
        }

    }
}
