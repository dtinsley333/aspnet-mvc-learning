using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChinookMusic3.Models;
using System.Data.Entity;
using ChinookMusic3.ViewModels;

namespace ChinookMusic3.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            using (var chinookContext = new ChinookContext())
            {
                List<Customer> customers = chinookContext.Customer.Take(1000).ToList();

                return View(customers);
            }
        
        }

        public ActionResult CanadianCustomers()
        {
            using (var chinookContext = new ChinookContext())
            {
                //first and last name of customer from canada order by last name
                List<CanadianCustomerViewModel> canadianCustomers = chinookContext.Customer
                     .Where(cust => cust.Country.ToLower() == "canada")
                     .OrderBy(a => a.LastName)
                     .Select(cust => new CanadianCustomerViewModel
                     {
                         LastName = cust.LastName,
                         FirstName = cust.FirstName
                     }).ToList();

                return View(canadianCustomers);
            }

        }
    }
}