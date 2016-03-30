using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelloWorld.ViewModels;
namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {

            GreetingViewModel greetingViewModel = new GreetingViewModel
            {
                Greeting = "Good Day from Jim's Thai Restuarant! Thanks for being a regular customer.",
                CurrentDate = DateTime.Now,
                UserId = new Guid(),
                UserFirstName="Bob",
                UserLastName="Smith"

            };
            return View(greetingViewModel);
            
        }
    }
}