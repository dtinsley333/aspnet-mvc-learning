using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using System.Drawing;
using System.Collections;
using Ideal.DomainModel;
using Quality.WebUI.Controllers;
using Quality.ViewModels;
using Ideal.DomainModel.Abstract;
using Ideal.DomainModel.Entities;
using Ideal.DomainModel.Repositories;
using TravelCard.DomainModel.Repositories;
using TravelCard.DomainModel.Abstract;
using TravelCard.DomainModel.Entities;
using System.Globalization;
using System.Threading;

namespace Quality.WebUI.Controllers
{
    public class TravelCardController : BaseController
    {
         IPartsRepository<Part> _partsRepository;
        IPartSetUpRepository _partsetupRepository;
        IPartSpecificationRepository _partspecificationRepository;
        IPartSpecificationRepository _componentpartspecificationRepository;
        IAdditionalProcessingRepository _additionalprocessesRepository;
        IPartsRepository<Part> _componentpartsRepository;
         IComponentsRepository<Component> _componentsRepository;
        IMeasurementMethodRepository _measurementmethodRepository;
        IMeasurementUnitRepository _measurementunitRepository;
        IFrequencyRepository _frequencyRepository;
        IPlantRepository _plantRepository;
        IUserSettingRepository _usersettingRepository;
        ITravelCardRepository _travelcardRepository;
        ILanguageRepository _languageRepository;
        ITCBarCodeRepository _tcbarcodeRepository;
        IPartSpecificationSequenceRepository _partspecificationsequenceRepository;
        

         public TravelCardController()
         {
         } 
        
        
        public TravelCardController(IPartsRepository<Part> partsRepository_, IComponentsRepository<Component> componentsRepository_, IPartsRepository<Part> componentpartsRepository_, IPartSetUpRepository partsetupRespository_,IAdditionalProcessingRepository additionalprocessesRepository_, IPartSpecificationRepository partspecificationRepository_,IPartSpecificationRepository componentpartspecificationRepository_,IMeasurementMethodRepository measurementmethodRepository_, IPlantRepository plantRepository_, IUserSettingRepository usersettingRepository_,IMeasurementUnitRepository measurementUnitRepository_,IFrequencyRepository frequencyRepository_,ITravelCardRepository travelcardrepository_,ILanguageRepository languagerepository_, ITCBarCodeRepository tcbarcodeRepository_,IPartSpecificationSequenceRepository partspecificationsequenceRepository_)
        {

            _partsRepository = partsRepository_;
            _componentsRepository = componentsRepository_;
            _componentpartsRepository = componentpartsRepository_;
            _partsetupRepository = partsetupRespository_;
            _additionalprocessesRepository = additionalprocessesRepository_;
            _partspecificationRepository = partspecificationRepository_;
            _partsetupRepository = partsetupRespository_;
            _componentpartspecificationRepository = componentpartspecificationRepository_;
            _measurementmethodRepository = measurementmethodRepository_;
            _measurementunitRepository = measurementUnitRepository_;
            _frequencyRepository = frequencyRepository_;
            _plantRepository = plantRepository_;
            _usersettingRepository = usersettingRepository_;
            _travelcardRepository = travelcardrepository_;
            _languageRepository = languagerepository_;
            _tcbarcodeRepository = tcbarcodeRepository_;
            _partspecificationsequenceRepository = partspecificationsequenceRepository_;
       
        }


