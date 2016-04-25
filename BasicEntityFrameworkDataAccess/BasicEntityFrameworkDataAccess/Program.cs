using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BasicEntityFrameworkDataAccess.Models;

namespace BasicEntityFrameworkDataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer<MyStoreContext>(null);

            MyStoreContext dbContext = new MyStoreContext();
            var employees = dbContext.Employee.Where(a => a.City == "Nashville");
            Console.WriteLine(Environment.NewLine);
            Console.Write("EMPLOYEES FROM NASHVILLE" );
            Console.WriteLine(Environment.NewLine);
            foreach (var employee in employees)
            {
                Console.Write(employee.Name + " " + employee.City);
                    }
            Console.WriteLine(Environment.NewLine);

            Console.Write("EMPLOYEE DEPARTMENT LISTING");

            Console.WriteLine(Environment.NewLine);

            //join data from two tables
            var employeeDetails = (from emp in dbContext.Employee
                                   join dept in dbContext.Department
                                   on emp.DepartmentId equals dept.DepartmentId
                                   orderby dept.Name
                                   select new   //roll join results into a new object
                                   {
                                       Name = emp.Name,
                                       Description = emp.Description,
                                       DepartmentName = dept.Name,
                                       SupervisorName=dept.SupervisorTitle
                                   }).ToList();

            foreach (var details in employeeDetails)
            {
                Console.Write("Employee Name: " + details.Name + " Department Name: " + details.DepartmentName + " Supervisor Title: " + details.SupervisorName);
                Console.WriteLine(Environment.NewLine);
            }

            Console.ReadLine();

        }
       
       
    }
   
}
