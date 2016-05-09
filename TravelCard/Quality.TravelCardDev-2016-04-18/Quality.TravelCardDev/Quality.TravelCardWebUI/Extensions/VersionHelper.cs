using System;
using System.Web.Mvc;
using System.Reflection;
namespace Quality.Extensions
{
    public static class VersionHelper
    {
   //
        /// <summary>
        /// Return the Current Version from the AssemblyInfo.cs file.
        /// </summary>
        public static string CurrentVersion(this HtmlHelper helper)
        {
            try
            {
                System.Version version = Assembly.GetExecutingAssembly().GetName().Version;
                return version.Major + "." + version.Minor + "." + version.Build;
            }
            catch (Exception)
            {
                return "?.?.?";
            }
        }
    }
}
