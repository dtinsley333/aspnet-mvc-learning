using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;
using TravelCard.DomainModel.Abstract;
using System.Data.Objects;

namespace TravelCard.DomainModel.Repositories
{
    public class TCBarCodeRepository : ITCBarCodeRepository
    {
        private Quality_devEntities _qualityEntities;
        private IQueryable<TravelCard.DomainModel.Entities.TCBarCode> _tcbarcode;
        public IQueryable<TravelCard.DomainModel.Entities.TCBarCode> TCBarCode
        {
            get { return _tcbarcode; }
        }

        public TCBarCodeRepository(string connectionString)
        {
            _qualityEntities = new Quality_devEntities(connectionString);
            ObjectQuery<TravelCard.DomainModel.Entities.TCBarCode> tcbarcodeQuery = _qualityEntities.TCBarCodes;
            _tcbarcode = tcbarcodeQuery;

        }

        public void Update(TravelCard.DomainModel.Entities.TCBarCode TCBarCode_)
        {
            var tcbarcodetoupdate = _qualityEntities.TCBarCodes
                .FirstOrDefault(x => x.TCBarCodeID == TCBarCode_.TCBarCodeID);
            tcbarcodetoupdate.PartSetUpID = TCBarCode_.PartSetUpID;
            tcbarcodetoupdate.BarCodeText = TCBarCode_.BarCodeText;
            tcbarcodetoupdate.BarCodeFile = TCBarCode_.BarCodeFile;
         
            _qualityEntities.SaveChanges(SaveOptions.None);

        }

        public int Insert(TravelCard.DomainModel.Entities.TCBarCode tcbarcode_)
        {
            try
            {
                var tcbarcodetoinsert = new TravelCard.DomainModel.Entities.TCBarCode
                {
                    PartSetUpID = tcbarcode_.PartSetUpID,
                    BarCodeText=tcbarcode_.BarCodeText,
                    BarCodeFile=tcbarcode_.BarCodeFile
                 
                };
                _qualityEntities.AddToTCBarCodes(tcbarcodetoinsert);
                _qualityEntities.SaveChanges(SaveOptions.None);
                return tcbarcodetoinsert.TCBarCodeID;
            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();
                return 0;
            }
        }

    }

}
