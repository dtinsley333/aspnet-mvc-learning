using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurgeEd.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public Byte[] Contents {get;set;}
        public DateTime UploadDate { get; set; }
    }
}