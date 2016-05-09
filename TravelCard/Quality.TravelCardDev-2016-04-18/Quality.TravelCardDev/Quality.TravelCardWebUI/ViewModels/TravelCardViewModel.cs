using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ideal.DomainModel.Entities;
using Ideal.DomainModel.Abstract;
using TravelCard.DomainModel.Abstract;
using TravelCard.DomainModel.Entities;
using System.Web.Mvc;
using System.Globalization;

namespace Quality.ViewModels
{
    public class TravelCardViewModel
    {
 public TravelCardViewModel()
 {
 } 

        public string ItemID { get; set; }
        public Int16 PartSetUpID { get; set; }
        public bool CanUserEdit { get; set; }
        public string returnanchor { get; set; }
        public CultureInfo UsersCulture { get; set; }
        public bool IsDraft { get; set; }
        public int MaxBarCodeforSetUP { get; set; }
        public bool ShowPartSetUpDetails { get; set; }
        public IEnumerable<Ideal.DomainModel.Entities.Part> Part { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.Plant> Plants { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.UserSetting>UserSettings { get; set; }
        public IEnumerable<Ideal.DomainModel.Entities.Component> Component { get; set; }
        public IEnumerable<Ideal.DomainModel.Entities.Component> ParentComponent { get; set; }
        public TravelCard.DomainModel.Entities.AdditionalProcessing AdditionalProcess { get; set; }
        public TravelCard.DomainModel.Entities.PartSpecification PartSpecification { get; set; }
        public TravelCard.DomainModel.Entities.Frequency Frequency { get; set; }
        public TravelCard.DomainModel.Entities.TCBarCode TCBarCode { get; set; }
        public TravelCard.DomainModel.Entities.PartSpecification ComponentPartSpecification { get; set; }
      
        public TravelCard.DomainModel.Entities.MeasurementMethod MeasurementMethod { get; set; }
       
        public TravelCard.DomainModel.Entities.Plant Plant { get; set; }
        public TravelCard.DomainModel.Entities.UserSetting UserSetting { get; set; }

        public IList<Part> PartComponentDetails { get; set; }
        public TravelCard.DomainModel.Entities.PartSetUp PartSetUp { get; set; }

        public IEnumerable<TravelCard.DomainModel.Entities.MeasurementMethod> MeasurementMethods { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.TCBarCode> TCBarCodes { get; set; }
      
        public IEnumerable<TravelCard.DomainModel.Repositories.PartSetUpRepository> PartSetUps { get; set; }
        public IEnumerable<Ideal.DomainModel.Repositories.PartRepository> Parts { get; set; }
        public IEnumerable<Ideal.DomainModel.Repositories.ComponentRepository> Components { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.AdditionalProcessing> AdditionalProcesses { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.Frequency> Frequencies { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.PartSpecification> PartSpecifications { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.PartSpecification> ComponentPartSpecifications { get; set; }
    
       
    }
}