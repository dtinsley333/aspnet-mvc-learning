using SurgeEd.Models;
using SurgeEd.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SurgeEd.Controllers
{
    public class EmployeeController : Controller
    {
     

        public ActionResult Index()
        {
            MyStoreContext _myStoreContext = new MyStoreContext();
            var employeeDetails = (from emp in _myStoreContext.Employee
                                   join dept in _myStoreContext.Department
                                   on emp.DepartmentId equals dept.DepartmentId
                                   orderby dept.Name
                                   select new EmployeeDetailsViewModel
                                   {
                                       Name = emp.Name,
                                       Description = emp.Description,
                                       DepartmentName = dept.Name
                                   }).ToList();

            EmployeeDepartmentDetailsViewModel employeeDepartmentDetails= new EmployeeDepartmentDetailsViewModel
            {
                Employees=employeeDetails
            };

                
            return View(employeeDepartmentDetails);

            
        }

    }
}
