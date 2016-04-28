using MyDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDatabase.ViewModels
{
    public class EmployeeDetailsViewModel
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int DepartmentId { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public string DepartmentName { get; set; }
       // public List<Department> Departments { get; set; }
       
    }
}