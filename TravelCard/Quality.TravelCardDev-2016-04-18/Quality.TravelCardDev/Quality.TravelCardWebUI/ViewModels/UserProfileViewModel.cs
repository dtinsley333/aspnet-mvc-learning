using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using TravelCard.DomainModel.Abstract;
using TravelCard.DomainModel.Entities;

namespace Quality.ViewModels
{
    public class UserProfileViewModel
    {

        public IEnumerable<TravelCard.DomainModel.Entities.Plant> Plants { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.Language> Languages { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.UserSetting> UserSettings { get; set; }
        public TravelCard.DomainModel.Entities.Plant Plant { get; set; }
        public TravelCard.DomainModel.Entities.Language Language { get; set; }
        public TravelCard.DomainModel.Entities.UserSetting UserSetting { get; set; }
        public string username { get; set; }
        public string UsersComputerName { get; set; }
        public List<string> groups { get; set; }
        public List<string> usergroupmembership { get; set; }
        public bool UserCanEdit { get; set; }
        public bool CanUserApprove { get; set; }
        public CultureInfo UsersCulture { get; set; }
        public string currentloggedinuser { get; set; }
        public ArrayList TravelCardUserList { get; set; }
        public ArrayList TravelCardMaintenanceList { get; set; }
        public ArrayList TravelCardApproverList { get; set; }
        public ArrayList TravelCardAdminList { get; set; }

        public SelectList PlantSelectList
        {
            get
            {

                if (Plants != null)
                {
                    return new SelectList(Plants
                        .Where(a => a.IsActive)
                        .OrderBy(n => n.PlantName),
                        "PlantCodeID", "PlantName");
                 
                }
                else
                {
                    return null;
                }



            }

        }

       
    }
}