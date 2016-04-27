using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchQuiz
{
    public class Order
    {
        public string Drink { get; set; }
        public string Entree { get; set; }
        public string Dessert { get; set; }

        public bool VerifyOrderComplete()
        {
            //if (!String.IsNullOrEmpty(this.Drink) &&
            //    !String.IsNullOrEmpty(this.Entree) &&
            //    !String.IsNullOrEmpty(this.Dessert))
            //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
                bool isOrderComplete = !String.IsNullOrEmpty(this.Drink) &&
                !String.IsNullOrEmpty(this.Entree) &&
                !String.IsNullOrEmpty(this.Dessert);
            return isOrderComplete;
        }

    }
}
