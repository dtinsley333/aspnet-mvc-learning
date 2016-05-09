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
    public class FrequencyController : BaseController
    {
        IFrequencyRepository _frequencyRepository;
        public FrequencyController(IFrequencyRepository frequencyRepository_)
        {
            _frequencyRepository = frequencyRepository_;

        }

        [HttpGet]
        public ActionResult FrequencyEdit(int id_)
        {
            GetUserInfo();
            bool canuseredit = CanUserEdit();
            FrequencyViewModel viewModel = new FrequencyViewModel()
            {
                CanUserEdit = canuseredit,
                Frequency = _frequencyRepository.Frequency.FirstOrDefault(a => a.FrequencyID == id_)
            };
            viewModel.IsActive = viewModel.Frequency.IsActive;
            return View(viewModel);


        }

        [HttpPost]
        public ActionResult FrequencyEdit(FrequencyViewModel viewModel_)
        {
            GetUserInfo();

            if (viewModel_.Frequency.Description_EN == null)
            {
                ModelState.AddModelError("FrequencyCreateError", "An English description is required.");
            }


            if (ModelState.IsValid)
            {
                // viewModel_.PartCategory.CategoryID = viewModel_.PartCategory.CategoryID;

                viewModel_.Frequency.Description_EN = viewModel_.Frequency.Description_EN;
                viewModel_.Frequency.Description_MX = viewModel_.Frequency.Description_MX;
                viewModel_.Frequency.Description_CN = viewModel_.Frequency.Description_CN;
                viewModel_.Frequency.IsActive = viewModel_.Frequency.IsActive;
                viewModel_.Frequency.Notes = viewModel_.Frequency.Notes;
                viewModel_.Frequency.LastEditDate = DateTime.Now;
                viewModel_.Frequency.LastEditBy = username;
                _frequencyRepository.Update(viewModel_.Frequency);
                this.ShowSaveSuccessfull();
               string anchor = "frequency" + viewModel_.Frequency.FrequencyID.ToString();

                FrequencyViewModel viewModel = new FrequencyViewModel()
                {
                    Frequencies = _frequencyRepository.Frequency.Where(a => a.IsActive == true),
                    CanUserEdit = UserCanEdit,
                    returnanchor = anchor

                };

                return View("FrequencyMaintenance",viewModel);
            }
            else
            {
                return View(viewModel_);
            }




        }


        [HttpGet]
        public ActionResult FrequencyCreate()
        {
            GetUserInfo();
            bool canuseredit = CanUserEdit();


           FrequencyViewModel viewModel = new FrequencyViewModel()
            {
                CanUserEdit = canuseredit
            };

            return View();
        }

        [HttpPost]
        public ActionResult FrequencyCreate(FrequencyViewModel viewModel_)
        {

            if (viewModel_.Frequency.Description_EN == null)
            {
                ModelState.AddModelError("FrequencyCreateError", "An English description is required.");
            }



            GetUserInfo();

            if (ModelState.IsValid)
            {

                int Frequencyid = 0;
                viewModel_.Frequency.Description_EN = viewModel_.Frequency.Description_EN;
                viewModel_.Frequency.Description_MX = viewModel_.Frequency.Description_MX;
                viewModel_.Frequency.Description_CN = viewModel_.Frequency.Description_CN;
                viewModel_.Frequency.IsActive = true;
                viewModel_.Frequency.Notes = viewModel_.Frequency.Notes;
                viewModel_.Frequency.CreateDate = DateTime.Now;
                viewModel_.Frequency.CreatedBy = username;
                viewModel_.Frequency.LastEditDate = DateTime.Now;
                viewModel_.Frequency.LastEditBy = username;


                Frequencyid = _frequencyRepository.Insert(viewModel_.Frequency);
                this.ShowSaveSuccessfull();
                string anchor = "frequency" + Frequencyid.ToString();
              

                FrequencyViewModel viewModel = new FrequencyViewModel()
                {
                    Frequencies = _frequencyRepository.Frequency.Where(a => a.IsActive == true),
                    CanUserEdit = UserCanEdit,
                    returnanchor=anchor

                };



                return View("FrequencyMaintenance", viewModel);
            }
            else
            {
                return View(viewModel_);
            }


        }
        public ActionResult FrequencyMaintenance()
        {
            GetUserInfo();
            FrequencyViewModel viewModel = new FrequencyViewModel()
            {
                Frequencies = _frequencyRepository.Frequency.Where(a => a.IsActive == true),
                CanUserEdit = UserCanEdit

            };


            return View("FrequencyMaintenance", viewModel);
        }

    }
}
