using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchQuiz
{
    public class Customer
    {
        public bool ReadyToPlaceOrder(Order order, Restaurant restaurant)
        {
            //if ( (!String.IsNullOrEmpty(order.Drink) && restaurant.ItemIsOnMenu(order.Drink) ) &&
            //     (!String.IsNullOrEmpty(order.Entree) && restaurant.ItemIsOnMenu(order.Entree) ) &&
            //     (!String.IsNullOrEmpty(order.Dessert) && restaurant.ItemIsOnMenu(order.Dessert) ) )
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            var isReadyToPlaceOrder = ((!String.IsNullOrEmpty(order.Drink) && restaurant.ItemIsOnMenu(order.Drink)) &&
                 (!String.IsNullOrEmpty(order.Entree) && restaurant.ItemIsOnMenu(order.Entree)) &&
                 (!String.IsNullOrEmpty(order.Dessert) && restaurant.ItemIsOnMenu(order.Dessert)));
            return isReadyToPlaceOrder;
        }
    }
}
