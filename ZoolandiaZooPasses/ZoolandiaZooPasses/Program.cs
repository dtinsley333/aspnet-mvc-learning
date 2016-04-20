using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoolandiaZooPasses
{
    class Program
    {
        static void Main(string[] args)
        {

            //create some singlepass holders 
            SinglePassHolder singlePassHolder1 = new SinglePassHolder
            {
                CustomerId = Guid.NewGuid(),
                FirstName = "Bob",
                LastName = "Smith",
                Email = "bob@bob.com",
                PassType = "Single",
                IsPassActive = false
            };

            SinglePassHolder singlePassHolder2 = new SinglePassHolder
            {
                CustomerId = Guid.NewGuid(),
                FirstName = "Bill",
                LastName = "Watson",
                Email = "bill@bill.com",
                PassType = "Single",
                IsPassActive = true
            };

            SinglePassHolder singlePassHolder3 = new SinglePassHolder
            {
                CustomerId = Guid.NewGuid(),
                FirstName = "Jenny",
                LastName = "Adams",
                Email = "Jenny@Jenny.com",
                PassType = "Single",
                IsPassActive = true
            };

            SinglePassHolder singlePassHolder4 = new SinglePassHolder
            {
                CustomerId = Guid.NewGuid(),
                FirstName = "Pepe",
                LastName = "Lopez",
                Email = "Pepe@Pepe.com",
                PassType = "Single",
                IsPassActive = true
            };
            SinglePassHolder singlePassHolder5 = new SinglePassHolder
            {
                CustomerId = Guid.NewGuid(),
                FirstName = "Randy",
                LastName = "Orton",
                Email = "Orton@Orton.com",
                PassType = "Single",
                IsPassActive = false
            };

            //build a list of single pass holders  
            List<SinglePassHolder> singlePassHolders = new List<SinglePassHolder>();
            singlePassHolders.Add(singlePassHolder1);
            singlePassHolders.Add(singlePassHolder2);
            singlePassHolders.Add(singlePassHolder3);
            singlePassHolders.Add(singlePassHolder4);
            singlePassHolders.Add(singlePassHolder5);

            List<SinglePassHolder> singleCustomersHavingActivePasses = singlePassHolders.Where(a => a.IsPassActive == false).OrderBy(a => a.LastName).ToList();


            Console.WriteLine("LIST OF INACTIVE SINGLE ZOO PASSHOLDERS NEEDING EMAIL REMINDER TO RENEW");

            foreach (var customer in singleCustomersHavingActivePasses)
            {
                var currentPass = customer.IsPassActive ? "Active" : "Inactive";

                Console.WriteLine("CustomerId: " + customer.CustomerId.ToString() + "  Name: " + customer.FirstName + " " + customer.LastName + " Email: " + customer.Email + " Zoo Pass Type: " + customer.PassType + "Active? " + currentPass);
                Console.ReadLine();
            }





        }
    }
}
