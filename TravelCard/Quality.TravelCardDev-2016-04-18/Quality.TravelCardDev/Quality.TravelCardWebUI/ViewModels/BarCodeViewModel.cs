using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TravelCard.DomainModel.Abstract;
using TravelCard.DomainModel.Entities;

namespace Quality.ViewModels
{
    public class BarCodeViewModel
    {
        public string BarcodeText { get; set; }
        public string BarcodeThickness { get; set; }
        public bool ShowBarcodeText { get; set; }
        public string BarcodeImageUrl { get; set; }
        public string BarcodeFilePath { get; set; }


        public TravelCard.DomainModel.Entities.TCBarCode TCBarCode { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.TCBarCode> TCBarCodes { get; set; }

        private List<SelectListItem> _ThicknessOptions = null;
        public List<SelectListItem> ThicknessOptions
        {
            get
            {
                if (_ThicknessOptions == null)
                {
                    _ThicknessOptions = new List<SelectListItem>();

                    _ThicknessOptions.Add(new SelectListItem() { Text = "Thin", Value = "1" });
                    _ThicknessOptions.Add(new SelectListItem() { Text = "Medium", Value = "2" });
                    _ThicknessOptions.Add(new SelectListItem() { Text = "Thick", Value = "3" });
                }

                return _ThicknessOptions;
            }
        }
    }
}