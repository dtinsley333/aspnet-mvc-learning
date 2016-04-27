using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchQuiz
{
    public class FastFood : Restaurant,  IProvidesLunch, IProvidesDinner
    {
        public bool HasDriveThrough { get; set; }
        public bool openForLunch { get; set; }
        public bool openForDinner { get; set; }
        public bool hasEarlyBirdSpecial { get; set; }
        public bool hasExpressLunchMenu { get; set; }

        // constructor
        public FastFood()
        {
            this.openForLunch = true;
            this.hasExpressLunchMenu = false;
            this.hasEarlyBirdSpecial = false;
            this.openForDinner = true;
            this.MenuItems = new List<MenuItem>();
        }
    }
}
