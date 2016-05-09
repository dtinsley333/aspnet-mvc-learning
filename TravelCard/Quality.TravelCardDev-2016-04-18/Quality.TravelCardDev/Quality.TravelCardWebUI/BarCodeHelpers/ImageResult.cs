using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Security.Principal;



using System.Security;



namespace Quality.BarCodeHelpers
{
    /// <summary>
    /// Code taken from the following blog entry by Jeremiah Clark:
    /// http://blogs.msdn.com/b/miah/archive/2008/11/13/extending-mvc-returning-an-image-from-a-controller-action.aspx
    /// </summary>
    public class ImageResult : ActionResult
    {
        public ImageResult(Stream imageStream, string contentType,string id,string filepath)
        {
            if (imageStream == null)
                throw new ArgumentNullException("imageStream");
            if (contentType == null)
                throw new ArgumentNullException("contentType");
            if (id == null)
                throw new ArgumentNullException("id");

            if (filepath == null)
                throw new ArgumentNullException("filepath");
            this.ImageStream = imageStream;
            this.ContentType = contentType;
            this.ID = id;
            this.FilePath = filepath;
           
        }

        public Stream ImageStream { get; private set; }
        public string ContentType { get; private set; }
        public string ID { get; set; }
        public string FilePath { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
       //     if (context == null)
        //        throw new ArgumentNullException("context");

        //    HttpResponseBase response = context.HttpContext.Response;

         //   response.ContentType = this.ContentType;


        //    HttpContext usercontext = HttpContext.Current;

            /* Get the service provider from the context */
         //   IServiceProvider iServiceProvider = usercontext as IServiceProvider;

            /*Get a Type which represents an HttpContext */
        //    Type httpWorkerRequestType = typeof(HttpWorkerRequest);

            /* Get the HttpWorkerRequest service from the service provider 
            NOTE: When trying to get a HttpWorkerRequest type from the 
            HttpContext unmanaged code permission is demanded. */

         //   HttpWorkerRequest httpWorkerRequest =
          //  iServiceProvider.GetService(httpWorkerRequestType) as HttpWorkerRequest;

            /* Get the token passed by IIS */
         //   IntPtr ptrUserToken = httpWorkerRequest.GetUserToken();

            /* Create a WindowsIdentity from the token */
         //   WindowsIdentity winIdentity = new WindowsIdentity(ptrUserToken);

          //  string username= WindowsIdentity.GetCurrent().Name;

            /* Impersonate the user */
          //  WindowsImpersonationContext impContext = winIdentity.Impersonate();
          //  string message="Impersonating: " + WindowsIdentity.GetCurrent().Name;

            /* Place resource access code here 
            You can write code for File Access, Directory Creation or
            delete file or folde */


            
            
            
            
            
            
            byte[] buffer = new byte[4096];
            while (true)
            {
                int read = this.ImageStream.Read(buffer, 0, buffer.Length);
                if (read == 0)
                    break;

                // response.OutputStream.Write(buffer, 0, read);

                using (FileStream fsOut = new FileStream(this.FilePath + this.ID+".gif", FileMode.Create))
                {
                    byte[] imgData = buffer.ToArray();
                    fsOut.Write(imgData, 0, imgData.Length);
                    FilePath = this.FilePath + this.ID + ".gif";


                     int count = 0;
           
            do
        {
            if(count>3)
            {
           //wait until the file bar code is written needed for mexico users, the web page was displaying prior to the files being written.
                System.Threading.Thread.Sleep(1);
            }
            count++;
           
             }
        while (System.IO.File.Exists(FilePath) == false&&count<60);
        
          
        }



                }

             //   impContext.Undo();
                  //  response.End();
            }
          
        }
    }
