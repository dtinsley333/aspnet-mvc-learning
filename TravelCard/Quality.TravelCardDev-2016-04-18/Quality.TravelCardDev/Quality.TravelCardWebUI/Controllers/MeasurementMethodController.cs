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
    public class MeasurementMethodController : BaseController
    {
        IMeasurementMethodRepository _measurementmethodRepository;
        public MeasurementMethodController(IMeasurementMethodRepository measurementmethodRepository_)
        {
            _measurementmethodRepository = measurementmethodRepository_;

        }

        [HttpGet]
        public ActionResult MeasurementMethodEdit(int id_)
        {
            GetUserInfo();
            bool canuseredit = CanUserEdit();
            MeasurementMethodViewModel viewModel = new MeasurementMethodViewModel()
            {
                CanUserEdit = canuseredit,
                MeasurementMethod = _measurementmethodRepository.MeasurementMethod.FirstOrDefault(a => a.MeasurementMethodID == id_)
            };
            viewModel.IsActive = viewModel.MeasurementMethod.IsActive;
            return View(viewModel);


        }

        [HttpPost]
        public ActionResult MeasurementMethodEdit(MeasurementMethodViewModel viewModel_)
        {
            GetUserInfo();

            if (viewModel_.MeasurementMethod.Description_EN == null)
            {
                ModelState.AddModelError("MeasurementMethodCreateError", "An English description is required.");
            }


            if (ModelState.IsValid)
            {
               // viewModel_.PartCategory.CategoryID = viewModel_.PartCategory.CategoryID;
             
                viewModel_.MeasurementMethod.Description_EN = viewModel_.MeasurementMethod.Description_EN;
                viewModel_.MeasurementMethod.Description_MX = viewModel_.MeasurementMethod.Description_MX;
                viewModel_.MeasurementMethod.Description_CN = viewModel_.MeasurementMethod.Description_CN;
                viewModel_.MeasurementMethod.IsActive = viewModel_.MeasurementMethod.IsActive;
                viewModel_.MeasurementMethod.Notes = viewModel_.MeasurementMethod.Notes;
                viewModel_.MeasurementMethod.LastEditDate = DateTime.Now;
                viewModel_.MeasurementMethod.LastEditBy = username;
                _measurementmethodRepository.Update(viewModel_.MeasurementMethod);
                this.ShowSaveSuccessfull();
               string anchor = "measure"+viewModel_.MeasurementMethod.MeasurementMethodID.ToString();
                MeasurementMethodViewModel viewModel = new MeasurementMethodViewModel()
                {
                    MeasurementMethods = _measurementmethodRepository.MeasurementMethod.Where(a => a.IsActive == true),
                    CanUserEdit = UserCanEdit,
                    returnanchor = anchor

                };


                return View("MeasurementMethodMaintenance", viewModel);
            }
            else
            {
                return View(viewModel_);
            }




        }


        [HttpGet]
        public ActionResult MeasurementMethodCreate()
        {
            GetUserInfo();
            bool canuseredit = CanUserEdit();


            MeasurementMethodViewModel viewModel = new MeasurementMethodViewModel()
            {
                CanUserEdit = canuseredit
            };

            return View();
        }

        [HttpPost]
        public ActionResult MeasurementMethodCreate(MeasurementMethodViewModel viewModel_)
        {

            if (viewModel_.MeasurementMethod.Description_EN == null)
            {
                ModelState.AddModelError("CategoryCreateError", "An English description is required.");
            }



            GetUserInfo();

            if (ModelState.IsValid)
            {

                int measurementmethodid = 0;
                viewModel_.MeasurementMethod.Description_EN = viewModel_.MeasurementMethod.Description_EN;
                viewModel_.MeasurementMethod.Description_MX = viewModel_.MeasurementMethod.Description_MX;
                viewModel_.MeasurementMethod.Description_CN = viewModel_.MeasurementMethod.Description_CN;
                viewModel_.MeasurementMethod.IsActive = true;
                viewModel_.MeasurementMethod.Notes = viewModel_.MeasurementMethod.Notes;
                viewModel_.MeasurementMethod.CreateDate = DateTime.Now;
                viewModel_.MeasurementMethod.CreatedBy = username;
                viewModel_.MeasurementMethod.LastEditDate = DateTime.Now;
                viewModel_.MeasurementMethod.LastEditBy = username;


                measurementmethodid = _measurementmethodRepository.Insert(viewModel_.MeasurementMethod);
                string anchor="measure"+measurementmethodid.ToString();
                this.ShowSaveSuccessfull();
                MeasurementMethodViewModel viewModel = new MeasurementMethodViewModel()
                {
                    MeasurementMethods = _measurementmethodRepository.MeasurementMethod.Where(a => a.IsActive == true),
                    CanUserEdit = UserCanEdit,
                    returnanchor=anchor

                };


                return View("MeasurementMethodMaintenance", viewModel);
            }
            else
            {
                return View(viewModel_);
            }


        }
        public ActionResult MeasurementMethodMaintenance()
        {
            GetUserInfo();
            MeasurementMethodViewModel viewModel = new MeasurementMethodViewModel()
            {
                MeasurementMethods = _measurementmethodRepository.MeasurementMethod.Where(a => a.IsActive == true),
                CanUserEdit = UserCanEdit

            };


            return View("MeasurementMethodMaintenance", viewModel);
        }

    }
}
