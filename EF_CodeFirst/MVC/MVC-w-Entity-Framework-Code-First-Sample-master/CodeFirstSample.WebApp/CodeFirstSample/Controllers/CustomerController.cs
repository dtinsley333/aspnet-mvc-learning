using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodeFirstSample.Models;
using CodeFirstSample.ViewModels;

namespace CodeFirstSample.Controllers
{
    public class CustomerController : Controller
    {
        

        [HttpGet]
        public ActionResult Index()
        {
            MyStoreContext _myStoreContext = new MyStoreContext();
            List<Customer> customers = _myStoreContext.Customer.OrderBy(a=>a.LastName).ToList();
            CustomerDetailsViewModel customerModel = new CustomerDetailsViewModel
            {
               Customers =customers
            };

            return View(customerModel);
        }

        [HttpGet]
        public ActionResult Details(int? customerId)
        {
            MyStoreContext _myStoreContext = new MyStoreContext();

            Customer customer = _myStoreContext.Customer.Find(customerId);

            CustomerDetailsViewModel customerModel = new CustomerDetailsViewModel
            {
                FirstName=customer.FirstName,
                LastName=customer.LastName,
                ContactNumber=customer.ContactNumber
            };
            return View(customerModel);
        }

       [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        public ActionResult Create(CustomerDetailsViewModel customerDetails)
        {
            MyStoreContext _myStoreContext = new MyStoreContext();

            if (ModelState.IsValid)
            {
                Customer customer = new Customer
                {
                    FirstName = customerDetails.FirstName,
                    LastName = customerDetails.LastName,
                    ContactNumber = customerDetails.ContactNumber
                };
                _myStoreContext.Customer.Add(customer);
                _myStoreContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerDetails);
        }

        [HttpGet]
        public ActionResult Edit(int? customerId)
        {
            MyStoreContext _myStoreContext = new MyStoreContext();
           
           Customer customer = _myStoreContext.Customer.Find(customerId);

            CustomerDetailsViewModel customerModel = new CustomerDetailsViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                ContactNumber = customer.ContactNumber
            };
            return View(customerModel);
        }

   
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerDetailsViewModel customerDetails)
        {
            MyStoreContext _myStoreContext = new MyStoreContext();
            var customer = _myStoreContext.Customer.Find(customerDetails.CustomerId);
            if (ModelState.IsValid)
            {
                customer.FirstName = customerDetails.FirstName;
                customer.LastName = customerDetails.LastName;
                customer.ContactNumber = customerDetails.ContactNumber;
 
                _myStoreContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerDetails);
        }

       
        public ActionResult Delete(int? customerId)
        {
            MyStoreContext _myStoreContext = new MyStoreContext();
            if (customerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _myStoreContext.Customer.Find(customerId);

            ViewBag.Title = "Got a quarrel with " + customer.FirstName + " or something?";

            _myStoreContext.Customer.Remove(customer);
            _myStoreContext.SaveChanges();
            return View(customer);
        }

        [HttpPost]
        public ActionResult Delete(int customerId)
        {
            MyStoreContext _myStoreContext = new MyStoreContext();
            Customer customer = _myStoreContext.Customer.Find(customerId);
            if (customer != null)
            {
                _myStoreContext.Customer.Remove(customer);
                _myStoreContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    MyStoreContext _myStoreContext = new MyStoreContext();
        //    if (disposing)
        //    {
        //        _myStoreContext.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
