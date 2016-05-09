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
///
namespace Quality.Controllers
{
    public class PartCategoryController : BaseController
    {
        IPartCategoryRepository _partcategoryRepository;
        public PartCategoryController(IPartCategoryRepository partcategoryRepository_)
        {
            _partcategoryRepository = partcategoryRepository_;
        
        }

        [HttpGet]
        public ActionResult PartCategoryEdit(int id_)
        {
            GetUserInfo();
            bool canuseredit = CanUserEdit();
            PartCategoryViewModel viewModel = new PartCategoryViewModel()
            {
                CanUserEdit = canuseredit,
                PartCategory = _partcategoryRepository.PartCategory.FirstOrDefault(a => a.CategoryID == id_)
            };
            viewModel.IsActive = viewModel.PartCategory.IsActive;
            return View(viewModel);
        
        
        }

        [HttpPost]
        public ActionResult PartCategoryEdit(PartCategoryViewModel viewModel_)
        
        
        
        {
            GetUserInfo();

            if (viewModel_.PartCategory.CategoryName == null)
            {
                ModelState.AddModelError("CategoryCreateError", "An English name is required.");
            }
            
            
            if (ModelState.IsValid)
            {
              //  viewModel_.PartCategory.CategoryID = viewModel_.PartCategory.CategoryID;
                   viewModel_.PartCategory.CategoryName = viewModel_.PartCategory.CategoryName;
                viewModel_.PartCategory.CategoryNameES = viewModel_.PartCategory.CategoryNameES;
                viewModel_.PartCategory.CategoryNameCN = viewModel_.PartCategory.CategoryNameCN;
                viewModel_.PartCategory.CategoryDescription = viewModel_.PartCategory.CategoryDescription;
                viewModel_.PartCategory.CategoryDescriptionES = viewModel_.PartCategory.CategoryDescriptionES;
                viewModel_.PartCategory.CategoryDescriptionCN = viewModel_.PartCategory.CategoryDescriptionCN;
                viewModel_.PartCategory.LastEditDate = DateTime.Now;
                viewModel_.PartCategory.LastEditedBy = username;
                 viewModel_.PartCategory.Notes = viewModel_.PartCategory.Notes;
                 viewModel_.PartCategory.IsActive = viewModel_.PartCategory.IsActive;
                _partcategoryRepository.Update(viewModel_.PartCategory);
                this.ShowSaveSuccessfull();
                string partcategoryid = viewModel_.PartCategory.CategoryID.ToString();
                string anchor = "category" + partcategoryid;
              
                PartCategoryViewModel viewModel = new PartCategoryViewModel()
                {
                    PartCategories = _partcategoryRepository.PartCategory.Where(a => a.IsActive == true),
                    CanUserEdit = UserCanEdit,
                    returnanchor = anchor

                };


                return View("PartCategoryMaintenance",viewModel);
            }
            else
            {
                return View(viewModel_);
            }

        
        
        
        }
        
        
        [HttpGet]
        public ActionResult PartCategoryCreate()
        
        {
            GetUserInfo();
            bool canuseredit = CanUserEdit();


            PartCategoryViewModel viewModel = new PartCategoryViewModel()
            {
                 CanUserEdit=canuseredit
            };

            return View();
        }

        [HttpPost]
        public ActionResult PartCategoryCreate(PartCategoryViewModel viewModel_)
        {

            if (viewModel_.PartCategory.CategoryName == null)
            {
                ModelState.AddModelError("CategoryCreateError", "An English name is required.");
            }
            
            
            
            GetUserInfo();

                 if (ModelState.IsValid)
                 {

                     int categoryid = 0;
                     viewModel_.PartCategory.CategoryName = viewModel_.PartCategory.CategoryName;
                     viewModel_.PartCategory.CategoryNameES = viewModel_.PartCategory.CategoryNameES;
                     viewModel_.PartCategory.CategoryNameCN = viewModel_.PartCategory.CategoryNameCN;
                     viewModel_.PartCategory.CategoryDescription = viewModel_.PartCategory.CategoryDescription;
                     viewModel_.PartCategory.CategoryDescriptionES = viewModel_.PartCategory.CategoryDescriptionES;
                     viewModel_.PartCategory.CategoryDescriptionCN = viewModel_.PartCategory.CategoryDescriptionCN;
                     viewModel_.PartCategory.LastEditDate = DateTime.Now;
                     viewModel_.PartCategory.LastEditedBy = username;
                     viewModel_.PartCategory.CreateDate = DateTime.Now;
                     viewModel_.PartCategory.CreatedBy = username;
                     viewModel_.PartCategory.Notes = viewModel_.PartCategory.Notes;
                     viewModel_.PartCategory.IsActive = true;
                     categoryid = _partcategoryRepository.Insert(viewModel_.PartCategory);
                     this.ShowSaveSuccessfull();
                     string anchor = "category" + categoryid.ToString();
                     PartCategoryViewModel viewModel = new PartCategoryViewModel()
                     {
                         PartCategories = _partcategoryRepository.PartCategory.Where(a => a.IsActive == true),
                         CanUserEdit = UserCanEdit,
                         returnanchor=anchor

                     };



                     return View("PartCategoryMaintenance",viewModel);
                 }
                 else {
                     return View(viewModel_);
                 }


        }
        public ActionResult PartCategoryMaintenance()
        {
            GetUserInfo();
            PartCategoryViewModel viewModel=new PartCategoryViewModel()
            {
                PartCategories=_partcategoryRepository.PartCategory.Where(a=>a.IsActive==true),
                CanUserEdit=UserCanEdit

            };
            
            
            return View("PartCategoryMaintenance",viewModel);
        }

    }
}
