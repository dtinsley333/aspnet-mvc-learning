using System;
using System.Collections.Generic;
using System.Linq;


namespace AspNETMVC51st.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public Byte[] Contents {get;set;}
        public DateTime UploadDate { get; set; }
        public string UploadUserId { get; set; }
    }
}