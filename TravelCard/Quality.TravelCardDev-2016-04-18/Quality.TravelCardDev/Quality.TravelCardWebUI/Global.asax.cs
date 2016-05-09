using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Quality.WebUI.Infrastructure;

namespace Sample1
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "TravelCard",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "TravelCard", action = "PartMaintenanceIndex", id = "" }  // Parameter defaults


            );

            routes.MapRoute(
               "TravelCardwithAnchor",                                              // Route name
               "{controller}/{action}/#{id}",                           // URL with parameters
               new { controller = "TravelCard", action = "PartMaintenanceIndex", id = "" }  // Parameter defaults


           );



          
           

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas(); 
            RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
           

        
        }
    }
}