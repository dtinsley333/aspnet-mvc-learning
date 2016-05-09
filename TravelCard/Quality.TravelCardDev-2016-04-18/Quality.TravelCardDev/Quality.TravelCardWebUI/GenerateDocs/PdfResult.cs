using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Winnovative.WnvHtmlConvert;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web.Mvc;
//using Elmah;
namespace GenerateDocs
{
    public class PdfResult : ViewResult
    {
     
        public string ContentType { get; set; }
        public string Content { get; set; }
        public string OutputFileName { get; set; }
        public bool ReturnAsAttachment { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public PdfResult(string html, string outputFileName, bool returnAsAttachment)
        {

            this.ContentType = "application/pdf";
            if (html == string.Empty)
            {
                html = "<h2>There was a problem. No data was returned.</h2>";
            }
            this.Content = html;
            this.OutputFileName = outputFileName;
            this.ReturnAsAttachment = returnAsAttachment;
        }

        public PdfResult(string html, int height, int width, string outputFileName, bool returnAsAttachment)
        {
            //this constructor was made for pdfs that need customized heights set.
            this.ContentType = "application/pdf";
            if (html == string.Empty)
            {
                html = "<h2>There was a problem. No data was returned.</h2>";
            }
            this.Content = html;
            this.OutputFileName = outputFileName;
            this.ReturnAsAttachment = returnAsAttachment;
            this.Width = width;
            this.Height = height;
        }

        public override void ExecuteResult(ControllerContext context)
        {

            try
            {
               
                context.HttpContext.Response.ContentType = ContentType;
                string baseURL = "";
                string htmlString = this.Content;

                bool selectablePDF = true;
                PdfConverter pdfConverter;
                // Create the PDF converter. Optionally you can specify the virtual browser width as parameter.

                //some pages require a custom height otherwise the data is cut off.
                pdfConverter = new PdfConverter(this.Width, this.Height);

                //If there is no license key, defaults to demo version
                if (ConfigurationManager.AppSettings["htmltopdf.licensekey"] != String.Empty)
                {
                    pdfConverter.LicenseKey = ConfigurationManager.AppSettings["htmltopdf.licensekey"];
                }

                pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression;
                pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;
                pdfConverter.PdfDocumentOptions.ShowHeader = false;
                pdfConverter.PdfDocumentOptions.ShowFooter = false;
                pdfConverter.PdfDocumentOptions.StretchToFit = true;
                pdfConverter.PdfDocumentOptions.InternalLinksEnabled = true;
             
                pdfConverter.OptimizePdfPageBreaks = true;
                 pdfConverter.PdfDocumentOptions.LiveUrlsEnabled = true;
              // pdfConverter.PdfDocumentOptions.FitWidth = true;
              // pdfConverter.PdfDocumentOptions.FitHeight = true;
       
               pdfConverter.PdfDocumentOptions.LeftMargin = 3;
               pdfConverter.PdfDocumentOptions.RightMargin = 3;
                pdfConverter.PdfDocumentOptions.TopMargin = 3;
                pdfConverter.PdfDocumentOptions.BottomMargin = 3;
              
           
               

                // set to generate selectable pdf or a pdf with embedded image
                pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = selectablePDF;
                byte[] pdfBytes = null;

                if (baseURL.Length > 0)
                {
                    pdfBytes = pdfConverter.GetPdfBytesFromHtmlString(htmlString, baseURL);
                }
                else
                {
                    pdfBytes = pdfConverter.GetPdfBytesFromHtmlString(htmlString);
                }




                // send the PDF document as a response to the browser for download
                // System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                context.HttpContext.Response.Clear();

                context.HttpContext.Response.AddHeader("Content-Type", "application/pdf");

                if (this.ReturnAsAttachment)
                {
                    //add timestamp to outputted file
                    string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HHmmssffff");


                    context.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + this.OutputFileName + timestamp + ".pdf; size=" + pdfBytes.Length.ToString());
                }
                //Disabling buffering so that, the file content is immediately written to output stream
                context.HttpContext.Response.Buffer = false;

                context.HttpContext.Response.BinaryWrite(pdfBytes);
                //No need to Flush() and End() the response as the ouptpu stream is not buffered. 
                //These were commented out because it was causing an error whenever the user cancelled the download
                //  context.HttpContext.Response.Flush();
                // context.HttpContext.Response.End();
             //    context.HttpContext.Response.RedirectToRoute("index");
            }
            catch(Exception ex)
            {
            //if the error occurs because the user clicks the "x" on the open/save pdf dialog box just swallow, otherwise log via elmah

                if ((ex.GetBaseException() is System.Web.HttpException) == false)
                {

                    throw ex;
                }
              
            }

        }
    }

}