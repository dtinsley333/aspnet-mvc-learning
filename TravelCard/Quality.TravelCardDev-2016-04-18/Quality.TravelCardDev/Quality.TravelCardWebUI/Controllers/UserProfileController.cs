using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quality.WebUI.Controllers;
using Quality.ViewModels;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Security.Policy;
using System.Security.Principal;
using System.Globalization;
using System.Configuration;
using TravelCard.DomainModel.Repositories;
using TravelCard.DomainModel.Abstract;
using TravelCard.DomainModel.Entities;



namespace Quality.WebUI.Controllers
{

    
    public class UserProfileController : BaseController
    {

        IPlantRepository _plantRepository;
        IUserSettingRepository _usersettingRepository;
        ILanguageRepository _languageRepository;
        public UserProfileController(IPlantRepository plantRepository_, IUserSettingRepository usersettingRepository_,ILanguageRepository languageRepository_)
        {
            _plantRepository = plantRepository_;
            _usersettingRepository = usersettingRepository_;
            _languageRepository = languageRepository_;
        }
       
        
        
        public ActionResult Menubar()
        {

            GetUserInfo();
            bool canuserapprove = CanUserApprove();
            UserProfileViewModel viewModel = new UserProfileViewModel
            {
                username = username,
                usergroupmembership = usergroupmembership,
                CanUserApprove=canuserapprove

            };
            
            return View("Menubar",viewModel);
        }



        public ActionResult NoAccess()
        {
            GetUserInfo();
            UserProfileViewModel viewModel = new UserProfileViewModel
            {
                username = username,
                usergroupmembership = usergroupmembership

            };
            if (viewModel.usergroupmembership == null || viewModel.usergroupmembership.Count == 0)
            {
                
                Redirect("http://tn-sqldevel:9777/noaccess.aspx");
                return null;
            }
            else
            
            {
                return null;
            }
        }


       public ActionResult UserListing()
        {
            ArrayList travelcarduserlist = GetADGroupUsers("TravelCardUser");
            ArrayList travelcardmaintenancelist = GetADGroupUsers("TravelCardMaintenance");
            ArrayList travelcardadminlist = GetADGroupUsers("TravelCardAdmin");
            ArrayList travelcardapproverlist=GetADGroupUsers("TravelCardApprover");

            UserProfileViewModel viewModel = new UserProfileViewModel {
            TravelCardAdminList=travelcardadminlist,
            TravelCardMaintenanceList=travelcardmaintenancelist,
            TravelCardApproverList=travelcardapproverlist,
            TravelCardUserList=travelcarduserlist
            
            };

            return View(viewModel);
       }



        ArrayList GetADGroupUsers(string groupName)
        {
            SearchResult result;
            DirectorySearcher search = new DirectorySearcher();
            search.Filter = String.Format("(cn={0})", groupName);
            search.PropertiesToLoad.Add("member");
            result = search.FindOne();

            ArrayList userNames = new ArrayList();
            if (result != null)
            {
                for (int counter = 0; counter <
                         result.Properties["member"].Count; counter++)
                {
                    string user = (string)result.Properties["member"][counter];
                    
                    userNames.Add(user);
                }
            }
            return userNames;
        }


        [HttpPost]
        public ActionResult SetUserPlant(UserProfileViewModel viewModel,FormCollection result)
        {
            GetUserInfo();

            string name=username.ToLower();

            viewModel.UserSetting = _usersettingRepository.UserSetting.Where(a => a.UserName == name).FirstOrDefault();

            int plantid = viewModel.Plant.PlantCodeID;
        
            string sequenceid = (result["PartSpecification.SequenceID"]);
            //This is junk, need to fix
            Int16 languageid=0;
           if(plantid==1)
           {
             languageid=1;
           }
          if(plantid==2)
           {
             languageid=2;
           }

           if(plantid==4)
           {
             languageid=3;
           }

         

            if (viewModel.UserSetting == null)
            {
                //insert a new user setting

                UserSetting usersetting_ = new UserSetting

                {
                    LanguageID = languageid,
                    UserName = username.ToLower(),
                    PlantCodeID = Convert.ToInt16(plantid)
                };


                _usersettingRepository.Insert(usersetting_);
                usersetting_ = null;

            }
            else
            {
                UserSetting usersetting_ = new UserSetting

                {
                   
                    UserID=viewModel.UserSetting.UserID,
                    LanguageID = languageid,
                    UserName = viewModel.UserSetting.UserName,
                    PlantCodeID = Convert.ToInt16(plantid)
                };
               
                _usersettingRepository.Update(usersetting_);
                usersetting_ = null;  



            }



            return RedirectToAction("PartMaintenanceIndex", "TravelCard", viewModel);

        }



        public void SetDefaultPlant(UserProfileViewModel viewModel)
        {
            GetUserInfo();
            string culture = RegionInfo.CurrentRegion.DisplayName.ToLower();

            string name = username.ToLower();

            viewModel.UserSetting = _usersettingRepository.UserSetting.Where(a => a.UserName == name).FirstOrDefault();

            string language = viewModel.UsersCulture.Name;

            viewModel.Language = _languageRepository.Language.FirstOrDefault(a => a.LanguageCode == language);
            Int16 plantcodeid=0;
            Int16 languageid=0;
            if (culture=="united states")
            {
                plantcodeid = 1;
                languageid = 1;
            }
            else if(culture=="mexico")
            {

                plantcodeid = 2;
                languageid = 2;
            }

            else if (culture=="china")
            {

                plantcodeid = 4;
                languageid = 3;
            }
            else {
                plantcodeid = 1;
                plantcodeid = 1;
            }
           



            if (viewModel.UserSetting == null)
            {
                //insert a new user setting

                UserSetting usersetting_ = new UserSetting

                {
                    LanguageID = languageid,
                    UserName = username.ToLower(),
                    PlantCodeID = plantcodeid
                };


                _usersettingRepository.Insert(usersetting_);
                usersetting_ = null;

            }
           




        }




        public ActionResult SelectPlant()
        {

            GetUserInfo();
            string name = username.ToString().ToLower();

            UserProfileViewModel viewModel = new UserProfileViewModel
            {

                username = username.ToLower(),
                usergroupmembership = usergroupmembership,
                Plants = _plantRepository.Plant,
            };
            return View(viewModel);


        }



        public ActionResult UserHeadline()
        {
           
            GetUserInfo();
            string name = username.ToString().ToLower();
          
            UserProfileViewModel viewModel = new UserProfileViewModel
            {

             username=username.ToLower(),
             usergroupmembership=usergroupmembership,
             UsersCulture=ResolveCulture(),
             UserSetting=_usersettingRepository.UserSetting.FirstOrDefault(a=>a.UserName==name)

            };
            if (viewModel.UserSetting != null)
            {
                viewModel.Plant = _plantRepository.Plant.FirstOrDefault(a => a.PlantCodeID == viewModel.UserSetting.PlantCodeID);
            }
            else
            {
                SetDefaultPlant(viewModel);
                viewModel.UserSetting = _usersettingRepository.UserSetting.FirstOrDefault(a => a.UserName == name);
                viewModel.Plant = _plantRepository.Plant.FirstOrDefault(a => a.PlantCodeID == viewModel.UserSetting.PlantCodeID);
            }
                return View(viewModel);
          

        }

       
    }
}
