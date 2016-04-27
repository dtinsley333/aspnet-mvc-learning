using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchQuiz
{
    public class Restaurant
    {
        public List<MenuItem> MenuItems { get; set; }
        public List<Customer> CurrentCustomers { get; set; }

        public int Numberof4PersonTables { get; set; }
        public int Numberof2PersonTables { get; set; }

        // constructor
        public Restaurant()
        {
            this.MenuItems = new List<MenuItem>();
            this.CurrentCustomers = new List<Customer>();
        }

        public virtual int capacity()  // will be overridden in FineDining class
        {
            return this.Numberof4PersonTables * 4 + this.Numberof2PersonTables * 2;
        }

        public void addMenuItem(string newMenuItem)
        {
            this.MenuItems.Add(new MenuItem {
                Name = newMenuItem
            });
        }

        public void addMenuItem(string newMenuItem, double Price)
        {
            this.MenuItems.Add(new MenuItem
            {
                Name = newMenuItem,
                Price = Price 
            });
        }

        public void AddCustomerToList(Customer customer)
        {
            this.CurrentCustomers.Add(customer);
        }

        public bool ItemIsOnMenu(string menuItem)
        {
            //foreach (MenuItem mi in this.MenuItems)
            //{
            //    if (mi.Name == menuItem) return true;
            //}
            var onMenu = MenuItems.Where(a => a.Name == menuItem).Select(a=>a.Name).FirstOrDefault()!= null? true :false;
           
            return onMenu;
        }

        public bool ItemIsOnMenu(MenuItem menuItem)
        {

            var isOnMenu = MenuItems.Contains(menuItem) ? true : false;
            return isOnMenu;
            //if (this.MenuItems.Contains(menuItem))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

    }
}
