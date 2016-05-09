using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Management;
using System.Threading;

namespace TravelCardPrint.Printing
{
    class PrinterHelper
    {
        #region Member Variables & Method Signatures
        #region " CONSTANTS "
        private const int SW_SHOWNORMAL = 2;
        #endregion
        #region " API "
        [DllImport("shell32")]
       
        public static extern IntPtr ShellExecute(IntPtr hWnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);
        #endregion

        public bool PrintFile(string FilePath)
        {
            string path = FilePath;
            try
            {
                if (System.IO.File.Exists(FilePath))
                {
                    if (ShellExecute((IntPtr)(1), "Print", FilePath, "", Directory.GetDirectoryRoot(FilePath), SW_SHOWNORMAL).ToInt32() <= 32)
                    {
                        return false;
                    }

                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
            //TODO; do something with this exception
                return false;
            
            }
        }

        
        
      
          
      
        public void kill()
        {
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.ProcessName.StartsWith("Acro"))
                {
                    string proname = proc.ProcessName.ToString();
                    if (proc.HasExited == false)
                    {
                        proc.WaitForExit(10000);
                        string title = proc.MainWindowTitle.ToString();
                        if (title == "Adobe Reader" && proname == "AcroRd32")
                        {
                            proc.Kill();
                            break;
                        }

                    }
                    else
                    {
                        try
                        {
                            proc.Kill();
                            break;
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
            }
        }



      public string DefaultPrinterName() 
      {
         System.Drawing.Printing.PrinterSettings oPS = new System.Drawing.Printing.PrinterSettings();
         string defaultprintername = "";

         try
             {
             defaultprintername = oPS.PrinterName;

            }
         catch (Exception ex)
            {
             defaultprintername = "";

             }
            finally
             { 
            oPS = null;
            }



     return defaultprintername;

          }





          







    }
        #endregion
}