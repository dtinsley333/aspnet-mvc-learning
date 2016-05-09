using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelCard.DomainModel.Abstract;
using TravelCard.DomainModel.Entities;
using TravelCard.DomainModel.Repositories;


namespace TravelCardPrint.ViewModels
{
    public class TravelCardPrintViewModel
    {
        public IEnumerable<TravelCard.DomainModel.Entities.PartSetUp> PartSetUp { get; set; }
        public IEnumerable<TravelCard.DomainModel.Entities.TravelCard> TravelCard { get; set; }
    }
}