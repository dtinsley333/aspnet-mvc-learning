using System;
using System.Collections.Generic;
using System.Linq;
using ExpertPdf.MergePdf;
using System.IO;
using System.Web;
using Winnovative.WnvHtmlConvert;
using System.Text.RegularExpressions;
using System.Configuration;
using TravelCardPrint.ViewModels;
using TravelCard.DomainModel.Entities;
using TravelCard.DomainModel.Abstract;
using TravelCard.DomainModel.Repositories;

namespace pdfReportGenerator
{
    public class OMIPdfGenerator 
    {
        public string ContentType { get; set; }
        public string Content { get; set; }
        public string OutputFileName { get; set; }
        public bool ReturnAsAttachment { get; set; }
        public string[] HtmlElementIds { get; set; }

        public int Height { get; set; }
        public int Width { get; set; }
        public int NumofCopies { get; set; }
        public string BaseUrl { get; set; }
        public string QualityAlert1File { get; set; }
        public DateTime? QualityAlert1StartDte { get; set; }
        public DateTime? QualityAlert1EndDte { get; set; }
        public string QualityAlert2File { get; set; }
        public DateTime? QualityAlert2StartDte { get; set; }
        public DateTime? QualityAlert2EndDte { get; set; }
        public string MachineSetUpFile { get; set; }
        public string DeviationFile1Path { get; set; }
        public DateTime? DeviationFile1StartDte { get; set; }
        public DateTime? DeviationFile1EndDte { get; set; }
        public string DeviationFile2Path { get; set; }
        public DateTime? DeviationFile2StartDte { get; set; }
        public DateTime? DeviationFile2EndDte { get; set; }
        public string DrawingFilePath { get; set; }
        public string DieSetUpFilePath { get; set; }
        public string AdditionalFilePath { get; set; }
        public string FinalFileName { get; set; }
        public int PartSetUpId { get; set; }
        public string PartID { get; set; }
        public string HtmlString{get;set;}
        public string UserMessage { get; set; }


        public OMIPdfGenerator(string partid_, string html, string outputFileName, bool returnAsAttachment, int numofcopies, string baseurl, PartSetUp partsetup,  int partsetupid)
        {

            this.ContentType = "application/pdf";
            if (html == string.Empty)
            {
                html = "<h2>There was a problem. No data was returned.</h2>";
            }
            this.Content = html;
            this.OutputFileName = outputFileName;
            this.ReturnAsAttachment = true;
            this.NumofCopies = numofcopies;
            this.BaseUrl = baseurl;
            this.QualityAlert1File = partsetup.QualityAlertFile;
            this.QualityAlert1StartDte = partsetup.QualityAlertStartDte;
            this.QualityAlert1EndDte = partsetup.QualityAlertEndDte;
            this.QualityAlert2File = partsetup.QualityAlert2;
            this.QualityAlert2StartDte = partsetup.QualityAlert2StartDte;
            this.QualityAlert2EndDte = partsetup.QualityAlert2EndDte;
            this.MachineSetUpFile = partsetup.SetupDrawingFile;
            this.DeviationFile1Path = partsetup.DeviationFile;
            this.DeviationFile1StartDte = partsetup.DeviationFileStartDte;
            this.DeviationFile1EndDte = partsetup.DeviationFileEndDte;
            this.DeviationFile2Path = partsetup.DeviationFile2;
            this.DeviationFile2StartDte = partsetup.DeviationFile2StartDte;
            this.DeviationFile2EndDte = partsetup.DeviationFile2EndDte;
            this.DrawingFilePath = partsetup.DrawingFile;
            this.DieSetUpFilePath = partsetup.DieSetUpFile;
            this.AdditionalFilePath = partsetup.AdditionalFile;
            this.PartSetUpId = partsetupid;
            this.PartID = partid_;
            this.HtmlString=html;
            


        }

        public OMIPdfGenerator(string html, int height, int width, string outputFileName, bool returnAsAttachment)
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

