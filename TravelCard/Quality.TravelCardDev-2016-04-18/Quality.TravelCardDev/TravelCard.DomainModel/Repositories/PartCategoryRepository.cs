using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelCard.DomainModel.Entities;
using TravelCard.DomainModel.Abstract;
using System.Data.Objects;



namespace TravelCard.DomainModel.Repositories
{
    public class PartCategoryRepository : IPartCategoryRepository
    {
        private Quality_devEntities _qualityEntities;
        private IQueryable<PartCategory> _partcategory;


        public IQueryable<PartCategory> PartCategory
        {
            get { return _partcategory; }
        }


        public PartCategoryRepository(string connectionString)
        {

            _qualityEntities = new Quality_devEntities(connectionString);
            ObjectQuery<PartCategory> partcategoryQuery = _qualityEntities.PartCategories;
            _partcategory = partcategoryQuery;


        }


        public void Update(PartCategory PartCategory_)
        {
            var categorytoupdate = _qualityEntities.PartCategories
                .FirstOrDefault(x => x.CategoryID == PartCategory_.CategoryID);

            categorytoupdate.CategoryName = PartCategory_.CategoryName;
            categorytoupdate.CategoryNameES = PartCategory_.CategoryNameES;
            categorytoupdate.CategoryNameCN = PartCategory_.CategoryNameCN;
            categorytoupdate.CategoryDescription = PartCategory_.CategoryDescription;
            categorytoupdate.CategoryDescriptionCN = PartCategory_.CategoryDescriptionCN;
            categorytoupdate.CategoryDescriptionES = PartCategory_.CategoryDescriptionES;
            categorytoupdate.IsActive = PartCategory_.IsActive;
            categorytoupdate.Notes = PartCategory_.Notes;
            categorytoupdate.LastEditDate = PartCategory_.LastEditDate;
            categorytoupdate.LastEditedBy = PartCategory_.LastEditedBy;
            
            _qualityEntities.SaveChanges(SaveOptions.None);


        }




        public int Insert(PartCategory partcategory_)
        {
            try
            {
               
                var partcategorytoinsert = new PartCategory
                {
                   CategoryID = partcategory_.CategoryID,

                    CategoryName = partcategory_.CategoryName,
                    CategoryNameES=partcategory_.CategoryNameES,
                   CategoryNameCN = partcategory_.CategoryNameCN,
                    CategoryDescription=partcategory_.CategoryDescription,
                    CategoryDescriptionES=partcategory_.CategoryDescriptionES,
                   CategoryDescriptionCN = partcategory_.CategoryDescriptionCN,
                   IsActive=true,
                   Notes=partcategory_.Notes,
                   LastEditedBy=partcategory_.LastEditedBy,
                   LastEditDate=partcategory_.LastEditDate,
                   CreateDate=partcategory_.CreateDate,
                   CreatedBy=partcategory_.CreatedBy

                   

                };

                _qualityEntities.AddToPartCategories(partcategorytoinsert);
                _qualityEntities.SaveChanges(SaveOptions.None);
                return partcategorytoinsert.CategoryID;

            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();
                return 0;
            }



        }
      

       
    }





}
