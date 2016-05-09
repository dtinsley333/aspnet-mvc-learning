using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelCard.DomainModel.Abstract;
using TravelCard.DomainModel.Entities;

namespace Quality.ViewModels
{
    public class PartSpecificationViewModel
    {
        //public parameters to pass into or retrieve from the view 
        public string ReturnUrl { get; set; }
        public bool IsActive { get; set; }
        public string OpCode { get; set; }
        public string ParentPartID { get; set; }
        public bool CanUserEdit { get; set; }
        public bool IsPartCharacteristic { get; set; }
        public int MeasurementMethodID { get; set; }
        public int MeasurementUnitID { get; set; }
        public int FrequencyID { get; set; }
        public string ItemID { get; set; }
        public TravelCard.DomainModel.Entities.PartSetUp PartSetupToCloneFrom { get; set; }
        public TravelCard.DomainModel.Entities.PartSetUp PartSetupToCloneTo { get; set; }

        public int PartSetUpID { get; set; }
        //get the entiities
        public TravelCard.DomainModel.Entities.PartSpecification PartSpecification { get; set; }
        public TravelCard.DomainModel.Entities.PartSetUp PartSetUp { get; set; }
        public TravelCard.DomainModel.Entities.MeasurementMethod MeasurementMethod { get; set; }
        public TravelCard.DomainModel.Entities.Frequency Frequency { get; set; }
        public IEnumerable<Ideal.DomainModel.Entities.Component> Component { get; set; }
        public TravelCard.DomainModel.Entities.MeasurementUnit MeasurementUnit { get; set; }
        public TravelCard.DomainModel.Entities.PartSpecification ComponentPartSpecification { get; set; }
        public TravelCard.DomainModel.Entities.PartSpecificationSequence PartSpecificationSequence{get;set;}

        //get repositories
        public IEnumerable<TravelCard.DomainModel.Entities.PartSpecification>PartSpecifications { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.PartSetUp> PartSetUps { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.MeasurementMethod> MeasurementMethods { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.Frequency> Frequencies { get; set; }
      
        public IEnumerable<TravelCard.DomainModel.Entities.MeasurementUnit> MeasurementUnits { get; set; }
        public IEnumerable<Ideal.DomainModel.Repositories.ComponentRepository> Components { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.PartSpecification> ComponentPartSpecifications { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.PartSpecificationSequence>PartSpecificationSequences{get;set;}



        public SelectList PartSetUpSelectList
        {
            get
            {

                if (PartSetUps != null)
                {
                    return new SelectList(PartSetUps
                        .OrderBy(n => n.PartID),
                        "PartId", "FullPartSetUpDesc");
                }
                else
                {
                    return null;
                }



            }

        }

   
        public SelectList MeasurementMethodSelectList
        {
            get
            {

                if (MeasurementMethods != null)
                {
                    return new SelectList(MeasurementMethods
                        .Where(a => a.IsActive)
                        .OrderBy(n => n.Description_EN),
                        "MeasurementMethodID", "FullMeasurementDesc");
                }
                else
                {
                    return null;
                }



            }


        }


        public SelectList FrequencySelectList
        {
            get
            {

                if (Frequencies != null)
                {
                    return new SelectList(Frequencies
                        .Where(a => a.IsActive)
                        .OrderBy(n => n.Description_EN),
                        "FrequencyID", "FullFrequencyDesc");
                }
                else
                {
                    return null;
                }



            }


        }

       
        public SelectList MeasurementUnitSelectList
        {
            get
            {

                if (MeasurementUnits != null)
                {
                    return new SelectList(MeasurementUnits
                        .Where(a => a.IsActive)
                        .OrderBy(n => n.Name),
                        "unitID", "Abbreviation");
                }
                else
                {
                    return null;
                }



            }


        }


        
        public SelectList PartSpecificationSequenceSelectList
        {
            get
            {

                if (PartSpecificationSequences != null)
                {
                    return new SelectList(PartSpecificationSequences
                       
                        .OrderBy(n => n.SequenceOrder),
                        "SequenceOrder", "FullSequenceOrderDesc");
                }
                else
                {
                    return null;
                }



            }


        }
    }




}
