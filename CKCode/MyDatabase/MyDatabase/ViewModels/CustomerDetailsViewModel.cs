using MyDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDatabase.ViewModels
{
    public class CustomerDetailsViewModel
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public List<Customer> Customers { get; set; }
    }
}