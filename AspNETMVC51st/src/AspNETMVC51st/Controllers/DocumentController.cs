using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using AspNETMVC51st.Models;
using AspNETMVC51st.ViewModels;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.IO;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNETMVC51st.Controllers
{
    public class DocumentController : Controller
    {
        private ApplicationDbContext dbContext {get;set;}
        public DocumentController(ApplicationDbContext context)
        {
            dbContext = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            

                var docs = (from d in dbContext.Document
                            orderby d.FileName
                            select d).ToList();

                DocumentDetailsViewModel vm = new DocumentDetailsViewModel()
                {
                    Documents = docs
                };
                return View(vm);
         
        }

        [HttpPost]
        public IActionResult Index(IFormFile file, DocumentDetailsViewModel docViewModel)
        {
            
                if (file != null && file.Length > 0)
                    try
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        using (var reader = new StreamReader(file.OpenReadStream()))
                        {
                            string contentAsString = reader.ReadToEnd();
                            byte[] bytes = new byte[contentAsString.Length * sizeof(char)];
                            System.Buffer.BlockCopy(contentAsString.ToCharArray(), 0, bytes, 0, bytes.Length);
                            var fileType = file.ContentType;
                            Document doc = new Document()
                            {
                                Title = docViewModel.Title,
                                FileName = fileName,
                                Contents = bytes,
                                ContentType=fileType,
                                UploadDate = DateTime.Now,
                                UploadUserId = "dtinsley91@gmail.com"

                            };
                            dbContext.Document.Add(doc);
                            dbContext.SaveChanges();
                        }

                        ViewBag.Message = "File uploaded successfully";

                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                //go back to index and the new doc should show

                var docs = from d in dbContext.Document
                           orderby d.FileName
                           select d;




                DocumentDetailsViewModel vm = new DocumentDetailsViewModel()
                {
                    Documents = docs.ToList()

                };
                return View(vm);
            }
     


        public IActionResult FileDownLoad(int documentId)
        {

                var fileId = documentId;

             var myFile = dbContext.Document.Where(d=>d.DocumentId==documentId).FirstOrDefault();
             //  var myFile = dbContext.Document.Find(documentId).FirstOrDefault();
                if (myFile != null)
                {
                    byte[] file = myFile.Contents;

                    var cd = new System.Net.Mime.ContentDisposition
                    {
                        FileName = myFile.FileName,

                        // always prompt the user for downloading, set to true if you want
                        // the browser to try to show the file inline
                        Inline = true,
                    };
                 
 
                    Response.ContentType = myFile.ContentType;
                    Response.Headers.Add("Content-Disposition", myFile.ContentType);
                    return File(myFile.Contents, myFile.ContentType);

                }
                else return null;

            }
        }
    }
  

