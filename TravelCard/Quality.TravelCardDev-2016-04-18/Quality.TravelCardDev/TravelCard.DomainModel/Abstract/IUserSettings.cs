using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;

namespace TravelCard.DomainModel.Abstract
{
    public interface IUserSettingRepository
    {

        IQueryable<UserSetting> UserSetting { get; }



       void Update(UserSetting usersetting_);
       int Insert(UserSetting usersetting_);

    }
}
