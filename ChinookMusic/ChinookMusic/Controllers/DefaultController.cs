using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChinookMusic.Models;
using ChinookMusic.ViewModels;

namespace ChinookMusic.Controllers
{
    public class DefaultController : Controller
    {
  
        public ActionResult Index()
        {
            using (var chinookContext = new ChinookDbContext())
            {
               List<Customer> customers = chinookContext.Customer.Take(1000).ToList();
                                   
          
                return View(customers);
            }
        }
        public ActionResult CanadianCustomers()
        {
            using (var chinookContext = new ChinookDbContext())
            {
                //first and last name of customer from canada order by last name
               List<CanadianCustomerViewModel> canadianCustomers = chinookContext.Customer
                    .Where(cust => cust.Country.ToLower() == "canada")
                    .OrderBy(a => a.LastName)
                    .Select(cust => new CanadianCustomerViewModel
                    { LastName=cust.LastName,
                      FirstName =cust.FirstName }).ToList();

                return View(canadianCustomers);
            }
           
        }
    }
}