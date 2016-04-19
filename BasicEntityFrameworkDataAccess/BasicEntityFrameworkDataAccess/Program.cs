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
            var employees = dbContext.Employee.Where(a => a.City == "Nashbille");

            foreach (var employee in employees)
            {
                Console.Write(employee.Name + " " + employee.City);
                    }
            Console.ReadLine();

        }
       
       
    }
   
}
