using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Printing;
using System.IO;
using System.Windows.Data;
using System.Windows.Documents;
using System.Collections.Specialized;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing.Printing;
using TravelCardPrint.Printing;
using System.Web;
using mshtml;
using System.Diagnostics;
using System.Drawing;
using TravelCardPrint.ViewModels;
using TravelCard.DomainModel.Entities;
using TravelCard.DomainModel.Abstract;
using TravelCard.DomainModel.Repositories;
using System.Management;
using Quality.WebUI;

using System.Globalization;



using System.Deployment.Application;



namespace TravelCardPrint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   
    public partial class MainWindow : Window
    {
     
        public  string partid { get; set; }
        public int PartSetUpID { get; set; }
        public string htmlString { get; set; }
        public string filename { get; set; }
        public int NumofContinuationOMIs { get; set; }
        public string redlightgreenlightfilepath{get;set;}

        public MainWindow()
        {

          

        }


       

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

           
        }

       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

         

            button1.Content = Quality.Resources.Strings.Cancel;

            btnPrintFiles.Visibility = Visibility.Hidden;
            btnPrintRedLight.Visibility = Visibility.Hidden;
       

            NameValueCollection querystrings = GetQueryStringParameters();
            string _language = "";
            string _isdraft = "";
            string _partid = "";
            string webapppath= System.Configuration.ConfigurationManager.AppSettings["OMIWebAppPath"].ToString();
            foreach (string key in querystrings.AllKeys)
            {
            if(key=="_isdraft")
            {
            
            _isdraft=querystrings["_isdraft"];
            
            }

                if(key=="_language")
                {
                
                _language=querystrings["_language"];
                
                }


                if (key == "_partid")
                {
                    _partid = querystrings["_partid"];

                }
            
            
            }

           
            
            if(_language=="es-MX" && _isdraft=="1")
            {
                webBrowser1.Navigate(webapppath+"TravelCard/Index?_isdraft=True&_language=es-MX&_partid="+_partid);
            }

            else if (_language == "es-MX" && _isdraft == "0")
            {
                webBrowser1.Navigate(webapppath + "TravelCard/Index?_isdraft=False&_language=es-MX&_partid=" + _partid);
            }

            else if (_language == "en-US" && _isdraft == "1")
            {
               webBrowser1.Navigate(webapppath + "TravelCard/Index?_isdraft=True&_language=en-US&_partid=" + _partid);
              
            }

            else if (_language == "en-US" && _isdraft == "0")
            {
                webBrowser1.Navigate(webapppath + "TravelCard/Index?_isdraft=False&_language=en-US&_partid=" + _partid);
            }
            else
            {
                webBrowser1.Navigate(webapppath);
            
            
            }
    
        }


        private NameValueCollection GetQueryStringParameters()
        {
            NameValueCollection nameValueTable = new NameValueCollection();

       //     if (ApplicationDeployment.IsNetworkDeployed)
          //  {
string queryString = ApplicationDeployment.CurrentDeployment.ActivationUri.Query;
             //use a real string when testing locally.
//string queryString = "http://tn-sqldevel:8888/omiprintingapp_dev/travelcardprint.application?_openapp=1&_language=en-US&_partid=4125052&_isdraft=1";
              nameValueTable = System.Web.HttpUtility.ParseQueryString(queryString);
           // }

            return (nameValueTable);
        }

        private void PrintRedLightGreenLightFile()
        {
  
            webBrowser1.Visibility = Visibility.Visible;
           if(File.Exists(this.redlightgreenlightfilepath))
           {
            webBrowser1.Navigate(new System.Uri(this.redlightgreenlightfilepath));
            Mouse.OverrideCursor = null;
           }
           else{
                 string filenotfound = System.Configuration.ConfigurationManager.AppSettings["greenlightreadlightfilenotfound"];
              if(File.Exists(filenotfound))
              {

                  webBrowser1.Navigate(new System.Uri(filenotfound));
              }
           
           }

        }

       private void printthefile()
       {

        PrinterHelper printhelp = new PrinterHelper();
        string defaultprintername = printhelp.DefaultPrinterName();
        System.Windows.Controls.PrintDialog pd = new System.Windows.Controls.PrintDialog();
         //  pd.ShowDialog();
       
           printhelp.PrintFile(filename);

          printhelp.kill();

    
        }


        public void TravelCardPrint()

        {
            TravelCardPrintViewModel viewModel = new TravelCardPrintViewModel

            {


            };
            var partsetup = viewModel.PartSetUp.Where(q => q.PartID == this.partid);
            string partsetupid = partsetup.Select(a => a.PartSetUpID).ToString();
            partid = partid.Trim();
            string workpdffilepath = System.Configuration.ConfigurationManager.AppSettings["pdfWorkFilePath"].ToString();
            string filename = workpdffilepath + partsetupid.Trim() + ".pdf";

      
        }

        public string BuildHtmlPage(string htmlcontent)
        {
            string formattedhtml = htmlcontent;

            formattedhtml = HttpUtility.UrlDecode(htmlcontent);
       
         string stylesheetpath = System.Configuration.ConfigurationManager.AppSettings["stylesheetpath"].ToString();
         string stylesheettag = "<link href='" + stylesheetpath + "' rel='stylesheet' type='text/css'" + "/>";
          
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><head><body>");
            sb.Append(stylesheettag);
            sb.Append("</head>");
            sb.Append(formattedhtml);
            sb.Append("</body></html>");

            string formattedresult = sb.ToString();

            return formattedresult;
        }
        public void OMIPrint()
        {
           
            string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["Quality_ConnString"].ToString();
         PartSetUpRepository partsetupRespository_ = new PartSetUpRepository(connectionstring);
          
                string html =HttpUtility.UrlDecode(htmlString);
                string formattedhtml = BuildHtmlPage(html);
                string baseurl = System.Configuration.ConfigurationManager.AppSettings["OMIWebAppPath"].ToString();
                var partsetup = partsetupRespository_.PartSetUp.FirstOrDefault(a => a.PartID ==this.partid.Trim() );
          
                string partid_ = partsetup.PartID;
                this.redlightgreenlightfilepath = partsetup.RedLightGreenLightFile != null||partsetup.RedLightGreenLightFile!="" ? partsetup.RedLightGreenLightFile : null;
                string filename_ = partid_;
                int numtoprint_ = this.NumofContinuationOMIs;

                var pdfgenerator = new pdfReportGenerator.OMIPdfGenerator(partid_, formattedhtml, filename_, true, numtoprint_, baseurl, partsetup, this.PartSetUpID);

           pdfgenerator.ExecuteResult();
           
           
        }


        private void webBrowser1_LoadCompleted(object sender, NavigationEventArgs e)
        {
            try
            {

                
               
                if (!webBrowser1.Source.IsFile)
                {
                    mshtml.HTMLDocument doc = (mshtml.HTMLDocument)webBrowser1.Document;
                    if (doc!=null)
                    {
                        string docurl = doc.url;
                        if(docurl.Contains("TravelCard/TravelCard"))
                        {
              
                          string html = "";
                           html = doc.body.innerHTML;
                           this.htmlString = html;
                           this.partid = "";
                           this.NumofContinuationOMIs = 0;
                           string part = null;
                        
                           part = doc.getElementById("partnum").innerText;
                            if (part != "" && part != null)
                            {
                               this.partid =part;
                            }
                            string numofcards = "";
                            //TODO: change the name of this span to something more meaningful.
                            numofcards = doc.getElementById("Span4").innerText;
                        
                          //  numofcards = "0";
                                                       
                            if (numofcards != null && numofcards != "")
                            
                            {
                                this.NumofContinuationOMIs = Convert.ToInt16(numofcards);
                            }
                            else
                            {
                                this.NumofContinuationOMIs=0;
                            }
                            this.partid.Trim();
                            if (this.partid.Length > 0)
                            {
                                Mouse.OverrideCursor = Cursors.Wait; 
                                webBrowser1.Visibility = Visibility.Hidden;
                                OMIPrint();
                                btnPrintFiles.Visibility = Visibility.Visible;
                                btnPrintFiles.IsEnabled = true;
                                btnPrintFiles.Background = System.Windows.Media.Brushes.Orange;
                                btnPrintFiles.Foreground = System.Windows.Media.Brushes.Black;
                                btnPrintFiles.Content = "Print all docs for part #" + this.partid;
                                if (this.redlightgreenlightfilepath != ""||this.redlightgreenlightfilepath!=null)
                                {
                                    btnPrintRedLight.Visibility = Visibility.Visible;
                                    btnPrintRedLight.Background = System.Windows.Media.Brushes.Green;
                                  

                                }
                                webBrowser1.Visibility = Visibility.Visible;
                                string pdfpath = System.Configuration.ConfigurationManager.AppSettings["pdfWorkFilePath"].ToString();
                                webBrowser1.Navigate(new System.Uri(pdfpath + this.partid.Trim() + "Merged.pdf"));
                                Mouse.OverrideCursor = null;
                            
                            }

                        }

                    }
                   
                }
            } 
       
            catch (Exception ex)
            {
                string message = ex.Message;
              

            }

        }

        private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void btnPrintFiles_Click(object sender, RoutedEventArgs e)
        {

        
            lblStatus.Content = "Status: Files are being sent to your default printer. Please wait...";
            string pdfpath = System.Configuration.ConfigurationManager.AppSettings["pdfWorkFilePath"].ToString();
            
            this.filename = pdfpath + this.partid.Trim() + "Merged.pdf";
            printthefile();
           
            lblStatus.Content = "Status: File(s) sent to printer. Please check your default printer.";

        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

            PrintRedLightGreenLightFile();

            MessageBox.Show("To print this file in color click the printer icon on the pdf.");

        }

       

       

  
    }
}
