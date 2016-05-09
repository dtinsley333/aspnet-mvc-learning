using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;
using TravelCard.DomainModel.Abstract;
using System.Data.Objects;

namespace TravelCard.DomainModel.Repositories
{
    public class TravelCardRepository : ITravelCardRepository
    {
        private Quality_devEntities _qualityEntities;
        private IQueryable<TravelCard.DomainModel.Entities.TravelCard> _travelcard;
        public IQueryable<TravelCard.DomainModel.Entities.TravelCard> TravelCard
        {
            get { return _travelcard; }
        }

        public TravelCardRepository(string connectionString)
        {
            _qualityEntities = new Quality_devEntities(connectionString);
            ObjectQuery<TravelCard.DomainModel.Entities.TravelCard> travelcardQuery = _qualityEntities.TravelCards;
            _travelcard = travelcardQuery;

        }

        public void Update(TravelCard.DomainModel.Entities.TravelCard TravelCard_)
        {
            var travelcardtoupdate = _qualityEntities.TravelCards
                .FirstOrDefault(x => x.TCID == TravelCard_.TCID);
            travelcardtoupdate.PartSetUpID = TravelCard_.PartSetUpID;
            travelcardtoupdate.TCBarCodeText= TravelCard_.TCBarCodeText;
            travelcardtoupdate.IsContinuationCard = TravelCard_.IsContinuationCard;
            travelcardtoupdate.IsDraft = TravelCard_.IsDraft;
            travelcardtoupdate.OperationCode = TravelCard_.OperationCode;
            travelcardtoupdate.PrintDate = TravelCard_.PrintDate;
            travelcardtoupdate.PrintDate = TravelCard_.PrintDate;
            travelcardtoupdate.PrintLocation = TravelCard_.PrintLocation;
            travelcardtoupdate.LanguageID = TravelCard_.LanguageID;
            travelcardtoupdate.PrintedBy = TravelCard_.PrintedBy;
             travelcardtoupdate.Notes =TravelCard_.Notes;
            _qualityEntities.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);

        }

        public int Insert(TravelCard.DomainModel.Entities.TravelCard travelcard_)
        {
            try
            {
                var travelcardtoinsert = new TravelCard.DomainModel.Entities.TravelCard
                {
                    PartSetUpID=travelcard_.PartSetUpID,
                   
                    IsContinuationCard = travelcard_.IsContinuationCard,
                    IsDraft = travelcard_.IsDraft,
                    OperationCode=travelcard_.OperationCode,
                    LanguageID=travelcard_.LanguageID,
                    PrintDate=travelcard_.PrintDate,
                    PrintLocation=travelcard_.PrintLocation,
                    PrintedBy=travelcard_.PrintedBy,
                    Notes=travelcard_.Notes
                };
                _qualityEntities.AddToTravelCards(travelcardtoinsert);
                _qualityEntities.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
                return travelcardtoinsert.TCID;
            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();
                return 0;
            }
        }

    }

}
