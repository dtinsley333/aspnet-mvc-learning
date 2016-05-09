using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;
using TravelCard.DomainModel.Abstract;
using System.Data.Objects;



namespace TravelCard.DomainModel.Repositories
{
    public class UserSettingRepository : IUserSettingRepository
    {
        private Quality_devEntities _qualityEntities;
        private IQueryable<UserSetting> _usersetting;


        public IQueryable<UserSetting> UserSetting
        {
            get { return _usersetting; }
        }


        public UserSettingRepository(string connectionString)
        {

            _qualityEntities = new Quality_devEntities(connectionString);
            ObjectQuery<UserSetting> usersettingQuery = _qualityEntities.UserSettings;
            _usersetting = usersettingQuery;


        }


        public void Update(UserSetting UserSetting_)
        {
            var usersettingtoupdate = _qualityEntities.UserSettings
                .FirstOrDefault(x => x.UserID == UserSetting_.UserID);

            usersettingtoupdate.PlantCodeID = UserSetting_.PlantCodeID;
            usersettingtoupdate.LanguageID = UserSetting_.LanguageID;

            _qualityEntities.SaveChanges(SaveOptions.None);


        }




        public int Insert(UserSetting usersetting_)
        {
            try
            {

                var usersettingtoinsert = new UserSetting
                {
                    UserName = usersetting_.UserName,
                    LanguageID=usersetting_.LanguageID,
                    PlantCodeID=usersetting_.PlantCodeID

                   


                };

                _qualityEntities.AddToUserSettings(usersettingtoinsert);
                _qualityEntities.SaveChanges(SaveOptions.None);
                return usersettingtoinsert.UserID;

            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();
                return 0;
            }



        }



    }





}
