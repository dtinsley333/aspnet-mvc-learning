using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Text.RegularExpressions;
using System.Web.Mvc;
namespace GenerateDocs
{
    public class ExcelOutput : ViewResult
    {
      

        public string ContentType { get; set; }
        public string Content { get; set; }
        public string OutputFileName { get; set; }
        public bool ReturnAsAttachment { get; set; }
        public string output;

        public ExcelOutput(string html, string outputFileName, bool returnAsAttachment)
        {



            this.ContentType = "application/vnd.ms-excel";
            if (html == string.Empty)
            {
                html = "<h2>There was a problem. No data was returned.</h2>";
            }
            this.Content = html;
            this.OutputFileName = outputFileName;
            this.ReturnAsAttachment = returnAsAttachment;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = ContentType;


            string htmlString = this.Content;
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.Buffer = true;

            context.HttpContext.Response.Clear();


            context.HttpContext.Response.ContentType="application/vnd.ms-excel";

            if (this.ReturnAsAttachment)
            {
                context.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + this.OutputFileName + ".xls");

            }

            context.HttpContext.Response.Buffer = false;
            context.HttpContext.Response.Write(htmlString);
          

        }
    }

}