        public void ExecuteResult()
        {

            try
            {

   
                bool selectablePDF = true;
                PdfConverter pdfConverter;
                // Create the PDF converter. Optionally you can specify the virtual browser width as parameter.

                //some pages require a custom height otherwise the data is cut off.
                pdfConverter = new PdfConverter(this.Width, this.Height);
                pdfConverter.PdfDocumentOptions.PdfPageSize = Winnovative.WnvHtmlConvert.PdfPageSize.Letter;


               
                if (System.Configuration.ConfigurationManager.AppSettings["htmltopdf.licensekey"] != String.Empty)
                {
                    pdfConverter.LicenseKey = System.Configuration.ConfigurationManager.AppSettings["htmltopdf.licensekey"];
                }


                pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression;
                pdfConverter.PdfDocumentOptions.PdfPageOrientation = Winnovative.WnvHtmlConvert.PDFPageOrientation.Landscape;
                pdfConverter.PdfDocumentOptions.ShowHeader = false;
                pdfConverter.PdfDocumentOptions.ShowFooter = false;
                pdfConverter.PdfDocumentOptions.InternalLinksEnabled = false;
                pdfConverter.PdfDocumentOptions.JpegCompressionEnabled = false;
                pdfConverter.PdfDocumentOptions.StretchToFit = true;
                pdfConverter.OptimizeMemoryUsage = false;
                pdfConverter.PdfDocumentOptions.LiveUrlsEnabled = true;
                pdfConverter.PdfDocumentOptions.LeftMargin = 9;
                pdfConverter.PdfDocumentOptions.RightMargin = 9;
                pdfConverter.PdfDocumentOptions.TopMargin = 9;
                pdfConverter.PdfDocumentOptions.BottomMargin =9;



                // set to generate selectable pdf or a pdf with embedded image
                pdfConverter.HtmlExcludedRegionsOptions.HtmlElementIds = new string[] { "Continuation" };
                pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = selectablePDF;
                byte[] pdfBytes = null;
                pdfBytes = pdfConverter.GetPdfBytesFromHtmlString(this.HtmlString);
                CreatePdfFiles(pdfConverter, this.HtmlString);
            }
            catch (Exception ex)
            {
                string errormessage = ex.ToString();
                string partsetupid = this.PartSetUpId.ToString();
           
            }
        }

        public void HandleQualityAlert1File(MergeEx merger)
        {
            try
            {
                //handle quality alert file number #1.
                //1. If the current date is withn the start and end dates then print it. 
                //2. If not in effect don't print it.
                //3  If not found print the "not found page".

                if (!String.IsNullOrEmpty(this.QualityAlert1File))
                {
                    DateTime? startdate = this.QualityAlert1StartDte;
                    DateTime? enddate = this.QualityAlert1EndDte;
                    bool ineffecterange = IsInDateRange(startdate, enddate);

                    if (System.IO.File.Exists(this.QualityAlert1File) && ineffecterange)
                    {
                        merger.AddFile(this.QualityAlert1File);
                    }
                    if (System.IO.File.Exists(this.QualityAlert1File)==false && ineffecterange)
                    {
                        string filenotfound = System.Configuration.ConfigurationManager.AppSettings["qualityalertfile1notfound"];
                        if (System.IO.File.Exists(filenotfound))
                        {
                            merger.AddFile(filenotfound);

                        }
                    }

                }
            }
            catch (Exception ex)
            {

                string filenotfound = System.Configuration.ConfigurationManager.AppSettings["qualityalertfile1notfound"];
                if (System.IO.File.Exists(filenotfound))
                {
                    merger.AddFile(filenotfound);

                }

            }


        }
      
    public void HandleQualityAlert2File(MergeEx merger)
            {

                try
                {
                    //handle quality alert file number #2.
                    //1. If the current date is withn the start and end dates then print it. 
                    //2. If not in effect don't print it.
                    //3  If not found print the "not found page".

                    if (!String.IsNullOrEmpty(this.QualityAlert2File))
                    {
                        DateTime? startdate = this.QualityAlert2StartDte;
                        DateTime? enddate = this.QualityAlert2EndDte;
                        bool ineffecterange = IsInDateRange(startdate, enddate);

                        if (System.IO.File.Exists(this.QualityAlert2File) && ineffecterange)
                        {
                            merger.AddFile(this.QualityAlert2File);
                        }
                        if (System.IO.File.Exists(this.QualityAlert2File)==false && ineffecterange)
                        {
                            string filenotfound = System.Configuration.ConfigurationManager.AppSettings["qualityalertfile2notfound"];
                            if (System.IO.File.Exists(filenotfound))
                            {
                                merger.AddFile(filenotfound);

                            }
                        }

                    }
                }
                catch (Exception ex)
                {

                    string filenotfound = System.Configuration.ConfigurationManager.AppSettings["qualityalertfile2notfound"];
                    if (System.IO.File.Exists(filenotfound))
                    {
                        merger.AddFile(filenotfound);

                    }

                }

  
  
  
            }