        [HttpGet]
        public ActionResult Index(bool _isdraft,string _language, string _partid)
        {
            ViewData["NoResultMessage"] = "";
            string part = _partid != null ? _partid : "";
            ViewData["PartID"] = part;
            var viewModel = new TravelCardPrintViewModel
            {
              ItemID="",
              IsDraft=_isdraft,

              PartSetUp=_partsetupRepository.PartSetUp.FirstOrDefault(a=>a.PartID.Trim()==part),
              NumContinuationTCs=0

            };

            viewModel.TravelCards = _travelcardRepository.TravelCard.Where(a => a.PartSetUpID == viewModel.PartSetUp.PartSetUpID);
            DateTime curdate=DateTime.Now;
            DateTime oldtcardlookup=curdate.AddDays(-60);
            viewModel.PartSpecifications = _partspecificationRepository.PartSpecification.Where(a => a.PartSetUpID == viewModel.PartSetUp.PartSetUpID);
            var filteredcards= viewModel.TravelCards
               .Where(c=>c.PrintDate>oldtcardlookup)
               .Where(c=>c.IsContinuationCard==false);
                filteredcards = filteredcards.Where(a => a.TCBarCodeText !="0");
                filteredcards = filteredcards.Where(a => a.TCBarCodeText != "");

            viewModel.TravelCards = filteredcards;
            var opcodelist = (from p in viewModel.PartSpecifications.Where(c=>c.OperationCode!=null).Select(a => a.OperationCode).OrderBy(c=>c.Value).Distinct()
                              select new SelectListItem

                              {
Text=p.Value.ToString(),Value=p.Value.ToString()

                              });

          
                ViewData["opcodelist"] = opcodelist;

                if (opcodelist!=null&&opcodelist.Count()>0)
                {
                    viewModel.HasOpCodes = true;
                
                }
                var printorientation = viewModel.PrintOrientationSelectList;
                ViewData["printorientation"] = printorientation;
        
            string selectedlanguage = _language;
           Thread.CurrentThread.CurrentCulture= CultureInfo.CreateSpecificCulture(selectedlanguage);
           Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedlanguage);
           viewModel.Language = selectedlanguage;
         
          
            return View("Index", viewModel);
        }


        [HttpGet]
        public ActionResult IndexReturn(bool _isdraft, string _language,string _partid)
        {

            string part = _partid != null ? _partid : "";
            ViewData["PartID"] = part;
            var viewModel = new TravelCardPrintViewModel
            {
                ItemID = "",
                IsDraft = _isdraft,


            };

            string selectedlanguage = _language;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(selectedlanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedlanguage);
            viewModel.Language = selectedlanguage;
            ViewData["NoResultMessage"] = Quality.Resources.Strings.PartNumPlease;

            return View("Index", viewModel);
        }

      public ActionResult SpanishCardSearch()
      {
          return View("SpanishCardSearch");
      }


        public ActionResult PartSearch()
        {
            ViewData["Message"] = "Item Search";
            return View();
        }

     
        [HttpGet]
        public ActionResult Imprimir_Tarjeta_de_Viaje_MX()
        {

        return View();
         }

        [HttpGet]
        public ActionResult PrintTravelCard_US()
        {

            return View();
        }

        [HttpGet]
        public ActionResult PartMaintenanceIndex()
        {

                return View();
  
        }


       

       
        public ActionResult BackToPartSetUp(string _itemid,Int16 _partsetupid)
        {
        //send user back to partmaintenance index
                  TravelCardViewModel viewModelTC = new TravelCardViewModel
                  {
                      ItemID = _itemid,
                      PartSetUpID = _partsetupid
                  };
                  return RedirectToAction("PartMaintenanceIndexShowSavedResult", "TravelCard", viewModelTC);
        
        }
        
        [HttpPost]
        public ActionResult PrintTravelCard_US(string ItemID)
                          
            
        {

           ItemID=ItemID.ToUpper();
            int count = 0;
            bool canedit = CanUserEdit();

            if (ItemID.Length > 0)
            {
                GetUserInfo();
                string name=username.ToLower().Trim();
                
                TravelCardViewModel viewModel = new TravelCardViewModel
                {
                     UserSetting=_usersettingRepository.UserSetting.FirstOrDefault(a=>a.UserName==name),
                    PartSetUp=_partsetupRepository.PartSetUp.FirstOrDefault(x=>x.PartID==ItemID),
                    ItemID=ItemID,
                    CanUserEdit=canedit,
                   
    
                };

                viewModel.Plant=_plantRepository.Plant.FirstOrDefault(a=>a.PlantCodeID==viewModel.UserSetting.PlantCodeID);
                string plant = viewModel.Plant.PlantCode;
               // viewModel.Component = _componentsRepository.GetByID(ItemID,plant);
                viewModel.Part = _partsRepository.GetByID(ItemID, plant);
               // viewModel.Component.Where(a => a.Id != ItemID);
            //    viewModel.ParentComponent = _componentsRepository.GetParentComponentByID(viewModel.ItemID, plant);
               count = viewModel.Part.Count();
               
                if (count > 0)
                {

                    if (viewModel.PartSetUp==null)
                    {
                        var message = "A Travel Card for this part could not be found.<br > Please contact the quality department.";
                        ViewData["NoResultMessage"] = message;

                    }
                    return View("PrintTravelCard_US", viewModel);
                }
                if(count==0)
                {
                    var message = "Your search returned no results. Please try again.";
                    ViewData["NoResultMessage"] = message;
                    ViewData["PartID"] = ItemID;
                 
                    return View("PrintTravelCard_US", viewModel);
                }

               
            }
         
           return View();
        }



