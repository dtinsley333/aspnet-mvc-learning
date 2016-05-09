using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quality.WebUI.Extensions
{
    //
    
    public static class HttpRequestExtensions
    {
        public static string CurrentBaseURL(this System.Web.HttpRequest thisHttpRequest_)
        {
            try
            {
                return thisHttpRequest_.Url.GetLeftPart(UriPartial.Authority) + "/" + thisHttpRequest_.Url.GetRightPart();
            }
            catch (Exception)
            {
                return "~/";
            }
        }

        public static string CurrentBaseURL(this System.Web.HttpRequest thisHttpRequest_, string path_)
        {
            try
            {
                return GetLeftPart(thisHttpRequest_.Url) + path_;
            }
            catch (Exception)
            {
                return "~/" + path_;
            }
        }



        private static string GetRightPart(this Uri thisUri)
        {
            try
            {
                if (thisUri.Segments != null && thisUri.Segments.Length > 1)
                    return thisUri.Segments[1];
                else
                    return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static string GetLeftPart(this Uri thisUri)
        {
            try
            {
                if (thisUri.Segments != null && thisUri.Segments.Length > 1)
                {

                    HttpContext context = HttpContext.Current;
                    string baseUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/') + '/';
                    return baseUrl;



                }
                else
                    return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }




    }

}