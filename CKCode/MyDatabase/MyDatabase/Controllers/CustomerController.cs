using MyDatabase.Models;
using MyDatabase.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyDatabase.Controllers
{
    public class CustomerController : Controller
    {
     

        public ActionResult Index()
        {
            MyStoreContext _myStoreContext = new MyStoreContext();
            List<Customer> customers = _myStoreContext.Customer.OrderBy(a => a.LastName).ToList();
            CustomerDetailsViewModel customerModel = new CustomerDetailsViewModel
            {
                Customers = customers
            };

            return View(customerModel);

            
        }

    }
}