        [HttpPost]
        public ActionResult Imprimir_Tarjeta_de_Viaje_MX(string ItemID)
        {

            ItemID = ItemID.ToUpper();
            int count = 0;
            bool canedit = CanUserEdit();

            if (ItemID.Length > 0)
            {
                GetUserInfo();
                string name = username.ToLower().Trim();

                TravelCardViewModel viewModel = new TravelCardViewModel
                {
                    UserSetting = _usersettingRepository.UserSetting.FirstOrDefault(a => a.UserName == name),
                    PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(x => x.PartID == ItemID),
                    ItemID = ItemID,
                    CanUserEdit = canedit,


                };

                viewModel.Plant = _plantRepository.Plant.FirstOrDefault(a => a.PlantCodeID == viewModel.UserSetting.PlantCodeID);
                string plant = viewModel.Plant.PlantCode;
                // viewModel.Component = _componentsRepository.GetByID(ItemID,plant);
                viewModel.Part = _partsRepository.GetByID(ItemID, plant);
                // viewModel.Component.Where(a => a.Id != ItemID);
                //    viewModel.ParentComponent = _componentsRepository.GetParentComponentByID(viewModel.ItemID, plant);
                count = viewModel.Part.Count();

                if (count > 0)
                {

                    if (viewModel.PartSetUp == null)
                    {
                        var message = "Una Tarjeta de viaje para este numero de parte no pudo ser encontrada. Favor de contactar al Departamento de Calidad o Ingenieria de Producto.";
                        ViewData["NoResultMessage"] = message;

                    }
                    return View("Imprimir_Tarjeta_de_Viaje_MX", viewModel);
                }
                if (count == 0)
                {
                    var message = "Su busqueda retorno sin resultaods.  Favor de tratar nuevamente.";
                    ViewData["NoResultMessage"] = message;
                    ViewData["PartID"] = ItemID;

                    return View("Imprimir_Tarjeta_de_Viaje_MX", viewModel);
                }


            }

            return View();
        }


        [HttpPost]
        public ActionResult PartMaintenanceIndex(string ItemID)
        {

            ItemID = ItemID.ToUpper();
            int count = 0;
            bool canedit = CanUserEdit();

            if (ItemID.Length > 0)
            {
                GetUserInfo();
                string name = username.ToLower().Trim();

                TravelCardViewModel viewModel = new TravelCardViewModel
                {
                    UserSetting = _usersettingRepository.UserSetting.FirstOrDefault(a => a.UserName == name),
                    PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(x => x.PartID == ItemID),
                    ItemID = ItemID,
                    CanUserEdit = canedit,


                };

                viewModel.Plant = _plantRepository.Plant.FirstOrDefault(a => a.PlantCodeID == viewModel.UserSetting.PlantCodeID);
                string plant = viewModel.Plant.PlantCode;
                viewModel.Component = _componentsRepository.GetByID(ItemID, plant);
                viewModel.Part = _partsRepository.GetByID(ItemID, plant);
                viewModel.Component.Where(a => a.Id != ItemID);
                viewModel.ParentComponent = _componentsRepository.GetParentComponentByID(viewModel.ItemID, plant);
                count = viewModel.Part.Count();

                if (count > 0)
                {


                    return View("PartMaintenanceIndex", viewModel);
                }
                if (count == 0)
                {
                    var message = "Your search returned no results. Please try again";
                    ViewData["NoResultMessage"] = message;
                    ViewData["PartID"] = ItemID;

                    return View("PartMaintenanceIndex", viewModel);
                }


            }

            return View();
        }
        
