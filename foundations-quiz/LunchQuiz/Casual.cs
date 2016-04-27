using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchQuiz
{
    public class Casual : Restaurant, IProvidesLunch, IProvidesDinner
    {
        public bool HasPatio { get; set; }
        public bool openForLunch { get; set; }
        public bool hasExpressLunchMenu { get; set; }
        public bool openForDinner { get; set; }
        public bool hasEarlyBirdSpecial { get; set; }

        // constructor
        public Casual()
        {
            this.openForLunch = true;
            this.hasExpressLunchMenu = true;
            this.MenuItems.Add(new MenuItem
            {
                Name = "Jalapeno Cheese Poppers",
                Price = 5.99,
                Description = "Irresistible nuggets of death"
            });
        }
    }

}
