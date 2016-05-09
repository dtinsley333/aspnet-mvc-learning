using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using Quality.WebUI.Extensions;
using Quality.ViewModels;
using TravelCard.DomainModel.Repositories;
using TravelCard.DomainModel.Abstract;
using TravelCard.DomainModel.Entities;
using System.Configuration;


using GenerateDocs;

namespace Quality.WebUI.Controllers
{
    public class ReportsController : BaseController
    {

      IPartSetUpRepository _partsetupRepository;
        IPartCategoryRepository _partCategoryRepository;


        public ReportsController(IPartSetUpRepository partsetupRespository_, IPartCategoryRepository partcategoryRepository_)
        {
            _partsetupRepository = partsetupRespository_;
            _partCategoryRepository = partcategoryRepository_;
        }
        
        
        
        
        public PdfResult WrapperSectionReport(string html_, string filename_)
        {
            try
            {

                string html = HttpUtility.UrlDecode(html_);
                string formattedhtml = BuildHtmlPage(html);


                return new PdfResult(formattedhtml, filename_, true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
      
        public ExcelOutput ExcelReport(string html_, string filename_, int numtoprint_, int partsetupid_)
        {
            try
            {

                string html = HttpUtility.UrlDecode(html_);
                string formattedhtml = BuildHtmlPage(html);
                string baseurl = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority) + "/";
                var partsetup = _partsetupRepository.PartSetUp.FirstOrDefault(a => a.PartSetUpID == partsetupid_);
                string qualityalertfilepath = (partsetup.QualityAlertFile != null) ? partsetup.QualityAlertFile.ToString() : "";
                string machinesetupfilepath = (partsetup.SetupDrawingFile != null) ? partsetup.SetupDrawingFile.ToString() : "";
                string deviationsetupfilepath = (partsetup.DeviationFile != null) ? partsetup.DeviationFile.ToString() : "";
                string drawingfilepath = (partsetup.DrawingFile != null) ? partsetup.DrawingFile.ToString() : "";
                string diesetupfilepath = (partsetup.DieSetUpFile != null) ? partsetup.DieSetUpFile.ToString() : "";



                //pass in deviation files array
                //  string[] deviationpaths = GetDeviationPaths(partsetupid_);
                // pass in machine setup 
                //pass in quality alert array
                //drawing file
                return new ExcelOutput(formattedhtml, filename_, true);
               // return new ExcelOutput(formattedhtml, filename_, true, 1, baseurl, qualityalertfilepath, machinesetupfilepath, deviationsetupfilepath, drawingfilepath, diesetupfilepath, numtoprint_, partsetupid_);

            }
            catch (Exception ex)
            {
                return null;
            }
        }
  

     
      
        public ActionResult DisplayOutPuttedPDF(string _filename)

        {
     ///
         
            ViewData["filename"] = _filename;

            ViewData["userMessage"] = "";

            string filepath = ViewData["filename"].ToString();
            ViewData["seconds"] = "";
            
            ViewData["success"] = "true";
            bool fileexists = false;
            int count = 0;
            do
            {


                fileexists = System.IO.File.Exists(filepath);

                count++;
                if (count == 350)
                {
                    ViewData["success"] = "false";
                    ViewData["userMessage"] = "Sorry, cannot retrieve the file at this time. The system timed out. Try clicking the browser's back button.";
                    ViewData["seconds"]=count.ToString();
                    return View();
                }
                System.Threading.Thread.Sleep(1000); 
            }
            while (fileexists == false);

            ViewData["seconds"] = count.ToString();
            return View();
        
        
        }
        public ActionResult Showpdf(string html_, string filename_, int numtoprint_, int partsetupid_)
        {
            string pdfWorkFilePath = ConfigurationManager.AppSettings["pdfWorkFilePath"];
            ViewData["userMessage"]="";
              ViewData["filename"] = pdfWorkFilePath + filename_ + "-0.pdf";
           
           
            return View();
        }

        public string BuildHtmlPage(string htmlcontent)
        {
            string formattedhtml = htmlcontent;

            formattedhtml = HttpUtility.UrlDecode(htmlcontent);

           //string stylesheetpath = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority) + "/content/site.css";
           //string stylesheettag = "<link href='" + stylesheetpath + "' rel='stylesheet' type='text/css'" + "/>";
       //  formattedhtml = formattedhtml.Replace("images/ideallogo.gif"><BR>'", "<IMG src='http://tn-sqldevel:3333/content/images/ideallogo.jpg'/>");
            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
           // sb.Append(stylesheettag);
            sb.Append(formattedhtml);
            sb.Append("</html>");

            string formattedresult = sb.ToString();

            return formattedresult;
        }

        public PdfResult StandardPdfReport(string html_, string filename_)
        {
            try
            {

                string html = HttpUtility.UrlDecode(html_);
             ///    string productionstylesheetpath = "http://tn-sqldevel:3333/content/site.css";
               //  html = html.Replace("Content/Styles.css", productionstylesheetpath);
              //   html = "<html>" + html + "</div></html>";

                return new PdfResult(html, filename_, true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



















        //public ExcelOutput ExcelExport(string html_, string filename_)
        //{
        //    try
        //    {

        //        string html = HttpUtility.UrlDecode(html_).Replace("../", "");
        //        string productionstylesheetpath = "http://qrmapp.chs.net/Startup/content/styles.css";
        //        html = html.Replace("Content/Styles.css", productionstylesheetpath);
        //        html = "<html>" + html + "</div></html>";

        //        return new ExcelOutput(html, filename_, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}



      

    }
}

