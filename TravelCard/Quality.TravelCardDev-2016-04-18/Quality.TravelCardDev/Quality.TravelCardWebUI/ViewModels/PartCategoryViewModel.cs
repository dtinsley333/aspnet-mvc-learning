using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelCard.DomainModel.Abstract;
using TravelCard.DomainModel.Entities;

namespace Quality.ViewModels
{
    public class PartCategoryViewModel
    {
     
        public string ReturnUrl { get; set; }
        public bool IsActive { get; set; }
        public bool CanUserEdit { get; set; }
        public string ID { get; set; }
        public string returnanchor { get; set; }

     
        //get the entities
        public TravelCard.DomainModel.Entities.PartCategory PartCategory { get; set; }
       

        //get repositories
        public IEnumerable<TravelCard.DomainModel.Entities.PartCategory> PartCategories { get; set; }
        









    }
}