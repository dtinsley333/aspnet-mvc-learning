using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelCard.DomainModel.Abstract;
using TravelCard.DomainModel.Entities;

namespace Quality.ViewModels
{
    public class AdditionalProcessingViewModel
    {
    //public parameters to pass into or retrieve from the view 
    public string ReturnUrl{get;set;}
    public bool IsActive { get; set; }
    public bool CanUserEdit { get; set; }
    public string ItemID { get; set; }
    public TravelCard.DomainModel.Entities.PartSetUp PartSetupToCloneFrom { get; set; }
    public TravelCard.DomainModel.Entities.PartSetUp PartSetupToCloneTo { get; set; }
    public Guid SelectedObjectsIds { get; set; }

     
    public int PartSetUpID { get; set; }
    //get the entiities
    public TravelCard.DomainModel.Entities.AdditionalProcessing AdditionalProcess { get; set; }
    public TravelCard.DomainModel.Entities.PartSetUp PartSetUp { get; set; }

   //get repositories
    public IEnumerable<TravelCard.DomainModel.Entities.AdditionalProcessing> AdditionalProcesses { get; set; }
    public IEnumerable<TravelCard.DomainModel.Entities.PartSetUp> PartSetUps { get; set; }


    public class ProcessToClone
    { 
        public Guid Id { get; set; }
        public string Name { get; set; }
    }     
        
        
        
        public SelectList PartSetUpSelectList
    {
        get
        {

            if (PartSetUps != null)
            {
                return new SelectList(PartSetUps
                            .OrderBy(n => n.PartID.Trim()),
                    "PartId", "FullPartSetUpDesc");
            }
            else
            {
                return null;
            }



        }

    }


 public SelectList AdditionalProcessingSequence
        {
            get {

                return new SelectList(new [] {"01", "02", "03", "04", "05", "06", "07","08","09","10","11","12","13","14","15","16","17","18","19","20","21","22","23","24","25"}); 
            
            
            
            }

          
 }
    }




}
