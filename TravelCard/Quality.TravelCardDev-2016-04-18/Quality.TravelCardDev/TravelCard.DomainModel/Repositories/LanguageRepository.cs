using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;
using TravelCard.DomainModel.Abstract;
using System.Data.Objects;



namespace TravelCard.DomainModel.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private Quality_devEntities _qualityEntities;
        private IQueryable<Language> _language;


        public IQueryable<Language> Language
        {
            get { return _language; }
        }


        public LanguageRepository(string connectionString)
        {

            _qualityEntities = new Quality_devEntities(connectionString);
            ObjectQuery<Language> languageQuery = _qualityEntities.Languages;
            _language = languageQuery;


        }








    }





}