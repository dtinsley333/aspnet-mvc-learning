using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityExample.Model;

namespace EntityExample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var chinookContext = new ChinookDBContext())
            {
               var customerList = chinookContext.Customer.Where(cust => cust.State == "NY");

            }
            Console.ReadLine();

            }
    }
}
