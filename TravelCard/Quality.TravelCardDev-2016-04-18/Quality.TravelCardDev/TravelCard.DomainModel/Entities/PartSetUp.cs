
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TravelCard.DomainModel.Entities
{
    [MetadataType(typeof(PartSetUpMetaData))]
    public partial class PartSetUp
    {
        public string FullPartSetUpDesc
        {

            get
            {
                return this.PartID + " - " + this.PartCategory.CategoryName + "--Release ready=" + this.IsReleaseReady.ToString();
            }

        }


    }

    public class PartSetUpMetaData
    {
        [DisplayName("Part Set UP ID")]
        public int PartSetUpID { get; set; }

        [DisplayName("Part ID")]
        public string PartID { get; set; }


        [DisplayName("Part Category")]
        public int CategoryID { get; set; }

        [DisplayName("Pack Code")]
        public int PackCode { get; set; }

        [DisplayName("Created By")]
        public int CreatedBy { get; set; }

        [DisplayName("Create Date")]
        public DateTime CreateDate { get; set; }

        [RegularExpression(@"^.*\.(pdf|PDF)$ ",
            ErrorMessage = "Only pdf files are allowed.")]

        [DisplayName("Drawing File")]
        public string DrawingFile { get; set; }


        [DisplayName("Drawing Number")]
        public string DrawingNumber { get; set; }


        [DisplayName("Red Light Green Light File")]
        public string RedLightGreenLightFile { get; set; }

        //first quality Alert
        [DisplayName("Quality Alert #1")]
        public string QualityAlertFile { get; set; }


        [DisplayName("Quality #1 Start Date")]
        public DateTime QualityAlertStartDte { get; set; }

        [DisplayName("Quality #1 End Date")]
        public DateTime QualityAlertEndDte { get; set; }

        //2nd quality alert
        [DisplayName("Quality Alert #2")]
        public string QualityAlert2 { get; set; }


        [DisplayName("Quality #2 Start Date")]
        public DateTime QualityAlert2StartDte { get; set; }

        [DisplayName("Quality #2 End Date")]
        public DateTime QualityAlert2EndDte { get; set; }






        //First Deviation File-NOTE: the attachments should probably be in their own database table, 
        //particulary the attachments that have start and end effective dates. 
        //More attachments were added as the project progressed.
        //This should be refactored in the future.

        //First DeviationFile
        [DisplayName("Deviation File")]
        public string DeviationFile { get; set; }

        [DisplayName("DeviationFileStartDte")]
        public DateTime DeviationFileStartDte { get; set; }

        [DisplayName("DeviationFileEndDte")]
        public DateTime DeviationFileEndDte { get; set; }

        //2nd Deviation File
        [DisplayName("Deviation File # 2")]
        public string DeviationFile2 { get; set; }

        [DisplayName("DeviationFile2StartDte")]
        public DateTime DeviationFile2StartDte { get; set; }

        [DisplayName("DeviationFile2EndDte")]
        public DateTime DeviationFile2EndDte { get; set; }

        [DisplayName("Additional File")]
        public String AdditionalFile { get; set; }

        [DisplayName("Die Set Up File")]
        public string DieSetUpFile { get; set; }

        [DisplayName("Revision")]
        public string Revision { get; set; }

        [DisplayName("Machine Set Up Instructions")]
        public string SetupDrawingFile { get; set; }

        [DisplayName("Has Quality Alert")]
        public bool HasQualityAlert { get; set; }

        [DisplayName("Is this part setup release ready?")]
        public bool IsReleaseReady { get; set; }

        [DisplayName("Notes: (Not visible on travel card)")]
        public string Notes { get; set; }

        [DisplayName("Water Mark: (Communication Notes)")]
        public string CommunicationNote { get; set; }


        [DisplayName("Part Comment")]
        public string PartComment { get; set; }

        [DisplayName("Part Remarks")]
        public string PartRemarks { get; set; }


    }
}
