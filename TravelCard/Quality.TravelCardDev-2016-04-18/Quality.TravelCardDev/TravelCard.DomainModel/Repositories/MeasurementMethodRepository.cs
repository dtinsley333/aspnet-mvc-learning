using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;
using TravelCard.DomainModel.Abstract;
using System.Data.Objects;



namespace TravelCard.DomainModel.Repositories
{
    public class MeasurementMethodRepository : IMeasurementMethodRepository
    {
        private Quality_devEntities _qualityEntities;
        private IQueryable<MeasurementMethod> _measurementmethod;


        public IQueryable<MeasurementMethod> MeasurementMethod
        {
            get { return _measurementmethod; }
        }


        public  MeasurementMethodRepository(string connectionString)
        {

            _qualityEntities = new Quality_devEntities(connectionString);
            ObjectQuery<MeasurementMethod> measurementmethodQuery = _qualityEntities.MeasurementMethods;
            _measurementmethod = measurementmethodQuery;


        }


        public void Update(MeasurementMethod MeasurementMethod_)
        {
            var methodtoupdate = _qualityEntities.MeasurementMethods
                .FirstOrDefault(x => x.MeasurementMethodID == MeasurementMethod_.MeasurementMethodID);

            methodtoupdate.Description_EN = MeasurementMethod_.Description_EN;
            methodtoupdate.Description_MX = MeasurementMethod_.Description_MX;
            methodtoupdate.Description_CN = MeasurementMethod_.Description_CN;

     
           methodtoupdate.IsActive = MeasurementMethod_.IsActive;
           methodtoupdate.Notes = MeasurementMethod_.Notes;
           methodtoupdate.LastEditDate = MeasurementMethod_.LastEditDate;
           methodtoupdate.LastEditBy = MeasurementMethod_.LastEditBy;

            _qualityEntities.SaveChanges(SaveOptions.None);


        }




        public int Insert(MeasurementMethod measurementmethod_)
        {
            try
            {

                var methodtoinsert = new MeasurementMethod
                {
                   MeasurementMethodID = measurementmethod_.MeasurementMethodID,
                   Description_EN = measurementmethod_.Description_EN,
                   Description_MX = measurementmethod_.Description_MX,
                   Description_CN = measurementmethod_.Description_CN,
                   IsActive = true,
                   Notes = measurementmethod_.Notes,
                   CreateDate=measurementmethod_.CreateDate,
                   CreatedBy=measurementmethod_.CreatedBy,
                   
                   LastEditDate = measurementmethod_.LastEditDate,
                   LastEditBy = measurementmethod_.LastEditBy
                };

                _qualityEntities.AddToMeasurementMethods(methodtoinsert);
                _qualityEntities.SaveChanges(SaveOptions.None);
                return methodtoinsert.MeasurementMethodID;

            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();
                return 0;
            }



        }



    }





}