        [HttpGet]
        public ActionResult PartMaintenanceIndexShowSavedResult(TravelCardViewModel viewModelTC)
        {
            int count = 0;
            string ItemID = viewModelTC.ItemID;
            bool canedit = CanUserEdit();
       

            if (ItemID.Length > 0)
            {
                GetUserInfo();
                string name=username.ToLower().Trim();
             
                var viewModel = new TravelCardViewModel
                {
                    UserSetting = _usersettingRepository.UserSetting.FirstOrDefault(a => a.UserName == name),
                    PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(x => x.PartID == ItemID),
                    ShowPartSetUpDetails=true,
                    ItemID = ItemID,
                    CanUserEdit=canedit,
                    returnanchor=viewModelTC.returnanchor

                };

                viewModel.Plant = _plantRepository.Plant.FirstOrDefault(a => a.PlantCodeID == viewModel.UserSetting.PlantCodeID);
                string plant = viewModel.Plant.PlantCode;
                
                viewModel.Component = _componentsRepository.GetByID(ItemID, plant);
                    viewModel.Part = _partsRepository.GetByID(ItemID, plant);


                viewModel.ParentComponent = _componentsRepository.GetParentComponentByID(viewModel.ItemID, plant);
                count = viewModel.Part.Count();
                         string anchor = viewModel.returnanchor;
                if (count > 0)
                {
                    return View("PartMaintenanceIndex", viewModel);
                }
          
                if (count == 0)
                {
                    var message = "Your search returned no results. Please try again";
                    ViewData["NoResultMessage"] = message;
                    ViewData["PartID"] = ItemID;
                    return View("PartMaintenanceIndex", viewModel);
                }


            }

            return View();
        }
       
       
        public ActionResult PartMaintenanceIndex2(string ItemID)
        {
            GetUserInfo();
            string name = username.Trim().ToLower();
            int count = 0;
            bool canedit = CanUserEdit();
            ItemID = ItemID.ToUpper();
            if (ItemID.Length > 0)
            {
                var viewModel = new TravelCardViewModel
                {

                    UserSetting = _usersettingRepository.UserSetting.FirstOrDefault(a => a.UserName == name),
                 
                    PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(x => x.PartID == ItemID),
                    ItemID = ItemID,
                    CanUserEdit=canedit

                };

                  viewModel.Plant = _plantRepository.Plant.FirstOrDefault(a => a.PlantCodeID == viewModel.UserSetting.PlantCodeID);
                string plant = viewModel.Plant.PlantCode;

                   viewModel.Component = _componentsRepository.GetByID(ItemID, plant);
                   viewModel.Part = _partsRepository.GetByID(ItemID, plant);
                viewModel.ParentComponent = _componentsRepository.GetParentComponentByID(viewModel.ItemID, plant);
                count = viewModel.Part.Count();

                if (count > 0)
                {
                    return View("PartMaintenanceIndex", viewModel);
                }
                if (count == 0)
                {
                    var message = "Your search returned no results. Please try again";
                    ViewData["NoResultMessage"] = message;
                    ViewData["PartID"] = ItemID;

                    return View("PartMaintenanceIndex", viewModel);
                }


            }

            return View();
        }

