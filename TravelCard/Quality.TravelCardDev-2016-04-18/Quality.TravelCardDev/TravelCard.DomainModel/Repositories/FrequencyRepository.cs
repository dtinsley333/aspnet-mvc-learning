using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;
using TravelCard.DomainModel.Abstract;
using System.Data.Objects;



namespace TravelCard.DomainModel.Repositories
{
    public class FrequencyRepository : IFrequencyRepository
    {
        private Quality_devEntities _qualityEntities;
        private IQueryable<Frequency> _frequency;


        public IQueryable<Frequency> Frequency
        {
            get { return _frequency; }
        }


        public FrequencyRepository(string connectionString)
        {

            _qualityEntities = new Quality_devEntities(connectionString);
            ObjectQuery<Frequency> frequencyQuery = _qualityEntities.Frequencies;
            _frequency = frequencyQuery;


        }

        public void Update(Frequency Frequency_)
        {
            var frequencytoupdate = _qualityEntities.Frequencies
                .FirstOrDefault(x => x.FrequencyID == Frequency_.FrequencyID);

            frequencytoupdate.Description_EN = Frequency_.Description_EN;
            frequencytoupdate.Description_MX = Frequency_.Description_MX;
            frequencytoupdate.Description_CN = Frequency_.Description_CN;


            frequencytoupdate.IsActive = Frequency_.IsActive;
            frequencytoupdate.Notes = Frequency_.Notes;
            frequencytoupdate.LastEditDate = Frequency_.LastEditDate;
            frequencytoupdate.LastEditBy = Frequency_.LastEditBy;

            _qualityEntities.SaveChanges(SaveOptions.None);


        }



        public int Insert(Frequency frequency_)
        {
            try
            {

                var frequencytoinsert = new Frequency
                {
                    FrequencyID = frequency_.FrequencyID,
                    Description_EN = frequency_.Description_EN,
                    Description_MX = frequency_.Description_MX,
                    Description_CN = frequency_.Description_CN,
                    IsActive = true,
                    Notes = frequency_.Notes,
                    CreateDate = frequency_.CreateDate,
                    CreatedBy = frequency_.CreatedBy,
                    LastEditDate = frequency_.LastEditDate,
                    LastEditBy = frequency_.LastEditBy
                };

                _qualityEntities.AddToFrequencies(frequencytoinsert);
                _qualityEntities.SaveChanges(SaveOptions.None);
                return frequencytoinsert.FrequencyID;

            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();
                return 0;
            }



        }






    }





}