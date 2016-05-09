using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TravelCard.DomainModel.Entities
{
    [MetadataType(typeof(PartCategoryMetaData))]
    public partial class PartCategory
    {



    }

    public class PartCategoryMetaData
    {
        
        [DisplayName("Category ID")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "An English Category Name is required.")]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        [DisplayName("Category Name(Spanish)")]
        public string CategoryNameES { get; set; }

        [DisplayName("Category Name(Chinese)")]
        public string CategoryNameCN { get; set; }

        [DisplayName("Part Category Description")]
        public string CategoryDescription { get; set; }

        [DisplayName("Part Category Description (Chinese)")]
        public string CategoryDescriptionCN { get; set; }

        [DisplayName("Part Category Description (Spanish)")]
        public string CategoryDescriptionES { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [DisplayName("Notes(Does not display on the travel card)")]
        public string Notes { get; set; }


        [DisplayName("Last Editor")]
        public string LastEditedBy { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        [DisplayName("Create Date")]
        public DateTime CreateDate { get; set; }

        [DisplayName("Last Edit Date")]
        public DateTime LastEditDate { get; set; }



    }
}
