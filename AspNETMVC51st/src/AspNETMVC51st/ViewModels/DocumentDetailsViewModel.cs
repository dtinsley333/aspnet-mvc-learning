using System;
using System.Collections.Generic;
using System.Linq;

using AspNETMVC51st.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Http;

namespace AspNETMVC51st.ViewModels
{
    public class DocumentDetailsViewModel
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public byte[] Contents { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadUserId { get; set; }
        public List<Document> Documents { get; set; }
        [FileExtensions(Extensions = "jpg,jpeg")]
        public IFormFile File { get; set; }

    }
}