    public void HandleDeviation1File(MergeEx merger)
    {

        try
        {
            //handle deviation file number 1.
            //1. If the current date is withn the start and end dates then print it. 
            //2. If not in effect don't print it.
            //3  If not found print the "not found page".

            if (!String.IsNullOrEmpty(this.DeviationFile1Path))
            {
                DateTime? startdate = this.DeviationFile1StartDte;
                DateTime? enddate = this.DeviationFile1EndDte;
                bool ineffecterange = IsInDateRange(startdate, enddate);

                if (System.IO.File.Exists(this.DeviationFile1Path) && ineffecterange)
                {
                    merger.AddFile(this.DeviationFile1Path);
                }
                if (System.IO.File.Exists(this.DeviationFile1Path)==false&& ineffecterange)
                {
                    string filenotfound = System.Configuration.ConfigurationManager.AppSettings["deviationfilenum1notfound"];
                    if (System.IO.File.Exists(filenotfound))
                    {
                        merger.AddFile(filenotfound);

                    }
                }

            }
        }
        catch (Exception ex)
        {

            string filenotfound = System.Configuration.ConfigurationManager.AppSettings["deviationfilenum1notfound"];
            if (System.IO.File.Exists(filenotfound))
            {
                merger.AddFile(filenotfound);

            }

        }
    
    
    
    
    }

    public void HandleDeviation2File(MergeEx merger)
    {
        try
        {
            //handle deviation file number 2.
            //1. If the current date is withn the start and end dates then print it. 
            //2. If not in effect don't print it.
            //3  If not found print the "not found page".

            if (!String.IsNullOrEmpty(this.DeviationFile2Path))
            {
                DateTime? startdate = this.DeviationFile2StartDte;
                DateTime? enddate = this.DeviationFile2EndDte;
                bool ineffecterange = IsInDateRange(startdate, enddate);

                if (System.IO.File.Exists(this.DeviationFile2Path) && ineffecterange)
                {
                    merger.AddFile(this.DeviationFile2Path);
                }
                if (System.IO.File.Exists(this.DeviationFile2Path)==false && ineffecterange)
                {
                    string filenotfound = System.Configuration.ConfigurationManager.AppSettings["deviationfilenum2notfound"];
                    if (System.IO.File.Exists(filenotfound))
                    {
                        merger.AddFile(filenotfound);

                    }
                }

            }
        }
        catch (Exception ex)
        {

            string filenotfound = System.Configuration.ConfigurationManager.AppSettings["deviationfilenum2notfound"];
            if (System.IO.File.Exists(filenotfound))
            {
                merger.AddFile(filenotfound);

            }

        }
    
    
    
    
    
    }
    public void HandleDieSetUpInstructions(MergeEx merger)
    {
        try
        {
            if (!String.IsNullOrEmpty(this.DieSetUpFilePath))
            {
                if (File.Exists(this.DieSetUpFilePath))
                {
                    merger.AddFile(this.DieSetUpFilePath);
                }
                else
                {
                    string filenotfound = System.Configuration.ConfigurationManager.AppSettings["diesetupfilenotfound"];
                    if (System.IO.File.Exists(filenotfound))
                    {
                        merger.AddFile(filenotfound);
                    }
                }

            }
        }
        catch (Exception ex)
        {
            string filenotfound = System.Configuration.ConfigurationManager.AppSettings["diesetupfilenotfound"];
            if (System.IO.File.Exists(filenotfound))
            {
                merger.AddFile(filenotfound);
            }

        }
    
    
    
    
    
    }
    public void HandleAdditionalFile(MergeEx merger)
    {

        try
        {
            if (!String.IsNullOrEmpty(this.AdditionalFilePath))
            {
                if (System.IO.File.Exists(this.AdditionalFilePath))
                {

                    merger.AddFile(this.AdditionalFilePath);

                }
                else
                {
                    string filenotfound = System.Configuration.ConfigurationManager.AppSettings["additionalfilenotfound"];
                    if (System.IO.File.Exists(filenotfound))
                    {
                        merger.AddFile(filenotfound);
                    }

                }

            }
        }
        catch (Exception ex)
        {

            string filenotfound = System.Configuration.ConfigurationManager.AppSettings["additionalfilenotfound"];
            if (System.IO.File.Exists(filenotfound))
            {
                merger.AddFile(filenotfound);
            }
        }
    
    
    }