        public ActionResult PartMaintenanceIndexDetails(string ItemID)
        {
            GetUserInfo();
            string name = username.Trim().ToLower();
            int count = 0;
            bool canedit = CanUserEdit();
            ItemID = ItemID.ToUpper();
            if (ItemID.Length > 0)
            {
                var viewModel = new TravelCardViewModel
                {

                    UserSetting = _usersettingRepository.UserSetting.FirstOrDefault(a => a.UserName == name),

                    PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(x => x.PartID == ItemID),
                    ItemID = ItemID,
                    CanUserEdit = canedit

                };

                viewModel.Plant = _plantRepository.Plant.FirstOrDefault(a => a.PlantCodeID == viewModel.UserSetting.PlantCodeID);
                string plant = viewModel.Plant.PlantCode;

                viewModel.Component = _componentsRepository.GetByID(ItemID, plant);
                viewModel.Part = _partsRepository.GetByID(ItemID, plant);
                viewModel.ParentComponent = _componentsRepository.GetParentComponentByID(viewModel.ItemID, plant);
                count = viewModel.Part.Count();
                viewModel.ShowPartSetUpDetails = true;

                if (count > 0)
                {
                    return View("PartMaintenanceIndex", viewModel);
                }
                if (count == 0)
                {
                    var message = "Your search returned no results. Please try again";
                    ViewData["NoResultMessage"] = message;
                    ViewData["PartID"] = ItemID;

                    return View("PartMaintenanceIndex", viewModel);
                }


            }

            return View();
        }




        public string GetComponentName(IPartsRepository<Part> partsRepository_,string partid)
        {
            GetUserInfo();
            string name = username.ToString().Trim();
            var viewModel = new TravelCardViewModel
            {

                UserSetting = _usersettingRepository.UserSetting.FirstOrDefault(a => a.UserName == name),


            };
            viewModel.Plant = _plantRepository.Plant.FirstOrDefault(a => a.PlantCodeID == viewModel.UserSetting.PlantCodeID);
            string plant = viewModel.Plant.PlantCode;
            var componentdescription =partsRepository_.GetByID(partid, plant);
            string componenttextdescription = componentdescription.Select(x => x.ITMDESC).ToString();
            return componenttextdescription;
        }


        [HttpPost]
        public ActionResult TravelCardPrint(string ItemID)
        {

            ItemID = ItemID.ToUpper();

            string message = "";
            string partid = "";

            if (String.IsNullOrEmpty(ItemID))
            {
                message = "Please enter an item id and try again.";
                partid = ItemID;
                //send the user back to the search screen
                ViewData["NoResultMessage"] = message;
                ViewData["PartID"] = partid;
                return RedirectToAction("Index", "TravelCard");
            }
            else
            {
            
                var viewModel = new TravelCardViewModel
                {
                  
                    PartSetUp=_partsetupRepository.PartSetUp.FirstOrDefault(x=>x.PartID==ItemID),
                    Frequencies = _frequencyRepository.Frequency.Where(a => a.IsActive),
                    UsersCulture=ResolveCulture(),
               
                  
                  
                   

                };
                viewModel.TCBarCodes = _tcbarcodeRepository.TCBarCode.Where(a => a.PartSetUpID == viewModel.PartSetUp.PartSetUpID);
           
               int maxid=viewModel.TCBarCodes.Max(a=>a.TCBarCodeID);
               viewModel.MaxBarCodeforSetUP = maxid;

                viewModel.Plant = _plantRepository.Plant.FirstOrDefault(a => a.PlantCodeID == viewModel.UserSetting.PlantCodeID);
                string plant = viewModel.Plant.PlantCode;
                viewModel.Component = _componentsRepository.GetByID(ItemID, plant);
                    viewModel.Part = _partsRepository.GetByID(ItemID, plant);
                viewModel.PartSetUpID = _partsetupRepository.PartSetUp.Where(x => x.PartID == ItemID).Select(a => a.PartSetUpID).FirstOrDefault();
                var partsetupid=viewModel.PartSetUpID;
                viewModel.PartSpecifications = _partspecificationRepository.PartSpecification.Where(a => a.PartSetUpID == partsetupid)
                       .OrderBy(c => c.SequenceID).Where(c=>c.IsPartCharacteristic);
                viewModel.AdditionalProcesses = _additionalprocessesRepository.AdditionalProcess.Where(a => a.PartSetUpID == partsetupid).Where(b => b.IsActive).OrderBy(c => c.SequenceID);
                viewModel.PartSpecifications = viewModel.PartSpecifications.Where(a => a.IsActive);
                viewModel.AdditionalProcesses = viewModel.AdditionalProcesses.Where(a => a.IsActive);

                return View("TravelCardPrint", viewModel);
            }
        }


     
    
