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
    public class TravelCardPrintViewModel
    {
        

        public string ItemID { get; set; }
        public Int16 PartSetUpID { get; set; }
        public int NumContinuationTCs { get; set; }
        public CultureInfo UsersCulture { get; set; }
        public int? MaxBarCodeforSetUp { get; set; }
        public string TCBarCodeText { get; set; }
        public string ContinuationBarCodeText { get; set; }
        public bool IsDraft { get; set; }
        public int? OpCode { get; set; }
        public bool HasOpCodes { get; set; }
        public string Language { get; set; }
        public IEnumerable<Ideal.DomainModel.Entities.Part> Part { get; set; }
        public IEnumerable<Ideal.DomainModel.Entities.Component> Component { get; set; }
        public IEnumerable<Ideal.DomainModel.Entities.Component> ParentComponent { get; set; }
        public TravelCard.DomainModel.Entities.AdditionalProcessing AdditionalProcess { get; set; }
        public TravelCard.DomainModel.Entities.PartSpecification PartSpecification { get; set; }
        public TravelCard.DomainModel.Entities.PartSpecification ComponentPartSpecification { get; set; }
        public IList<Part> PartComponentDetails { get; set; }
        public TravelCard.DomainModel.Entities.PartSetUp PartSetUp { get; set; }
        public TravelCard.DomainModel.Entities.UserSetting UserSetting { get; set; }
        public TravelCard.DomainModel.Entities.Plant Plant { get; set; }
        public TravelCard.DomainModel.Entities.Frequency Frequency { get; set; }
        public TravelCard.DomainModel.Entities.MeasurementMethod MeasurementMethod { get; set; }
        public TravelCard.DomainModel.Entities.MeasurementUnit MeasurementUnit { get; set; }
        public TravelCard.DomainModel.Entities.TravelCard TravelCard { get; set; }
        public TravelCard.DomainModel.Entities.Language TCLanguage { get; set; }
        public TravelCard.DomainModel.Entities.TCBarCode TCBarCode { get; set; }
        public TravelCard.DomainModel.Entities.PartSpecificationSequence PartSpecificationSequence { get; set; }


     
        public IEnumerable<TravelCard.DomainModel.Entities.PartSpecificationSequence>PartSpecificationSequences{get;set;}
        public IEnumerable<TravelCard.DomainModel.Repositories.PartSetUpRepository> PartSetUps { get; set; }
        public IEnumerable<Ideal.DomainModel.Repositories.PartRepository> Parts { get; set; }
        public IEnumerable<Ideal.DomainModel.Repositories.ComponentRepository> Components { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.AdditionalProcessing> AdditionalProcesses { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.TravelCard> TravelCards { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.MeasurementMethod> MeasurementMethods { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.MeasurementUnit> MeasurementUnits { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.PartSpecification> PartSpecifications { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.PartSpecification> ComponentPartSpecifications { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.Frequency> Frequencies { get; set; }

        public IEnumerable<TravelCard.DomainModel.Entities.Language> Languages { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.UserSetting> UserSettings { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.TCBarCode> TCBarCodes { get; set; }

        public IEnumerable<TravelCard.DomainModel.Entities.Plant>Plants { get; set; }


        public SelectList OperationCodeSelectLIst
        {
            get
            {

               
                  
                
                
                return new SelectList(PartSpecifications
                        .Where(a => a.OperationCode!=null)
                             .OrderBy(n => n.OperationCode).Distinct(),
                        "OperationCode", "OperationCode");
              



            }

        }


        public SelectList TravelCardList
        {
            get
            {

                if (TravelCards != null)
                {
                    return new SelectList(TravelCards
                        .Where(a => a.TCBarCodeText!=null)
                      
                    
                      
                        .OrderBy(n => n.TCBarCodeText).Distinct(),
                       
                        "TCID", "TravelCardDesc");

                }
                else
                {
                    return null;
                }



            }

        }

        public SelectList NumOfContinuationTravelCards
        {
            get
            {

                return new SelectList(new[] { "0", "1", "2", "3", "4", "5", });



            }


        }


        public SelectList PrintOrientationSelectList
        {
            get
            {

                return new SelectList(new[] { "Portrait", "Landscape"});



            }


        }


    }
}