    public void HandleDrawingFile(MergeEx merger)
    {
        try
        {

            if (!String.IsNullOrEmpty(this.DrawingFilePath))
            {
                if (File.Exists(this.DrawingFilePath))
                {
                    merger.AddFile(this.DrawingFilePath);
                }
                else
                {
                    string filenotfound = System.Configuration.ConfigurationManager.AppSettings["drawingfilenotfound"];
                    if (System.IO.File.Exists(filenotfound))
                    {
                        merger.AddFile(filenotfound);


                    }
                }

            }
        }

        catch (Exception ex)
        {
            this.UserMessage = "An error occurred when the system attempted to merge a Drawing file to the Travel Card. " + ex.ToString();
        }
    
    
    
    
    }
        
        
        public void HandleMachineSetUpFile(MergeEx merger)
        {
            try
            {
                if (!String.IsNullOrEmpty(this.MachineSetUpFile))
                {
                    if (System.IO.File.Exists(this.MachineSetUpFile))
                    {

                        merger.AddFile(this.MachineSetUpFile);

                    }
                    else
                    {
                        string filenotfound = System.Configuration.ConfigurationManager.AppSettings["machinesetupfilenotfound"];
                        if (System.IO.File.Exists(filenotfound))
                        {
                            merger.AddFile(filenotfound);
                        }

                    }

                }
            }
            catch (Exception ex)
            {

                string filenotfound = System.Configuration.ConfigurationManager.AppSettings["machinesetupfilenotfound"];
                if (System.IO.File.Exists(filenotfound))
                {
                    merger.AddFile(filenotfound);
                }
            }
        
        }
        public bool IsInDateRange(DateTime? startDate, DateTime? endDate)
        {
          
         
            bool shouldprint=false;
            DateTime currentdate=DateTime.Now;
            //current date is between start date and end date means it prints.

            if (startDate == null && endDate== null)
            {
                shouldprint = true;
            }
            if (startDate != null&&endDate!=null)
            {
                if ((startDate <= currentdate) && (endDate >= currentdate))
                {
                    shouldprint = true;
                
                }
               

            }
            if (startDate != null)
            {
                //start date with no end date means it prints. 
                if ((startDate <= currentdate) && (endDate == null))
                {
                    shouldprint = true;
                }
            }

            if (endDate <=currentdate)
            {
                
                    shouldprint = false;
               
            }
            return shouldprint;
        }
        
        
        public void CreatePdfFiles(PdfConverter pdfConverter, string htmlString)
        {

            string pdfWorkFilePath = System.Configuration.ConfigurationManager.AppSettings["pdfWorkFilePath"];
            MergeEx merger= new MergeEx();
            string existingpdf = pdfWorkFilePath + this.OutputFileName.ToString() + ".pdf";
            string[] Files = System.IO.Directory.GetFiles(pdfWorkFilePath);

                int count = 0;
                string filename = this.OutputFileName.ToString() + ".pdf";
                string finalfilename = this.OutputFileName.ToString() + "MergedDocs.pdf";
                pdfConverter.SavePdfFromHtmlStringToFile(htmlString, pdfWorkFilePath + filename);
              
            

                merger.DestinationFile=pdfWorkFilePath + this.PartID.ToString()+"Merged.pdf";
                merger.SourceFolder=pdfWorkFilePath + filename;

                if (this.NumofCopies > 0)
                {
                    //save html for CC Card
                    string htmlCC = htmlString;
                 
                    htmlCC = htmlString.Replace("<SPAN id=FRONTPAGEBEGIN></SPAN>", "<H2>CONTINUATION TRAVEL CARD for "+this.PartID + "</H2>");
                    htmlCC = htmlCC.Replace("<SPAN id=ccMessage></SPAN>", "<SPAN id=ccMessage>&nbsp;&nbsp;&nbsp;CONTINUATION CARD</SPAN>");
                    htmlCC = htmlCC.Replace("<SPAN id=ccMessage2></SPAN>", "<SPAN id=ccMessage>&nbsp;&nbsp;&nbsp;CONTINUATION CARD</SPAN>");
                    string ccfilename = this.OutputFileName.ToString() + "Continuation.pdf";
                   
                    PdfConverter ccPdf = new PdfConverter(this.Width, this.Height);
                    ccPdf.PdfDocumentOptions.PdfPageSize = Winnovative.WnvHtmlConvert.PdfPageSize.Letter;

                    //NOTE: Users did not want the front page of the travel card to printing out on continuation card.  I was able to blank out the content on the first page could not figure out to eliminate
                    //the first page. It is probably possible and may be a good thing to refactor in the future.
                    ccPdf.HtmlExcludedRegionsOptions.HtmlElementIds = new string[] { "CONTENTONFRONT",  "BREAKFIRSTPAGE", "FRONTCONTENT" };

                    if (System.Configuration.ConfigurationManager.AppSettings["htmltopdf.licensekey"] != String.Empty)
                    {
                        ccPdf.LicenseKey = System.Configuration.ConfigurationManager.AppSettings["htmltopdf.licensekey"];
                    }

                  

                    ccPdf.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression;
                    ccPdf.PdfDocumentOptions.PdfPageOrientation = Winnovative.WnvHtmlConvert.PDFPageOrientation.Landscape;
                    ccPdf.PdfDocumentOptions.ShowHeader = false;
                    ccPdf.PdfDocumentOptions.ShowFooter = false;
                    ccPdf.PdfDocumentOptions.InternalLinksEnabled = false;
                    ccPdf.PdfDocumentOptions.JpegCompressionEnabled = false;
                    ccPdf.PdfDocumentOptions.StretchToFit = true;
                    ccPdf.OptimizeMemoryUsage = false;
                    ccPdf.PdfDocumentOptions.LiveUrlsEnabled = true;
                    ccPdf.PdfDocumentOptions.LeftMargin = 9;
                    ccPdf.PdfDocumentOptions.RightMargin = 9;
                    ccPdf.PdfDocumentOptions.TopMargin = 9;
                    ccPdf.PdfDocumentOptions.BottomMargin = 9;


                    ccPdf.SavePdfFromHtmlStringToFile(htmlCC, pdfWorkFilePath + ccfilename);


                    merger.AddFile(pdfWorkFilePath + filename);
                    int counter = 0;
                    do
                    {


                        counter++;
                        if (counter > 5)
                        {
                            //don't get stuck in a loop.
                            //users agreed at 5 cards printed at a time would be enough 1/2012 
                            break;

                        }


                        merger.AddFile(pdfWorkFilePath + ccfilename);
                    } while (counter < this.NumofCopies + 1);

                }
                else {
                    merger.AddFile(pdfWorkFilePath + filename);
                
                }
                if (this.DrawingFilePath != null || this.DrawingFilePath !=String.Empty)
                {
                    HandleDrawingFile(merger);
                }
                if (this.DeviationFile1Path != null || this.DeviationFile1Path != String.Empty)
                {
                    HandleDeviation1File(merger);
                }
                if (this.DeviationFile2Path != null || this.DeviationFile2Path != String.Empty)
                {
                    HandleDeviation2File(merger);
                }
                if (this.QualityAlert1File != null || this.QualityAlert1File != String.Empty)
                {
                    HandleQualityAlert1File(merger);
                }
                if (this.QualityAlert2File != null || this.QualityAlert2File != String.Empty)
                {
                    HandleQualityAlert2File(merger);
                }
                if (this.MachineSetUpFile != null || this.MachineSetUpFile != String.Empty)
                {
                    HandleMachineSetUpFile(merger);
                }
                if (this.AdditionalFilePath!= null || this.AdditionalFilePath != String.Empty)
                {
                    HandleAdditionalFile(merger);
                }
                if (this.DieSetUpFilePath != null || this.DieSetUpFilePath != String.Empty)
                {
                    HandleDieSetUpInstructions(merger);
                }

              
                count++;
                merger.Execute();


        }


    }

}