        public ActionResult BackToIndex(TravelCardPrintViewModel _viewModel)
        {
       ///
        
         ViewData["NoResultMessage"] = "";
            ViewData["PartID"] = "";
            var viewModel = new TravelCardPrintViewModel
            {
              ItemID=_viewModel.PartSetUp.PartID,
              IsDraft=_viewModel.IsDraft,
          

            };

            string selectedlanguage = _viewModel.Language;
           Thread.CurrentThread.CurrentCulture= CultureInfo.CreateSpecificCulture(selectedlanguage);
           Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedlanguage);
           viewModel.Language = selectedlanguage;
         
          
            return View("Index", viewModel);
        
    
        
        }

      

        [HttpPost]
        public ActionResult TravelCard(TravelCardPrintViewModel _viewModel,FormCollection result)
        {

           

            
            string ItemID = _viewModel.ItemID;
                string message = "";
                string partid = "";

                string operationcode = "";
           
               
                foreach (var key in result.AllKeys)
                {
                    if (key.Contains("OperationCodes"))
                    {
                        operationcode = operationcode + result.Get(key);
                    }


                   
                }

            GetUserInfo();
            string name=username.ToLower();
               
               

                if (String.IsNullOrEmpty(ItemID))
                {

                    ModelState.AddModelError("PartSearchError", "Please enter a part number");

                    
                 
                }
                if (ModelState["TravelCard.TCID"] != null)
                    ModelState["TravelCard.TCID"].Errors.Clear();
                if (ModelState.IsValid)
                {
                    bool isdraft = _viewModel.IsDraft;
                    ItemID = _viewModel.ItemID.ToUpper();
                   // int partsetupid = _viewModel.PartSetUp.PartSetUpID;
                
                    partid = ItemID.Trim();
                    int numofcards = _viewModel.NumContinuationTCs;
                   int? opcode=operationcode!=""?Convert.ToInt16(operationcode):0;
                   _viewModel.PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartID == partid);
                   
                   int thispartsetupid = _viewModel.PartSetUp.PartSetUpID;
                  


               
                  
                    TravelCardPrintViewModel viewModel = new TravelCardPrintViewModel()
                {
               
                  
                    PartSetUp = _partsetupRepository.PartSetUp.FirstOrDefault(x => x.PartID == partid),
                    PartSetUpID = _partsetupRepository.PartSetUp.Where(x => x.PartID == partid).Select(a => a.PartSetUpID).FirstOrDefault(),
                    
                    NumContinuationTCs = numofcards,
                    IsDraft = _viewModel.IsDraft,
                    UsersCulture = ResolveCulture(),
               
                  
                    UserSetting = _usersettingRepository.UserSetting.FirstOrDefault(a => a.UserName == name),
                   

                };

                 
                    
                    
                    if (_viewModel.TravelCard!= null)
                    {
                        viewModel.TravelCard = _travelcardRepository.TravelCard.FirstOrDefault(a => a.TCID == _viewModel.TravelCard.TCID);
                    }



                   if(viewModel.TravelCard != null)
                    {
                      viewModel.ContinuationBarCodeText=viewModel.TravelCard.TCBarCodeText;
         
                   }

                    viewModel.Plant = _plantRepository.Plant.FirstOrDefault(a => a.PlantCodeID == viewModel.UserSetting.PlantCodeID);
                    string plant = viewModel.Plant.PlantCode;
                    viewModel.Component = _componentsRepository.GetByID(partid, plant);
                    viewModel.Part = _partsRepository.GetByID(partid, plant);

                    string selectedlanguage = _viewModel.Language;
                    if (selectedlanguage != null)
                    {
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(selectedlanguage);
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedlanguage);
                        viewModel.UsersCulture = Thread.CurrentThread.CurrentCulture;
                    }
               

                    if (viewModel.Part.Count() > 0 && viewModel.PartSetUp == null)
                    {
                        message = "Part #" + partid + " was found in the ASI but the part has not been set up in the Travel Card system. Please contact Product Engineering.";

                        ViewData["NoResultMessage"] = message;
                        ViewData["PartID"] = partid;
                        return View("Index", viewModel);
                    }

                    if (viewModel.Part.Count() == 0 && viewModel.PartSetUp == null)
                    {
                        message = "Part #" + partid + " was not found in ASI . Please please try again.";

                        ViewData["NoResultMessage"] = message;
                        ViewData["PartID"] = partid;
                        return View("Index", viewModel);
                    }




                    var partsetupid = viewModel.PartSetUpID;
                    IEnumerable<Component> subcomponents = _componentsRepository.GetByID(partid, plant);
                    List<int> subcomponentpartsetupids = new List<int>();
                    foreach (string partno in subcomponents.Select(a => a.Id))
                    {
                        partno.ToString().Trim();
                        var partsetupidofsubcomponents = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartID == partno);
                        if (partsetupidofsubcomponents != null)
                        {
                            subcomponentpartsetupids.Add(partsetupidofsubcomponents.PartSetUpID);
                        }

                    }



                    if (operationcode.Length>0)
                    {
                        
                        
                        int opcodenum = Convert.ToInt16(operationcode);
                        viewModel.OpCode = opcodenum;
                        viewModel.PartSpecifications = _partspecificationRepository.PartSpecification.Where(a => a.PartSetUpID == partsetupid)
                               .Where(b => b.IsPartCharacteristic == true)
                               .Where(c => c.IsPartCharacteristic == true).OrderBy(c => c.SequenceID);
                        viewModel.PartSpecifications = viewModel.PartSpecifications.Where(a => a.OperationCode == opcodenum);
                     

                    }
                    else {

                        viewModel.PartSpecifications = _partspecificationRepository.PartSpecification.Where(a => a.PartSetUpID == partsetupid)
                           .Where(b => b.IsPartCharacteristic == true)
                           .Where(c => c.IsPartCharacteristic == true).OrderBy(c => c.SequenceID);
                    
                    
                    }

                    foreach (var setupid in subcomponentpartsetupids)
                    {
                        viewModel.ComponentPartSpecifications = _componentpartspecificationRepository.PartSpecification.Where(a => a.PartSetUpID == setupid)
                        .Where(c => c.IsComponentCharacteristic == true).OrderBy(c => c.SequenceID);
                    }




                    viewModel.AdditionalProcesses = _additionalprocessesRepository.AdditionalProcess.Where(a => a.PartSetUpID == partsetupid).Where(b => b.IsActive == true).OrderBy(c => c.SequenceID);
                    viewModel.MeasurementMethods = _measurementmethodRepository.MeasurementMethod;
                    viewModel.MeasurementUnits = _measurementunitRepository.MeasurementUnit;
                    viewModel.PartSpecificationSequences = _partspecificationsequenceRepository.PartSpecificationSequence;
                    viewModel.Frequencies = _frequencyRepository.Frequency.Where(a => a.IsActive);
                    
                    



                    bool isreleaseready = viewModel.PartSetUp.IsReleaseReady;
                    // bool isactive = viewModel.PartSetUp.PartIsActive;


                    message = "";






                    if ((isreleaseready == false && viewModel.PartSetUp != null) && (isdraft == false))
                    {
                        message = "Part #" + partid + " was found in ASI but the part set up has not been set as release ready in the Travel Card System. Please contact Product Engineering.";

                        ViewData["NoResultMessage"] = message;
                        ViewData["PartID"] = partid;
                        return View("Index", viewModel);


                    }



              
                    if (viewModel.Part.Count() == 0)
                    {
                        message = "Part not found in the system. Please try again.";
                        ViewData["NoResultMessage"] = message;
                        ViewData["PartID"] = partid;
                        return View("Index", viewModel);

                    }


                    return View("TravelCardPrint", viewModel);
                }
                else {

                    ViewData["NoResultMessage"] = "";
                    ViewData["PartID"] = "";
                    var viewModel = new TravelCardPrintViewModel
                    {
                        ItemID = "",
                        IsDraft = _viewModel.IsDraft,
                        UsersCulture = ResolveCulture()
                        


                    };

                    bool isdraft = _viewModel.IsDraft;
                    string language = _viewModel.Language;
                    viewModel.PartSpecifications = viewModel.PartSpecifications.Where(a => a.IsActive);

                    return View("TravelCardPrint", viewModel);
                }
                
                }
            }
            }
     

    

